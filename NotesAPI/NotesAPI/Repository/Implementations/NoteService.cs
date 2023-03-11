using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Repository.Implementations
{
    public class NoteService : INoteService
    {
        private readonly MainDbContext _dbContext;

        public NoteService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IEnumerable<Note> GetAll()
        {
            var notes = _dbContext.Notes.Include(n => n.Users);
            return notes;
        }
    }
}
