using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.Data;

namespace NotesAPI.Repository.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        bool UpdateUser(UserDataDto user);
        bool DeleteUser(int id);
    }
}
