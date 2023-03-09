using NotesAPI.Models.Enums;

namespace NotesAPI.Models.Entities
{
    public class NotesGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupType GroupType { get; set; } = GroupType.None;

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Note>? Notes { get; set; }



    }
}
