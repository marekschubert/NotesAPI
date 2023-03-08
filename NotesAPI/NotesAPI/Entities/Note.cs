namespace NotesAPI.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreationDate { get; set; }

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public int NotesGroupId { get; set; }
        public virtual NotesGroup NotesGroup { get; set; }






    }
}
