namespace NotesAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public virtual ICollection<Note>? Notes{ get; set; }
        public virtual ICollection<NotesGroup>? NotesGroups { get; set; }



    }
}
