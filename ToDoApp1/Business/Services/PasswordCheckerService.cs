using FluentValidation;
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Business.Services
{
public class PasswordCheckerService: IPasswordChecker
    {
        private readonly IValidator<PasswordCheckerRequestDto> _validator;
        public PasswordCheckerService(IValidator<PasswordCheckerRequestDto> validator)
        {
            _validator = validator;
        }
        public PasswordCheckerResponseDto CheckPassword(PasswordCheckerRequestDto request)
        {
            var validationResult=_validator.Validate(request);// requesti validate edip sonucu validation result içine kaydeder (response)
            if (!validationResult.IsValid)//eğer hatalı bir şey varsa
            {
                var firstErrorMessage=validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Geçersiz şifre.";
                return new PasswordCheckerResponseDto { IsValid = false, Message = firstErrorMessage };

            }
            return new PasswordCheckerResponseDto
            {
                IsValid = true,
                Message = "Şifre tüm kurallara uygundur."
            };
        }
        //public string GetPasswordRules()
        //{
        //    return "ŞİFRE KURALLARI\n Şifre boş olamaz!!!\n Şifre minimum 8 karakter olmalıdır!!!\nŞifre minimum 1 tane BÜYÜK harf içermelidir!!!\nŞifre minimum 1 tane KÜÇÜK harf içermelidir!!!\nŞifre minimum 1 tane RAKAM  içermelidir!!!\nŞifre minimum 1 tane özel karakter içermelidir!!!" ;
        //  }

}
}