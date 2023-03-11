namespace NotesAPI.Models.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;

      //  public int CreatorId { get; set; }
      //  public virtual User Creator { get; set; }

        public List<User> Users { get; set; }

        public List<NotesGroup> NotesGroups { get; set; }






    }
}
