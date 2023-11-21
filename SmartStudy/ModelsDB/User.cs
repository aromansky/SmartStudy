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
    }
}
