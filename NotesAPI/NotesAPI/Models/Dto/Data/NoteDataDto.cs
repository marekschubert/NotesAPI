namespace NotesAPI.Models.Dto.Data
{
    public class NoteDataDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
