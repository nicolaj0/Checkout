// <auto-generated />
using System;
using Checkout.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Checkout.Migrations
{
    [DbContext(typeof(CheckoutContext))]
    [Migration("20220523004949_InitialCreate4")]
    partial class InitialCreate4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Checkout.Domain.PaymentRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Identifier")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("RequestCardExpiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RequestCardHolderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RequestCardNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RequestCardSecurityNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Identifier");

                    b.ToTable("PaymentRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
