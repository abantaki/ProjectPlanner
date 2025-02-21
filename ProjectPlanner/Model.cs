//using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ProjectPlanner;
public class ProjectPlannerContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public string DbPath { get; set; }

    public ProjectPlannerContext() {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Project>()
            .HasOne(p => p.ResponsibleUser)
            .WithMany()
            .HasForeignKey(p => p.ResponsibleUserId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

        options.UseSqlite($"Data Source={DbPath}");
    }

}

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
}

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; } // Nullable for ongoing projects

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pending";

    [ForeignKey("User")]
    public int ResponsibleUserId { get; set; }

    public User ResponsibleUser { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Customer { get; set; } = string.Empty;
}
