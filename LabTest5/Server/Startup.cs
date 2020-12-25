using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.GraphiQL;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Types;

using LabTest5.Server.Data;
using LabTest5.Server.GraphTypes;
using LabTest5.Server.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace LabTest5.Server
{
	public class Startup
	{
		private readonly IHostEnvironment _hostEnvironment;
		public IConfiguration Configuration { get; }

		public Startup(
			IConfiguration configuration,
			IHostEnvironment hostEnvironment
		)
		{
			Configuration = configuration;
			_hostEnvironment = hostEnvironment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			    options.UseSqlServer(
				   Configuration.GetConnectionString("DefaultConnection")));

			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
			    .AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
			    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

			services.AddAuthentication()
			    .AddIdentityServerJwt();

			services.AddControllersWithViews();
			services.AddRazorPages();

			services
				.AddGraphQLServer()
				.AddQueryType<QueryType>()
				.BindRuntimeType<string, StringType>()
				.BindRuntimeType<Guid, IdType>()
				.ModifyRequestOptions(opt
					=> opt.IncludeExceptionDetails = _hostEnvironment.IsDevelopment()
				 						     || _hostEnvironment.IsStaging() 
				);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env
		)
		{
			var queryPath = "/graph";

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
				app.UseWebAssemblyDebugging();
				var playgroudPath = $"{queryPath}/play";
				var graphiqlPath = $"{queryPath}/gql";
				app.UsePlayground(
					new PlaygroundOptions
					{
						Path = playgroudPath,
						QueryPath = queryPath,
						SubscriptionPath = queryPath
					}
				).UseGraphiQL(
					new GraphiQLOptions
					{
						Path = graphiqlPath,
						QueryPath = queryPath,
						SubscriptionPath = queryPath
					}
				);
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();

			if (env.IsDevelopment() || env.IsStaging())
			{

			}

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGraphQL(queryPath);
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
