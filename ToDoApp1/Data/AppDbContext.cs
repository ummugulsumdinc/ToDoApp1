using Microsoft.EntityFrameworkCore;
using ToDoApp1.Models;

namespace ToDoApp1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanında oluşacak tüm tablolarımız
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Status> Statuses { get; set; }

        // tablolar arsı ilişki kuralları
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDo>()
                .HasOne(t => t.User)//here todonun  bir user olabilir
                .WithMany(u => u.ToDos)//bir userın birden fazla todosu olabilir
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);// user silinirken todolar silinmesin diye önlem

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)//her projenin bir user
                .WithMany(u => u.Projects)// bir userın birden fazla todosu olabilri
                .HasForeignKey(p=>p.UserId)
                .OnDelete(DeleteBehavior.Restrict );
                
        }
    }
}