using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesAPI.Exceptions;
using NotesAPI.Models;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto.LoginDto;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesAPI.Repository.Implementations
{
    public class UserService : IUserService
    {
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(MainDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public int AddUser(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;

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

        public string LoginUser(UserLoginDto dto)
        {
            var person = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (person != null)
            {
                var isValidPassword = _passwordHasher.VerifyHashedPassword(person, person.PasswordHash, dto.Password);

                if (isValidPassword == PasswordVerificationResult.Success)
                {
                    return GenerateJwt(dto);
                }
            }
            
            //return -1; 
            throw new InvalidLoginAttempt("Invalid username or password");
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
            _dbContext.SaveChanges();
            return true;
        }

        public string GenerateJwt(UserLoginDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new InvalidLoginAttempt("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new InvalidLoginAttempt("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
