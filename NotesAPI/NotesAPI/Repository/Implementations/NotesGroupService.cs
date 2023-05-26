using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Exceptions;
using NotesAPI.Models;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Repository.Implementations
{
    public class NotesGroupService : INotesGroupService
    {
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;
        public NotesGroupService(MainDbContext dbContext, IMapper mapper, ILogger<NoteService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int AddNotesGroup(CreateNotesGroupDto dto)
        {
            _logger.LogInformation($"POST AddNotesGroup invoked");
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == dto.UserId);
            if (user is null)
            {
                return -1;
            }

            var notesGroup = _mapper.Map<NotesGroup>(dto);
            notesGroup.Users = new List<User> { user };
            notesGroup.Notes = new List<Note>();

            _dbContext.NotesGroups.Add(notesGroup);
            _dbContext.SaveChanges();

            return notesGroup.Id;
        }

        public bool DeleteNotesGroup(int notesGroupId)
        {
            _logger.LogWarning($"DELETE DeleteNotesGroup with {notesGroupId} invoked");
            var notesGroup = _dbContext.NotesGroups.FirstOrDefault(n => n.Id == notesGroupId);
            if (notesGroup is null)
            {
                throw new NotFoundException("NotesGroup not found");
            }

            _dbContext.NotesGroups.Remove(notesGroup);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<NotesGroupDto> GetAllNotesGroups()
        {
            _logger.LogInformation($"GET GetAllNotesGroups invoked");
            var notesGroups = _dbContext.NotesGroups
                .Include(n => n.Users)
                .Include(n => n.Notes);

            var notesGroupsDtos = _mapper.Map<List<NotesGroupDto>>(notesGroups);

            return notesGroupsDtos;
        }

        public NotesGroupDto GetNotesGroupById(int notesGroupId)
        {
            _logger.LogInformation($"GET GetNotesGroupById with {notesGroupId} invoked");
            var notesGroup = _dbContext.NotesGroups
                .Include(n => n.Users)
                .Include(n => n.Notes)
                .FirstOrDefault(n => n.Id == notesGroupId);

            var result = notesGroup == null ? null : _mapper.Map<NotesGroupDto>(notesGroup);

            return result;
        }

        public bool UpdateNotesGroup(int id, NotesGroupDataDto dto)
        {
            _logger.LogInformation($"PUT UpdateNotesGroup with {id} invoked");
            var notesGroup = _dbContext.NotesGroups.FirstOrDefault(n => n.Id == id);
            if (notesGroup == null)
            {
                return false; // throw new NotFoundException("NotesGroup not found");
            }
            notesGroup.Name = dto.Name;
            notesGroup.GroupType = dto.GroupType;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
