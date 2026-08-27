using System.Net.NetworkInformation;

namespace ToDoApp1.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; }

        // --- 1. PROJE İLİŞKİSİ ---
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!; // ProjectName değil, tablo adı olan Project!

        // --- 2. DURUM İLİŞKİSİ ---
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!; // StatusName değil, Status!

        // --- 3. KULLANICI İLİŞKİSİ ---
        public int UserId { get; set; }
        public User User { get; set; } = null!; // string UserName değil, User tablosunun kendisi!
    }
}