using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;// appsettings.json'dan veri okumak için
using Microsoft.IdentityModel.Tokens;// Şifreleme algoritmaları için
using System.IdentityModel.Tokens.Jwt; // JWT token oluşturucu sınıflar için
using System.Security.Claims; // Token içine veri (Id) gömmek için
using System.Text;// Metinleri byte dizisine çevirmek için
using ToDoApp1.Business.Dtos.User;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Data;
using ToDoApp1.Models;

namespace ToDoApp1.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, IConfiguration configuration)//constructor
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<UserResponseDto>> GetAll()
        {
            var users=await _context.Users.ToListAsync();
            var responseList = new List<UserResponseDto>();

            foreach (var user in users)
            {
                responseList.Add(MapToDto(user));
            }
            return responseList;
        }

        public async Task<UserResponseDto?> GetById(int id) 
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                return MapToDto(user);
            }
            return null;
        }
        public async Task<UserResponseDto> Add(UserCreateDto userDto)
        {
            //passwordü şifreliyoruz
            string _passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var newUser = new User
            {
                Name = userDto.Name,
                Surname=userDto.Surname,   
                Email=userDto.Email,
                PasswordHash=_passwordHash
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();// database kaydediyoruz 
            return MapToDto(newUser);
        }
        public async Task Update(int id, UserCreateDto userDto)
        {
            var targetUser=await _context.Users.FindAsync(id);

            if(targetUser != null)
            {
                targetUser.Name = userDto.Name;
                targetUser.Surname = userDto.Surname;
                targetUser.Email = userDto.Email;

                if (!string.IsNullOrWhiteSpace(userDto.Password))
                {
                    targetUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                }

                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var targetUser = await _context.Users.FindAsync(id);

            if(targetUser != null)
            {
                _context.Users.Remove(targetUser);
                await  _context.SaveChangesAsync();
            }

        }
        public async Task<string> Login(UserLoginDto logindto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == logindto.Email);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }
            bool isPasswordValid=BCrypt.Net.BCrypt.Verify(logindto.Password,user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("şifre hatalı");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler= new JwtSecurityTokenHandler();//token oluşturan nesne
            var keyString = _configuration.GetSection("AppSettings:Token").Value;
            if (string.IsNullOrEmpty(keyString))
            {
                throw new Exception("kritik hata: appsettings.json dosyasında 'AppSettings:Token' bulunamadı! JWT üretilemiyor. ");
            }

            var key = Encoding.ASCII.GetBytes(keyString);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private UserResponseDto MapToDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }
    }
}