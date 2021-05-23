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

		public UserRole GetUserRole(string login)
		{
			return FromRepositoryUserRole(repository.GetUserByLogin(login).UserRole);
		}

		public User GetUserByLogin(string login)
		{
			return repository.GetUserByLogin(login);
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

		private UserRole FromRepositoryUserRole(Repository.Users.UserRole role)
		{
			switch (role)
			{
				case Repository.Users.UserRole.User:
					return UserRole.User;
				case Repository.Users.UserRole.Admin:
					return UserRole.Admin;
				default:
					throw new ArgumentException("Invalid user role");
			}
		}
	}
}
