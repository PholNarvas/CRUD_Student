using CRUDApplication.Models;
using Microsoft.EntityFrameworkCore;

public class CrudDbContext : DbContext
{
    public CrudDbContext(DbContextOptions<CrudDbContext> options)
    : base(options)
    {

    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Department> Departments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .ToTable("tblStudents")
            .HasKey(s => s.StudentsID);

        modelBuilder.Entity<Class>()
            .ToTable("tblClasses")
            .HasKey(c => c.ClassID);

        modelBuilder.Entity<Department>()
            .ToTable("tblDepartments")
            .HasKey(d => d.DepartmentID);
    }
}