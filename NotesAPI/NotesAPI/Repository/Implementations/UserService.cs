using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Repository.Implementations
{
    public class UserService : IUserService
    {
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(MainDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int AddUser(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public bool DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(n => n.Id == id);
            if (user is null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users
                .Include(u => u.Notes)
                .Include(u => u.NotesGroups);

            var usersDtos = _mapper.Map<List<UserDto>>(users);

            return usersDtos;
        }

        public UserDto GetUserById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Notes)
                .Include(u => u.NotesGroups)
                .FirstOrDefault(n => n.Id == id);

            var result = user == null ? null : _mapper.Map<UserDto>(user);

            return result;
        }

        public bool UpdateUser(int id, UserDataDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(n => n.Id == id);
            if (user == null)
            {
                return false; // throw new NotFoundException("User not found");
            }
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Password = dto.Password;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
