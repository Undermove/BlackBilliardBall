﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace SimpleWebApp
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<PredictionsManager>();
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = new PathString("/Auth"));
			services.AddAuthorization();
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
				endpoints.MapGet("/auth", async context =>
				{
					string page = File.ReadAllText("site/loginPage.html");
					await context.Response.WriteAsync(page);
				});

				endpoints.MapPost("/login", async context =>
				{
					var credentials = await context.Request.ReadFromJsonAsync<Credentials>();
					await context.Response.WriteAsync(credentials.Login);
				});

				endpoints.MapGet("/adminPage", async context =>
				{
					string page = File.ReadAllText("site/adminPage.html");
					await context.Response.WriteAsync(page);
				}).RequireAuthorization();

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

				endpoints.MapPost("/addPrediction", async context =>
				{
					var pm = app.ApplicationServices.GetService<PredictionsManager>();
					var query = await context.Request.ReadFromJsonAsync<Prediction>();

					pm.AddPrediction(query.PredictionString);
				});

				endpoints.MapGet("/getPredictions", async context =>
				{
					var pm = app.ApplicationServices.GetService<PredictionsManager>();

					await context.Response.WriteAsJsonAsync(pm.GetAllPredictions());
				});

				endpoints.MapDelete("/deletePrediction", async context =>
				{
					var pm = app.ApplicationServices.GetService<PredictionsManager>();
					var predictionNumber = await context.Request.ReadFromJsonAsync<int>();
					pm.DeletePrediction(predictionNumber);
				});

				endpoints.MapPut("/updatePrediction", async context =>
				{
					var pm = app.ApplicationServices.GetService<PredictionsManager>();
					var predictionUpdate = await context.Request.ReadFromJsonAsync<PredictionUpdateRequest>();
					pm.UpdatePrediction(predictionUpdate);
				});
			});
		}
	}
}
