# ToDoApp1

**[🇹🇷 Türkçe](#türkçe) | [🇬🇧 English](#english)**

---

## Türkçe

Kullanıcıların görevlerini (to-do), projelerini ve durumlarını yönetebildiği bir REST API. ASP.NET Core ile geliştirilmiştir ve yaz stajı kapsamında yazılmıştır.

### Özellikler

- Kullanıcı kaydı ve JWT ile giriş yapma
- Görev (ToDo) oluşturma, listeleme, güncelleme, silme
- Görevleri duruma göre filtreleme, tarihe göre sıralama ve arama
- Görevi tamamlandı olarak işaretleme
- Proje ve durum (status) yönetimi
- FluentValidation ile girdi doğrulama
- Ortak hata yönetimi (middleware)

### Kullanılan Teknolojiler

- ASP.NET Core (.NET 10)
- Entity Framework Core (SQLite)
- FluentValidation
- JWT Authentication
- BCrypt (şifre hash'leme)
- Swagger

### Proje Yapısı

```
ToDoApp1/
├── Controllers/     # API uç noktaları
├── Business/        # Servisler ve DTO'lar
├── Data/            # Veritabanı bağlamı (AppDbContext)
├── Models/          # Veritabanı modelleri
├── Validators/      # FluentValidation kuralları
├── Middlewares/     # Hata yönetimi
├── Exceptions/      # Özel exception sınıfları
├── Migrations/      # EF Core migration'ları
└── Program.cs       # Uygulama başlangıç noktası
```

### Kurulum

```bash
git clone <repo-url>
cd ToDoApp1
dotnet restore
dotnet ef database update
dotnet run
```

Uygulama çalıştıktan sonra Swagger arayüzüne `/swagger` yolundan erişebilirsiniz.

### API Uç Noktaları

| Alan | Yol | Açıklama |
|---|---|---|
| Users | `/api/users` | Kayıt, giriş, listeleme, güncelleme, silme |
| ToDos | `/api/todos` | Görev CRUD, filtreleme, arama, tamamlama (JWT gerekli) |
| Projects | `/api/projects` | Proje CRUD |
| Statuses | `/api/statuses` | Durum CRUD |

### Kimlik Doğrulama

`/api/users/login` ile giriş yapıldıktan sonra dönen JWT token, korumalı uç noktalarda `Authorization: Bearer <token>` başlığıyla gönderilmelidir.

---

## English

A REST API that lets users manage their to-do tasks, projects, and statuses. Built with ASP.NET Core as part of a summer internship project.

### Features

- User registration and JWT-based login
- Create, list, update, and delete to-do tasks
- Filter tasks by status, sort by date, and search
- Mark tasks as completed
- Project and status management
- Input validation with FluentValidation
- Centralized error handling (middleware)

### Tech Stack

- ASP.NET Core (.NET 10)
- Entity Framework Core (SQLite)
- FluentValidation
- JWT Authentication
- BCrypt (password hashing)
- Swagger

### Project Structure

```
ToDoApp1/
├── Controllers/     # API endpoints
├── Business/        # Services and DTOs
├── Data/            # Database context (AppDbContext)
├── Models/          # Database models
├── Validators/      # FluentValidation rules
├── Middlewares/      # Error handling
├── Exceptions/       # Custom exception classes
├── Migrations/        # EF Core migrations
└── Program.cs        # Application entry point
```

### Setup

```bash
git clone <repo-url>
cd ToDoApp1
dotnet restore
dotnet ef database update
dotnet run
```

Once running, you can access the Swagger UI at `/swagger`.

### API Endpoints

| Area | Path | Description |
|---|---|---|
| Users | `/api/users` | Register, login, list, update, delete |
| ToDos | `/api/todos` | Task CRUD, filtering, search, completion (requires JWT) |
| Projects | `/api/projects` | Project CRUD |
| Statuses | `/api/statuses` | Status CRUD |

### Authentication

After logging in via `/api/users/login`, include the returned JWT token in protected endpoints using the `Authorization: Bearer <token>` header.
