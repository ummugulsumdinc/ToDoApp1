using ToDoApp1.Business.Dtos.User;

namespace ToDoApp1.Business.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAll();
        Task<UserResponseDto?> GetById(int id);
        Task<UserResponseDto> Add(UserCreateDto userDto);
        Task Update(int id, UserCreateDto userDto);
        Task Delete(int id);
        Task<string> Login(UserLoginDto loginDto);
    }
}
