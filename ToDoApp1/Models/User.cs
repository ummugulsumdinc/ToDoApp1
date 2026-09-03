namespace ToDoApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Güvenlik için düz şifre yerine Hash tutuyoruz
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation Properties - Bir kullanıcının verileri
        public ICollection<ToDo> ToDos { get; set; } = new List<ToDo>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Status> Statuses { get; set; } = new List<Status>();

    }
}
