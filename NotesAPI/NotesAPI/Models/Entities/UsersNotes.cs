using NotesAPI.Models.Enums;

namespace NotesAPI.Models.Entities
{
    public class UsersNotes
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Note Note { get; set; }
        public int NoteId { get; set; }

        public UserNoteRole Role { get; set; }
    }
}
