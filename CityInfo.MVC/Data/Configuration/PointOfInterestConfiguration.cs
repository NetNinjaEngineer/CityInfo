using CityInfo.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityInfo.MVC.Data.Configuration;

public class PointOfInterestConfiguration : IEntityTypeConfiguration<PointOfInterest>
{
    public void Configure(EntityTypeBuilder<PointOfInterest> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100).IsRequired();

        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR")
            .HasMaxLength(1500).IsRequired();

        builder.Property(p => p.Category)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100).IsRequired();

        builder.HasOne(p => p.City)
            .WithMany(p => p.PointOfInterests)
            .HasForeignKey(p => p.CityId)
            .IsRequired();

        builder.ToTable("PointOfInterests");

    }
}
