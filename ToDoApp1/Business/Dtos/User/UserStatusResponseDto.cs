namespace ToDoApp1.Business.Dtos.User
{
    public class UserStatusResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int TotalToDo {  get; set; }
        public int UncompletedToDo {  get; set; }
        public int CurrentlyCompletingToDo {  get; set; }
        public int CompletedToDo { get; set; }
        public List<UserBasicToDoDto> TotalToDoList { get; set; }=new List<UserBasicToDoDto>();

    }
}
