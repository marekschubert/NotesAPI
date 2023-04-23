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
                .ForMember(dto => dto.NotesGroupData, x => x.MapFrom(x => x.NotesGroups));

            CreateMap<CreateNoteDto, Note>();

        }
    }
}
