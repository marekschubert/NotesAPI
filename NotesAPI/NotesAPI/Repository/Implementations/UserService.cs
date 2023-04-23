using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Repository.Implementations
{
    public class UserService : IUserService
    {
        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserDataDto user)
        {
            throw new NotImplementedException();
        }
    }
}
