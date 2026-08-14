namespace ToDoApp1.Business.Dtos
{
    public class PasswordCheckerRequestDto
    {
        public string Password { get; set; } = string.Empty;
    }
    
}
// empty yazınca başlangıçta  şifrenin boş olmasını sağlar ama bellekte yer ayrılmıstır null değil yani
// get;set; ile veri hem okunabilir hem de her zaman değiştirilebilir
// get;init; yazsaydık nesneyi okuyabilirdik ama sadece nesne oluşturulurken değer ataması yapabilirdik sonrasında değiştirme ve atama işlemi yapamazdık
