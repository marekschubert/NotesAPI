using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;

namespace NotesAPI.Repository.Interfaces
{
    public interface INoteService
    {
        IEnumerable<NoteDto> GetAllNotes();

        IEnumerable<UserDataDto> GetNoteUsersDataByNoteId(int noteId);
    }
}
