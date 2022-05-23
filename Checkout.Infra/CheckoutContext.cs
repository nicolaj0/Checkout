using Checkout.Domain;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infra;

public class CheckoutContext : DbContext
{
    public CheckoutContext()
    {
        
    }
    public CheckoutContext(DbContextOptions<CheckoutContext> options)
        : base(options)
    {
    }
    public virtual  DbSet<PaymentRequest> PaymentRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentRequest>()
            .HasIndex(p=>p.Identifier);
    }
}