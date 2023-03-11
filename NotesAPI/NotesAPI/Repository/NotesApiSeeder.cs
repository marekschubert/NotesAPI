using Microsoft.AspNetCore.Authentication;
using NotesAPI.Models;
using NotesAPI.Models.Entities;

namespace NotesAPI.Repository
{
    public class NotesApiSeeder
    {
        private readonly MainDbContext _dbContext;

        private List<Note> _notes;
        private List<NotesGroup> _notesGroups;
        private List<User> _users;
        public NotesApiSeeder(MainDbContext dbContext)
        {
            _dbContext = dbContext;
            _notes = GetNotes();
            _notesGroups = GetNotesGroups();
            _users = GetUsers();
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Notes.Any())
                {
                    //var seededNotes = GetNotes();
                    _dbContext.Notes.AddRange(_notes);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.NotesGroups.Any())
                {
                    //var seededNotesGroups = GetNotesGroups();
                    _dbContext.NotesGroups.AddRange(_notesGroups);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    //var seededUsers = GetUsers();
                    _dbContext.Users.AddRange(_users);
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
                  //  Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "Pass123,./",
                    Notes = new List<Note>() 
                    {
                        _notes[0], _notes[1]
                    },
                    NotesGroups = new List<NotesGroup>()
                    {
                        _notesGroups[0]
                    }
                },
                new User()
                {
                  //  Id = 2,
                    FirstName = "Michael",
                    LastName = "Smith",
                    Email = "michael.smith@gmail.com",
                    Password = "Pass123,./",
                    Notes = new List<Note>()
                    {
                        _notes[2], _notes[3]
                    },
                    NotesGroups = new List<NotesGroup>()
                    {
                        _notesGroups[1]
                    }
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
                  //  Id = 1,
                    Title = "Note 1",
                    Text = "Text of note 1"
                },
                new Note()
                {
                  //  Id = 2,
                    Title = "Note 2",
                    Text = "Text of note 2"
                },
                new Note()
                {
                  //  Id = 3,
                    Title = "Note 3",
                    Text = "Text of note 3"
                },
                new Note()
                {
                  //  Id = 4,
                    Title = "Note 4",
                    Text = "Text of note 4"
                }
            };
            return notes;
        }

        private List<NotesGroup> GetNotesGroups()
        {
            var notesGroups = new List<NotesGroup>()
            {
                new NotesGroup()
                {
                  //  Id = 1,
                    Name = "All notes",
                    GroupType = Models.Enums.GroupType.None,
                    Notes = new List<Note>()
                    {
                        _notes[0], _notes[1]
                    }
                },

                new NotesGroup()
                {
                  //  Id = 2,
                    Name = "Education notes",
                    GroupType = Models.Enums.GroupType.Education,
                    Notes = new List<Note>()
                    {
                        _notes[2], _notes[3]
                    }
                }
            };
            return notesGroups;
        }
    }
}
