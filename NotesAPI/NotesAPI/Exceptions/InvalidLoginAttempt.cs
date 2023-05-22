namespace NotesAPI.Exceptions
{
    public class InvalidLoginAttempt : Exception
    {
        public InvalidLoginAttempt(string message) : base(message) { }
    }
}
