using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Swagger Jeneratörü

builder.Services.AddScoped<ICalculator, CalculateService>();
builder.Services.AddSingleton<IToDoService, ToDoService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   // Swagger JSON dosyasýný oluþturur
    app.UseSwaggerUI(); // Swagger UI arayüzünü sunar
}

var env = app.Environment.EnvironmentName;

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();