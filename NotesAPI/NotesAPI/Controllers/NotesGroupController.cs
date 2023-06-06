using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models.Dto;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto.ReturnDto;
using NotesAPI.Repository.Implementations;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI.Controllers
{
    [Route("/api/notes_group")]
    [ApiController]
    [Authorize]
    public class NotesGroupController : ControllerBase
    {
        private readonly INotesGroupService _notesGroupService;

        public NotesGroupController(INotesGroupService notesGroupService)
        {
            _notesGroupService = notesGroupService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NotesGroupDto>> GetAllNotesGroups()
        {
            var notesGroups = _notesGroupService.GetAllNotesGroups();
            return Ok(notesGroups);
        }

        [HttpGet("{id}")]
        public ActionResult<NotesGroupDto> GetNotesGroupById([FromRoute] int id)
        {
            var notesGroup = _notesGroupService.GetNotesGroupById(id);
            return Ok(notesGroup);
        }

        [HttpGet("user")]
        public ActionResult<IEnumerable<NotesGroupNameSizeDto>> GetUsersNotesGroups()
        {
            var notesGroup = _notesGroupService.GetUsersNotesGroups();
            return Ok(notesGroup);
        }

        [HttpPost]
        public ActionResult AddNotesGroup([FromBody] CreateNotesGroupDto dto)
        {
            var notesGroupId = _notesGroupService.AddNotesGroup(dto);
            return Created($"/api/notesGroup/{notesGroupId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNotesGroup([FromRoute] int id)
        {
            _notesGroupService.DeleteNotesGroup(id);
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateNote([FromRoute] int id, [FromBody] NotesGroupDataDto dto)
        {
            _notesGroupService.UpdateNotesGroup(id, dto);
            return Ok();
        }        
    }
}
