using AutoMapper;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;

namespace NotesAPI
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Note, NoteDataDto>();
            CreateMap<User, UserDataDto>();
            CreateMap<NotesGroup, NotesGroupDataDto>();

            CreateMap<Note, NoteDto>()
                .ForMember(dto => dto.NoteData, x => x.MapFrom(x => x))
                .ForMember(dto => dto.UsersData, x => x.MapFrom(x => x.Users))
                .ForMember(dto => dto.NotesGroupsData, x => x.MapFrom(x => x.NotesGroups));

            CreateMap<CreateNoteDto, Note>();


            CreateMap<User, UserDto>()
                .ForMember(dto => dto.UserData, x => x.MapFrom(x => x))
                .ForMember(dto => dto.NotesData, x => x.MapFrom(x => x.Notes))
                .ForMember(dto => dto.NotesGroupsData, x => x.MapFrom(x => x.NotesGroups));

            CreateMap<CreateUserDto, User>();


            CreateMap<NotesGroup, NotesGroupDto>()
                .ForMember(dto => dto.NotesGroupData, x => x.MapFrom(x => x))
                .ForMember(dto => dto.NotesData, x => x.MapFrom(x => x.Notes))
                .ForMember(dto => dto.UsersData, x => x.MapFrom(x => x.Users));

            CreateMap<CreateNotesGroupDto, NotesGroup>();
        }
    }
}
