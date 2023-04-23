using AutoMapper;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;

namespace NotesAPI
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Note, NoteDataDto>()
                .ForMember(dto => dto.Id, x => x.MapFrom(x => x.Id))
                .ForMember(dto => dto.Title, x => x.MapFrom(x => x.Title))
                .ForMember(dto => dto.Text, x => x.MapFrom(x => x.Text))
                .ForMember(dto => dto.IsPublic, x => x.MapFrom(x => x.IsPublic))
                .ForMember(dto => dto.CreationDate, x => x.MapFrom(x => x.CreationDate));
            
            CreateMap<User, UserDataDto>()
                .ForMember(dto => dto.Id, x => x.MapFrom(x => x.Id))
                .ForMember(dto => dto.FirstName, x => x.MapFrom(x => x.FirstName))
                .ForMember(dto => dto.LastName, x => x.MapFrom(x => x.LastName))
                .ForMember(dto => dto.Email, x => x.MapFrom(x => x.Email))
                .ForMember(dto => dto.Password, x => x.MapFrom(x => x.Password));

            CreateMap<NotesGroup, NotesGroupDataDto>()
                .ForMember(dto => dto.Id, x => x.MapFrom(x => x.Id))
                .ForMember(dto => dto.Name, x => x.MapFrom(x => x.Name))
                .ForMember(dto => dto.GroupType, x => x.MapFrom(x => x.GroupType));




            CreateMap<User, UserDataDto>();


        }



    }
}
