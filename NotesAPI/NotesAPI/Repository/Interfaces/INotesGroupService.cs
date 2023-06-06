using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.ReturnDto;

namespace NotesAPI.Repository.Interfaces
{
    public interface INotesGroupService
    {
        IEnumerable<NotesGroupDto> GetAllNotesGroups();
        IEnumerable<NotesGroupNameSizeDto> GetUsersNotesGroups();
        NotesGroupDto GetNotesGroupById(int noteId);
        int AddNotesGroup(CreateNotesGroupDto dto);
        bool UpdateNotesGroup(int id, NotesGroupDataDto dto);
        bool DeleteNotesGroup(int noteId);

    }
}
