namespace ToDoApp1.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message): base(message) //constructor
        {
        }
    }
}
