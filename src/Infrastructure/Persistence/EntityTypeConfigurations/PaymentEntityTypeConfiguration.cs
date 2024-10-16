﻿using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.InvoiceId)
                   .HasColumnName("invoice_id")
                   .IsRequired();

            builder.HasIndex(p => p.InvoiceId);
                   

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(p => p.Reference)
                   .HasColumnName("reference")
                   .IsRequired();

            builder.HasIndex(p => p.Reference)
                 .IsUnique();

            builder.Property(p => p.Option)
                   .HasColumnName("option")
                   .HasColumnType("varchar(50)")
                   .HasConversion<EnumToStringConverter<PaymentOption>>()
                   .IsRequired();

            builder.Property(p => p.Status)
                   .HasColumnName("status")
                   .HasColumnType("varchar(50)")
                   .HasConversion<EnumToStringConverter<PaymentStatus>>()
                   .IsRequired();

            builder.Property(p => p.CreatedBy)
                   .HasColumnName("created_by")
                   .HasColumnType("varchar(255)");

            builder.Property(p => p.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(p => p.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(p => p.ModifiedOn)
                .HasColumnName("modified_date");

            
        }
    }
}
