using NotesAPI.Models.Entities;

namespace NotesAPI.Repository.Interfaces
{
    public interface INoteService
    {
        IEnumerable<Note> GetAll();
    }
}
