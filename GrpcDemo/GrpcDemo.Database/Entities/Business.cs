using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.Database.Entities
{
    public class Business
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }

    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable(nameof(Business));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(20);
            builder.Property(x => x.Address).HasMaxLength(50);
            builder.Property(x => x.Tel).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(50);
        }
    }
}
