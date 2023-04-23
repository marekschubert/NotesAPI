using NotesAPI.Models.Enums;

namespace NotesAPI.Models.Dto.Data
{
    public class NotesGroupDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupType GroupType { get; set; }
    }
}
