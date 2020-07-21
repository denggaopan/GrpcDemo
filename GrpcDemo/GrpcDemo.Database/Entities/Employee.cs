using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.Database.Entities
{
    public class Employee
    {
        public string Id { get; set; }

        public string BusinessId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Job { get; set; }

        public virtual Business Business { get; set; }
    }

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.BusinessId).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(20);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Job).HasMaxLength(20);
        }
    }
}
