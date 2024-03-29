﻿using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;

namespace NotesAPI.Repository.Interfaces
{
    public interface INoteService
    {
        IEnumerable<NoteDto> GetAllNotes();
        IEnumerable<NoteDto> GetUserNotes(int userId);
        IEnumerable<NoteDto> GetUsersNotes();
        NoteDto GetNoteById(int noteId);
        int AddNote(CreateNoteDto dto);
        bool UpdateNote(int id, NoteDataDto dto);
        bool DeleteNote(int noteId);
    }
}
