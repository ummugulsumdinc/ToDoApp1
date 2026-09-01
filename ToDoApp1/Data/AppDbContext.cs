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
        public DbSet<Log> Logs { get; set; }
    }
}