using Autofac;
using Autofac.Extensions.DependencyInjection;
using Checkout;
using Checkout.Application;
using Checkout.Controllers;
using Checkout.Infra;
using Checkout.Infrastructure;
using Checkout.Infrastructure.Filters;
using Checkout.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Refit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CheckoutContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("Checkout"),
            new MySqlServerVersion(new Version(8, 0, 20)),
            x =>
            {
                x.EnableRetryOnFailure();
                x.MigrationsAssembly("Checkout.API");
            })
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new MediatorModule());
});


builder.Services.AddTransient<IBankService, BankSimulatorClient>();
//builder.Services.AddTransient<IBankService, FakeBankService>();


builder.Services.AddAutoMapper(typeof(PaymentRequestProfile));
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));

});

builder.Services
    .AddRefitClient<IBankSimulatorClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BANK_SIMULATOR_URI")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger("Checkout.API", "v1");
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

var app = builder.Build();
app.UseSwaggerEndpointAndWebUI("api/workspace", "Checkout.API", "v1");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();