using NotesAPI.Models.Dto.Data;

namespace NotesAPI.Models.Dto
{
    public class UserDto
    {
        public UserDataDto UsersData { get; set; }
        public List<NotesGroupDataDto> NotesGroupData { get; set; }
        public List<NoteDataDto> NotseData { get; set; }
    }
}
