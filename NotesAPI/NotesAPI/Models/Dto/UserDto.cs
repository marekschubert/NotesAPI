using NotesAPI.Models.Dto.Data;

namespace NotesAPI.Models.Dto
{
    public class UserDto
    {
        public UserDataDto UserData { get; set; }
        public List<NotesGroupDataDto> NotesGroupsData { get; set; }
        public List<NoteDataDto> NotesData { get; set; }
    }
}
