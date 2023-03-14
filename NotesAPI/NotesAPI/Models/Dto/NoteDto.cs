namespace NotesAPI.Models.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public List<UserDataDto> UsersData { get; set; }

    }
}
