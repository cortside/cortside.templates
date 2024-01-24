﻿// <auto-generated />
using System;
using Acme.ShoppingCart.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acme.ShoppingCart.Data.Migrations {
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230315165459_add comment examples")]
    partial class AddCommentExamples {
        protected override void BuildTargetModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Address", b => {
                b.Property<int>("AddressId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                b.Property<string>("City")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Country")
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("CreateSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("LastModifiedDate")
                    .HasColumnType("datetime2");

                b.Property<Guid>("LastModifiedSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("State")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Street")
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("ZipCode")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("AddressId");

                b.HasIndex("CreateSubjectId");

                b.HasIndex("LastModifiedSubjectId");

                b.ToTable("Address", "dbo");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Customer", b => {
                b.Property<int>("CustomerId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                b.Property<Guid>("CreateSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<Guid>("CustomerResourceId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Email")
                    .HasMaxLength(250)
                    .HasColumnType("nvarchar(250)");

                b.Property<string>("FirstName")
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<DateTime>("LastModifiedDate")
                    .HasColumnType("datetime2");

                b.Property<Guid>("LastModifiedSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("LastName")
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.HasKey("CustomerId");

                b.HasIndex("CreateSubjectId");

                b.HasIndex("LastModifiedSubjectId");

                b.ToTable("Customer", "dbo");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Order", b => {
                b.Property<int>("OrderId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasComment("Primary Key");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                b.Property<int?>("AddressId")
                    .HasColumnType("int");

                b.Property<Guid>("CreateSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("CustomerId")
                    .HasColumnType("int");

                b.Property<DateTime>("LastModifiedDate")
                    .HasColumnType("datetime2");

                b.Property<Guid>("LastModifiedSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("OrderResourceId")
                    .HasColumnType("uniqueidentifier")
                    .HasComment("Public unique identifier");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasColumnType("nvarchar(20)")
                    .HasComment("Order status (created, paid, shipped, cancelled)");

                b.HasKey("OrderId");

                b.HasIndex("AddressId");

                b.HasIndex("CreateSubjectId");

                b.HasIndex("CustomerId");

                b.HasIndex("LastModifiedSubjectId");

                b.HasIndex("OrderResourceId")
                    .IsUnique();

                b.ToTable("Order", "dbo");

                b.HasComment("Orders");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.OrderItem", b => {
                b.Property<int>("OrderItemId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasComment("Primary Key");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                b.Property<Guid>("CreateSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<Guid>("ItemId")
                    .HasColumnType("uniqueidentifier")
                    .HasComment("FK to Item in Catalog service");

                b.Property<DateTime>("LastModifiedDate")
                    .HasColumnType("datetime2");

                b.Property<Guid>("LastModifiedSubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<int?>("OrderId")
                    .HasColumnType("int");

                b.Property<int>("Quantity")
                    .HasColumnType("int")
                    .HasComment("Quantity of Sku");

                b.Property<string>("Sku")
                    .HasMaxLength(10)
                    .HasColumnType("nvarchar(10)")
                    .HasComment("Item Sku");

                b.Property<decimal>("UnitPrice")
                    .HasColumnType("money")
                    .HasComment("Per quantity price");

                b.HasKey("OrderItemId");

                b.HasIndex("CreateSubjectId");

                b.HasIndex("LastModifiedSubjectId");

                b.HasIndex("OrderId");

                b.ToTable("OrderItem", "dbo");

                b.HasComment("Items that belong to an Order");
            });

            modelBuilder.Entity("Cortside.AspNetCore.Auditable.Entities.Subject", b => {
                b.Property<Guid>("SubjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("FamilyName")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("GivenName")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Name")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("UserPrincipalName")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("SubjectId");

                b.ToTable("Subject", "dbo");
            });

            modelBuilder.Entity("Cortside.DomainEvent.EntityFramework.Outbox", b => {
                b.Property<int>("OutboxId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OutboxId"), 1L, 1);

                b.Property<string>("Body")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("CorrelationId")
                    .HasMaxLength(250)
                    .HasColumnType("nvarchar(250)");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("EventType")
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("nvarchar(250)");

                b.Property<DateTime>("LastModifiedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("LockId")
                    .HasMaxLength(36)
                    .HasColumnType("nvarchar(36)");

                b.Property<string>("MessageId")
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnType("nvarchar(36)");

                b.Property<DateTime?>("PublishedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("RoutingKey")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<DateTime>("ScheduledDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("nvarchar(10)");

                b.Property<string>("Topic")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("OutboxId");

                b.HasIndex("MessageId")
                    .IsUnique();

                b.HasIndex("ScheduledDate", "Status")
                    .HasDatabaseName("IX_ScheduleDate_Status");

                SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("ScheduledDate", "Status"), new[] { "EventType" });

                b.HasIndex("Status", "LockId", "ScheduledDate")
                    .HasDatabaseName("IX_Status_LockId_ScheduleDate");

                b.ToTable("Outbox", "dbo");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Address", b => {
                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "CreatedSubject")
                    .WithMany()
                    .HasForeignKey("CreateSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "LastModifiedSubject")
                    .WithMany()
                    .HasForeignKey("LastModifiedSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("CreatedSubject");

                b.Navigation("LastModifiedSubject");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Customer", b => {
                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "CreatedSubject")
                    .WithMany()
                    .HasForeignKey("CreateSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "LastModifiedSubject")
                    .WithMany()
                    .HasForeignKey("LastModifiedSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("CreatedSubject");

                b.Navigation("LastModifiedSubject");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Order", b => {
                b.HasOne("Acme.ShoppingCart.Domain.Entities.Address", "Address")
                    .WithMany()
                    .HasForeignKey("AddressId")
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "CreatedSubject")
                    .WithMany()
                    .HasForeignKey("CreateSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("Acme.ShoppingCart.Domain.Entities.Customer", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "LastModifiedSubject")
                    .WithMany()
                    .HasForeignKey("LastModifiedSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("Address");

                b.Navigation("CreatedSubject");

                b.Navigation("Customer");

                b.Navigation("LastModifiedSubject");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.OrderItem", b => {
                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "CreatedSubject")
                    .WithMany()
                    .HasForeignKey("CreateSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("Cortside.AspNetCore.Auditable.Entities.Subject", "LastModifiedSubject")
                    .WithMany()
                    .HasForeignKey("LastModifiedSubjectId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("Acme.ShoppingCart.Domain.Entities.Order", null)
                    .WithMany("Items")
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.NoAction);

                b.Navigation("CreatedSubject");

                b.Navigation("LastModifiedSubject");
            });

            modelBuilder.Entity("Acme.ShoppingCart.Domain.Entities.Order", b => {
                b.Navigation("Items");
            });
#pragma warning restore 612, 618
        }
    }
}
