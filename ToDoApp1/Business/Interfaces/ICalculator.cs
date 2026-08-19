using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Business.Interfaces
{
    public interface ICalculator
    {
        double Hesapla(CalculateDto calculation);// dönüş tipi metrod adı (parametre)
    }
}
