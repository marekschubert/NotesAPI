using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.Data;
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

        public IEnumerable<NoteDto> GetAllNotes()
        {
            var notes = _dbContext.Notes
                .Include(n => n.Users)
                .Include(n => n.NotesGroups);

            var notesDto = new List<NoteDto>();
            foreach (var note in notes)
            {
                var noteDataDto = _mapper.Map<NoteDataDto>(note);
                var userDataDto = _mapper.Map<List<UserDataDto>>(note.Users);
                var notesGroupDataDto = _mapper.Map<List<NotesGroupDataDto>>(note.NotesGroups);
                notesDto.Add(new NoteDto
                {
                    NoteData = noteDataDto,
                    UsersData = userDataDto,
                    NotesGroupData = notesGroupDataDto
                });
            }
            return notesDto;
        }

        public IEnumerable<UserDataDto> GetNoteUsersDataByNoteId(int noteId)
        {
            var notes = _dbContext.Notes
                .Include(n => n.Users)
                .Include(n => n.NotesGroups);

            //  var notesDto = _mapper.Map<List<NoteDto>>(notes);

            //  return notesDto;
            return null;
        }
    }
}
