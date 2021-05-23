namespace SimpleWebApp.Repository.Users
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}