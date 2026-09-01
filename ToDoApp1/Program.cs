using FluentValidation; 
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;
using ToDoApp1.Data;
using ToDoApp1.Validators;

var builder = WebApplication.CreateBuilder(args);
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "todo.db");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Swagger Jeneratör

builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateToDoValidator>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
    .MinimumLevel.Information() // Bizim eklediklerimiz  görünecek
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Microsoft SADECE Warning ve Error
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .WriteTo.Console()
        .WriteTo.SQLite(
            sqliteDbPath: dbPath,
            tableName: "Logs" 
        );

});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

var env = app.Environment.EnvironmentName;

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ToDoApp1.Middlewares.ExceptionMiddleware>();

app.Run();