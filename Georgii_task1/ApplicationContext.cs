using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<AppVersion> AppVersions { get; set; } = null!;

    public ApplicationContext(){}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\helloapp.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(u => u.Email);
        builder.Property(u => u.Age).HasDefaultValue(18);
        builder.ToTable(t => t.HasCheckConstraint("Age", "Age > 0 AND Age < 100"));
        builder
            .HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .OnDelete(DeleteBehavior.SetNull);
    }
}