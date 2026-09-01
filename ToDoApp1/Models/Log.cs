using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp1.Models
{
    [Table("Logs")] // Entity Framework'e "Veritabanındaki Logs tablosuna bak" diyoruz.
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string? RenderedMessage { get; set; } // Logun kendi metni
        public string? Level { get; set; } // Information, Warning, Error
        public string? Timestamp { get; set; } // Ne zaman atıldığı
        public string? Exception { get; set; } // Varsa hata detayı
    }
}