using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace SimpleWebApp.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		UsersService _usersService;

		public AdminController(UsersService usersService)
		{
			_usersService = usersService;
		}

		[Route("adminPage")]
		public ActionResult Index()
		{
			var userLogin = User.Identity.Name;
			var role = _usersService.GetUserRole(userLogin);
			if (role == UserRole.Admin)
			{
				byte[] page = System.IO.File.ReadAllBytes("site/adminPage.html");
				return File(page, "text/html");
			}

			return Unauthorized();
		}
	}
}
