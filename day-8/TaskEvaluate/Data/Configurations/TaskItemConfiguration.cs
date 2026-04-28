using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEvaluate.Models;

namespace TaskEvaluate.Data.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(100);
            builder.Property(t => t.Description)
                .HasMaxLength(100);
            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("getDate()");
        }
    }
}
