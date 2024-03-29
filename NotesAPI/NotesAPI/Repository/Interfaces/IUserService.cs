﻿using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto.LoginDto;

namespace NotesAPI.Repository.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        int AddUser(CreateUserDto dto);
        bool UpdateUser(int id, UserDataDto dto);
        bool DeleteUser(int id);
        string LoginUser(UserLoginDto dto);

    }
}
