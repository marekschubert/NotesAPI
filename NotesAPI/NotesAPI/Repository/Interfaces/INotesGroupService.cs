using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto;

namespace NotesAPI.Repository.Interfaces
{
    public interface INotesGroupService
    {
        IEnumerable<NotesGroupDto> GetAllNotesGroups();

        NotesGroupDto GetNotesGroupById(int noteId);
        int AddNotesGroup(CreateNotesGroupDto dto);
        bool UpdateNotesGroup(int id, NotesGroupDataDto dto);
        bool DeleteNotesGroup(int noteId);

    }
}
