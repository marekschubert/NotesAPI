namespace NotesAPI.Models.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<NotesGroup> NotesGroups { get; set; }






    }
}
