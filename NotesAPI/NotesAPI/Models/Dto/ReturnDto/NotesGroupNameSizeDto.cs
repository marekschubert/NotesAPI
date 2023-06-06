using NotesAPI.Models.Dto.Data;

namespace NotesAPI.Models.Dto.ReturnDto
{
    public class NotesGroupNameSizeDto
    {
        public NotesGroupDataDto NotesGroupData { get; set; }
        public int Size { get; set; }
    }
}
