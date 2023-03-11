using NotesAPI.Models.Enums;

namespace NotesAPI.Models.Entities
{
    public class NotesGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupType GroupType { get; set; } = GroupType.None;


        public List<User> Users { get; set; }

        public List<Note> Notes { get; set; }



    }
}
