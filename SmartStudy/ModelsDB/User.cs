namespace SmartStudy.ModelsDB
{
    public class User
    {
        public long user_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public User() { }

        public bool IsTutor() => Role.Equals("Tutor");

        public string IconAdd
        {
            get
            {
#if WINDOWS
                return "user_add_black.png";
#else
                return "user_add_black.svg";
#endif
            }
        }

        public string IconRemove
        {
            get
            {
#if WINDOWS
                return "user_remove_black.png";
#else
                return "user_remove_black.svg";
#endif
            }
        }
    }
}
