using NotesAPI.Models.Dto.Data;

namespace NotesAPI.Models.Dto
{
    public class NotesGroupDto
    {
        public NotesGroupDataDto NotesGroupData { get; set; }
        public List<UserDataDto> UsersData { get; set; }
        public List<NoteDataDto> NotesData { get; set; }
    }
}
