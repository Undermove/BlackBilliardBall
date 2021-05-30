using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using SimpleWebApp.Repository;
using SimpleWebApp.Repository.Users;
using System.Text;

namespace SimpleWebApp
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IPredictionsRepository, PredictionsDatabaseRepository>();
			services.AddSingleton<PredictionsManager>();
			services.AddSingleton<IUsersRepository, UsersDatabaseRepository>();
			services.AddSingleton<UsersService>();
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = new PathString("/Auth"));
			services.AddAuthorization();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action}");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/auth", async context =>
				{
					string page = File.ReadAllText("site/loginPage.html");
					await context.Response.WriteAsync(page);
				});

				endpoints.MapGet("/register", async context =>
				{
					string page = File.ReadAllText("site/registrationPage.html");
					await context.Response.WriteAsync(page);
				});

				endpoints.MapPost("/login", async context =>
				{
					var credentials = await context.Request.ReadFromJsonAsync<Credentials>();
					// с заданным логином и паролем мы пойдем в базу
					// если в базе есть пользователь, то всё ок, если нет, то ничего не делаем
					var usersService = app.ApplicationServices.GetService<UsersService>();
					var user = usersService.GetUserByLogin(credentials.Login);
					if(credentials.Login == user.Login && credentials.Password == user.Password)
					{
						List<Claim> claims = new List<Claim>()
						{
							new Claim(ClaimsIdentity.DefaultNameClaimType, credentials.Login)
						};
						// создаем объект ClaimsIdentity
						ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

						// добавляем куки нашему пользователю
						await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

						// перенаправляем на нужную сраницу
						context.Response.Redirect("/adminPage");
					}

					await context.Response.WriteAsync(credentials.Login);
				});

				endpoints.MapPost("/registerUser", async context =>
				{
					var request = await context.Request.ReadFromJsonAsync<UserRegistrationRequest>();

					var usersService = app.ApplicationServices.GetService<UsersService>();
					var isSuccess = usersService.TryRegisterUser(request);

				});

				endpoints.MapGet("/", async context =>
				{
					string page = File.ReadAllText("site/predictionsPage.html");
					await context.Response.WriteAsync(page);
				});

				endpoints.MapGet("/answersPage", async context =>
				{
					string page = File.ReadAllText("site/answersPage.html");
					await context.Response.WriteAsync(page);
				});

				endpoints.MapGet("/randomPrediction", async context => 
				{
					var pm = app.ApplicationServices.GetService<PredictionsManager>();
					var s = pm.GetRandomPrediction();
					await context.Response.WriteAsync(s.PredictionString);
				});
			});
		}
	}
}
