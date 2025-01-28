using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechInterviewTask.Entities.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id");
        
        builder.Property(t => t.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.Currency)
            .HasColumnName("currency")
            .HasMaxLength(3)
            .IsRequired();
        
        builder.Property(t => t.FromUserId)
            .HasColumnName("from_user_id")
            .IsRequired();

        builder.Property(t => t.ToUserId)
            .HasColumnName("to_user_id")
            .IsRequired();
        
        builder.HasOne(t => t.FromUser)
            .WithMany()
            .HasForeignKey(t => t.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(t => t.ToUser)
            .WithMany()
            .HasForeignKey(t => t.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}