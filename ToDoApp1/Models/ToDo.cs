using System.Net.NetworkInformation;

namespace ToDoApp1.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; }

        // PROJECT İLİŞKİSİ
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!; // ProjectTitle değil, tablo adı olan Project!

        //  STATUS İLİŞKİSİ 
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!; // StatusName değil, Status!

        //  USER İLİŞKİSİ 
        public int UserId { get; set; }
        public User User { get; set; } = null!; // string UserName değil, User tablosunun kendisi!

        public int? ParentId { get; set; }=null!;
        public ToDo? ParentToDo { get; set; }
        public ICollection<ToDo> SubToDos { get; set; } = new List<ToDo>();
    
}
}