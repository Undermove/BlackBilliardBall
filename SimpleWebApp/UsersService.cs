using SimpleWebApp.Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
	public class UsersService
	{
		IUsersRepository repository;

		public UsersService(IUsersRepository repository)
		{
			this.repository = repository;
		}

		public bool TryRegisterUser(UserRegistrationRequest registrationRequest)
		{
			var user = repository.GetUser(registrationRequest.Email);
			if(user != null)
			{
				return false;
			}

			repository.CreateUser(registrationRequest.Login, registrationRequest.Password, registrationRequest.Email, ToRepositoryUserRole(UserRole.User));
			return true;
		}

		private Repository.Users.UserRole ToRepositoryUserRole(UserRole role)
		{
			switch (role)
			{
				case UserRole.User:
					return Repository.Users.UserRole.User;
				case UserRole.Admin:
					return Repository.Users.UserRole.Admin;
				default:
					throw new ArgumentException("Invalid user role");
			}				
		}
	}
}
