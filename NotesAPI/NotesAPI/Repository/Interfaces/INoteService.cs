using NotesAPI.Models.Dto;
using NotesAPI.Models.Entities;

namespace NotesAPI.Repository.Interfaces
{
    public interface INoteService
    {
        IEnumerable<NoteDto> GetAll();
    }
}
