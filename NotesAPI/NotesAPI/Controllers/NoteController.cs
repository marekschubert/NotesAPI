using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Entities;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Controllers
{
    [Route("/api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> GetAllNotes() 
        {
            var notes = _noteService.GetAllNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById([FromRoute] int id)
        {
            var note = _noteService.GetNoteById(id);
            return Ok(note);
        }

        [HttpGet("user/{id}")]
        public ActionResult<IEnumerable<NoteDto>> GetUserNotes([FromRoute] int id)
        {
            var notes = _noteService.GetUserNotes(id);
            return Ok(notes);
        }


        [HttpPost]
        public ActionResult AddNote([FromBody] CreateNoteDto dto)
        {
            var noteId = _noteService.AddNote(dto);
            return Created($"/api/note/{noteId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNote([FromRoute] int id)
        {
            _noteService.DeleteNote(id);
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateNote([FromRoute] int id, [FromBody] NoteDataDto dto)
        {
            _noteService.UpdateNote(id, dto);
            return Ok();
        }

    }
}
