using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Business.Interfaces
{
    public interface IPasswordChecker
    {
        public PasswordCheckerResponseDto CheckPassword(PasswordCheckerRequestDto request);
        // dönüş tipi metodun adı(parametre)  request ile alınacak response ile döndürülecek
        //string GetPasswordRules();//kuralları yazdırır
    }
}
