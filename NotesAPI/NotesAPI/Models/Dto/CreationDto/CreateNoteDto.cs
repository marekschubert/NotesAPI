namespace NotesAPI.Models.Dto.CreationDto
{
    public class CreateNoteDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; } = false;
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public int NotesGroupId { get; set; }
    }
}
