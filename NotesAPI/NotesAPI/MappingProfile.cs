using AutoMapper;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Entities;

namespace NotesAPI
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Note, NoteDto>()
                .ForMember(dto => dto.UsersData, x => x.MapFrom(x => x.Users));
            
            CreateMap<User, UserDataDto>();


        }



    }
}
