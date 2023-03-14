using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Repository.Implementations
{
    public class NoteService : INoteService
    {
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;

        public NoteService(MainDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public IEnumerable<NoteDto> GetAll()
        {
            var notes = _dbContext.Notes.Include(n => n.Users);

            var notesDto = _mapper.Map<List<NoteDto>>(notes);

            return notesDto;
        }
    }
}
