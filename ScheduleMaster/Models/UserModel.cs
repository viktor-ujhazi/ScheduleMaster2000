using System;

namespace ScheduleMaster.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public UserModel() { }
        public UserModel(int id, string email, string name, string password, bool isAdmin)
        {
            ID = id;
            Email = email;
            Name = name;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}
