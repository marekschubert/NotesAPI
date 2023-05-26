using NotesAPI.Models.Enums;

namespace NotesAPI.Models.Dto.CreationDto
{
    public class CreateNotesGroupDto
    {
        public string Name { get; set; }
        public GroupType GroupType { get; set; }
        public int UserId { get; set; }
    }
}
