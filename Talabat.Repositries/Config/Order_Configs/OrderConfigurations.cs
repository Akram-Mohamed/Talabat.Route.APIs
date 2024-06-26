﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositries.Config.Order_Configs
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.Property(order => order.Status)
                .HasConversion
                (
                    (orderStatus) => orderStatus.ToString(),
                    (orderStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus)
                );

            builder.HasOne(order => order.DelivreyMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(order => order.SubTotal).HasColumnType("decimal(12,2)");

            builder.HasMany(order => order.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
