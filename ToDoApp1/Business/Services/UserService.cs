using Microsoft.EntityFrameworkCore;
using ToDoApp1.Business.Dtos.User;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Data;
using ToDoApp1.Models;

namespace ToDoApp1.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)//constructor
        {
            _context = context;
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
            var newUser = new User
            {
                Name = userDto.Name,
                Surname=userDto.Surname,   
                Email=userDto.Email,
                Password=userDto.Password
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
                targetUser.Password = userDto.Password;

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