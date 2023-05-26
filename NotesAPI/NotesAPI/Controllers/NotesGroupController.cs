using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Controllers
{
    public class NotesGroupController : ControllerBase
    {

        [Route("/api/note")]
        [ApiController]
        public class NoteController : ControllerBase
        {
            private readonly INotesGroupService _noteService;

            public NoteController(INoteService noteService)
            {
                _noteService = noteService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<NotesGroupDto>> GetAllNotes()
            {
                var notes = _noteService.GetAllNotes();
                return Ok(notes);
            }

            [HttpGet("{id}")]
            public ActionResult<NotesGroupDto> GetNoteById([FromRoute] int id)
            {
                var note = _noteService.GetNoteById(id);
                return Ok(note);
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
}
