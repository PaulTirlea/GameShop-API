using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsersDtoAsync()
        {
            var users = await _usersRepository.GetUsersAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            var userDbo = _mapper.Map<UserDto>(user);
            return userDbo;
        }

        public async Task CreateUserDtoAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _usersRepository.CreateUserAsync(user);
        }

        public async Task UpdateUserDtoAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _usersRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserDtoAsync(int id)
        {
            return await _usersRepository.DeleteUserAsync(id);
        }

        public bool UserExists(int userId)
        {
            return _usersRepository.UserExists(userId);
        }
    }
}
