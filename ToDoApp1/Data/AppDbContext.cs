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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Var olan ayarları koru

            // iç içe TODOLARIN kuralı
            modelBuilder.Entity<ToDo>()
                .HasOne(x => x.ParentToDo)
                .WithMany(x => x.SubToDos)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = (int)DefaultStatuses.Uncompleted, Name = "Tamamlanmadı" },
                new Status { Id = (int)DefaultStatuses.InProgress, Name = "Devam Ediyor" },
                new Status { Id = (int)DefaultStatuses.Completed, Name = "Tamamlandı" }
                );
        }
    }
}