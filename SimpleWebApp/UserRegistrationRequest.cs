namespace SimpleWebApp
{
	public class UserRegistrationRequest
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}

	public enum UserRole
	{
		User,
		Admin
	}
}
