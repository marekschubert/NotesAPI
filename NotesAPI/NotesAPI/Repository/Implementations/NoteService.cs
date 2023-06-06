using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Exceptions;
using NotesAPI.Models;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;
using RestaurantAPI.Services.Interfaces;

namespace NotesAPI.Repository.Implementations
{
    public class NoteService : INoteService
    {
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;
        private readonly IUserContextService _userContextService;
        public NoteService(MainDbContext dbContext, IMapper mapper, ILogger<NoteService> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public IEnumerable<NoteDto> GetAllNotes()
        {
            _logger.LogInformation($"GET GetAllNotes invoked");
            var notes = _dbContext.Notes
                .Include(n => n.Users)
                .Include(n => n.NotesGroups);

            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        public NoteDto GetNoteById(int noteId)
        {
            _logger.LogInformation($"GET GetNoteById with {noteId} invoked");
            var note = _dbContext.Notes
                .Include(n => n.Users)
                .Include(n => n.NotesGroups)
                .FirstOrDefault(n => n.Id == noteId);

            var result = note == null ? null : _mapper.Map<NoteDto>(note);
            
            return result;
        }
        public bool UpdateNote(int id, NoteDataDto dto)
        {
            _logger.LogInformation($"PUT UpdateNote with {id} invoked");
            var note = _dbContext.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return false; // throw new NotFoundException("Note not found");
            }
            note.Title = dto.Title;
            note.IsPublic = dto.IsPublic;
            note.Text = dto.Text;
            _dbContext.SaveChanges();
            return true;
        }
        public bool DeleteNote(int noteId)
        {
            _logger.LogWarning($"DELETE DeleteNote with {noteId} invoked");
            var note = _dbContext.Notes.FirstOrDefault(n => n.Id == noteId);
            if (note is null)
            {
                throw new NotFoundException("Note not found");
            }

            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();
            return true;
        }

        public int AddNote(CreateNoteDto dto)
        {
            _logger.LogInformation($"POST AddNote invoked");
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == dto.UserId);
            var notesGroup = _dbContext.NotesGroups.FirstOrDefault(u => u.Id == dto.NotesGroupId);
            if (user is null || notesGroup is null)
            {
                return -1;
            }

            var note = _mapper.Map<Note>(dto);
            note.Users = new List<User> { user };
            note.NotesGroups = new List<NotesGroup> { notesGroup };
            
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();

            return note.Id;
        }

        public IEnumerable<NoteDto> GetUserNotes(int userId)
        {
            _logger.LogInformation($"GET GetUserNotes with {userId} invoked");

            var notes = _dbContext.Notes
                .Include(n => n.NotesGroups)
                .Where(n => n.Users.Any(u => u.Id == userId))
                .ToList();

            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        public IEnumerable<NoteDto> GetUsersNotes()
        {
            var userId = _userContextService.GetUserId;
            return GetUserNotes((int)userId);
        }
    }
}
