using FluentValidation;
using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Validators
{
    public class CreateToDoValidator:AbstractValidator<ToDoCreateDto>
    {
        public CreateToDoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().
                WithMessage("Title Boş Olamaz").
                MinimumLength(3).
                WithMessage("Title minimum üç karakterden oluşmalıdır").
                MaximumLength(100).
                WithMessage("Title maksimum yüz karakterden oluşabilir");
            RuleFor(x=>x.Description).MaximumLength(500).
                WithMessage("Description maksimum beş yüz karakterden oluşabilir");


            // 1. DueDate Kuralı: Eğer tarih girildiyse, bugünden büyük veya eşit olmalı
            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Bitiş tarihi geçmiş bir tarih olamaz.")
                .When(x => x.DueDate.HasValue); // Sadece değer gönderildiyse bu kuralı çalıştır

            // 2. Priority Kuralı: Sadece 1, 2 veya 3 değerlerini alabilir
            RuleFor(x => x.Priority)
                .InclusiveBetween(1, 3)
                .WithMessage("Öncelik değeri sadece 1 (Düşük), 2 (Orta) veya 3 (Yüksek) olabilir.");
        }
    }
}
