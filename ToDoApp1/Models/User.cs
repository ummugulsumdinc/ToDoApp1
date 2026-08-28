using ToDoApp1.Business.Dtos.User;

namespace ToDoApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
        public ICollection<ToDo> TotalToDoList { get; set; } = new List<ToDo>();
        // total todo listesi
    }
}
