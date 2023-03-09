using Microsoft.AspNetCore.Authentication;
using NotesAPI.Models;
using NotesAPI.Models.Entities;

namespace NotesAPI
{
    public class NotesApiSeeder
    {
        private readonly MainDbContext _dbContext;
        public NotesApiSeeder(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Users.Any())
                {
                    var seededUsers = GetUsers();
                    _dbContext.Users.AddRange(seededUsers);
                    _dbContext.SaveChanges();
                }

            }

            _dbContext.SaveChanges();
        }


        private List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "Pass123,./"
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Michael",
                    LastName = "Smith",
                    Email = "michael.smith@gmail.com",
                    Password = "Pass123,./"
                }
            };
            return users;
        }

        private List<Note> GetNotes()
        {
            var notes = new List<Note>()
            {
                new Note()
                {
                    Id = 1,
                    Title = "Note 1",
                    Text = "Text of note 1",
                    CreatorId = 1,

                }
            };
            return notes;
        }

        private void AssignNoteToUser(int userId, int noteId)
        {
           // _dbContext.
        }

    }
}
