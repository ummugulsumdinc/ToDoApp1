using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Business.Services
{
    public class CalculateService: ICalculator
    {
        public double Hesapla(CalculateDto calculation)
        {
            double result = 0.0;

            switch (calculation.Operation)
            {
                case "+":
                    result = calculation.Num1 + calculation.Num2;
                    break;
                case "-":
                    result = calculation.Num1 - calculation.Num2;
                    break;
                case "*":
                case "x":
                    result = calculation.Num1 * calculation.Num2;
                    break;
                case "/":
                    if (calculation.Num2 != 0)
                    {
                        result = calculation.Num1 / calculation.Num2;
                    }
                    else
                    {
                        
                        throw new DivideByZeroException("Hata: Bir sayı sıfıra bölünemez!");
                    }
                    break;
                default:
                    throw new ArgumentException("Hata: Geçersiz işlem türü!");
            }

            return result;
        }
    }
}
