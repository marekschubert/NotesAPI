using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
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
        public ActionResult<Note> GetAll() 
        {
            var notes = _noteService.GetAll();
            return Ok(notes);
        }


    }
}
