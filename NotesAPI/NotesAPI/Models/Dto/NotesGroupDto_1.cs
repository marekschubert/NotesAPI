using NotesAPI.Models.Dto.Data;

namespace NotesAPI.Models.Dto
{
    public class NotesGroupDto
    {
        public NoteDataDto NoteData { get; set; }
        public List<UserDataDto> UsersData { get; set; }
        public List<NotesGroupDataDto> NotesGroupsData { get; set; }
    }
}
