using FluentValidation;
using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Validators
{
    public class PasswordCheckerValidator: AbstractValidator<PasswordCheckerRequestDto>
    {
        public PasswordCheckerValidator()
        { 
            RuleFor(x=> x.Password).NotEmpty()
                .WithMessage("Şifre boş olamaz!!!")
                .MinimumLength(8)
                .WithMessage("Şifre minimum 8 karakter olmalıdır!!!")
                .Matches("[A-Z]")
                .WithMessage("Şifre minimum 1 tane BÜYÜK harf içermelidir!!!")
                .Matches("[a-z]")
                .WithMessage("Şifre minimum 1 tane KÜÇÜK harf içermelidir!!!")
                .Matches("[0-9]")
                .WithMessage("Şifre minimum 1 tane RAKAM  içermelidir!!!")
                .Matches("[^a-zA-Z0-9]")//bunlardan olmayaan bir karakter ara demek
                .WithMessage("Şifre minimum 1 tane özel karakter içermelidir!!!");
        }
    }
}
