using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DemoAngular.Middleware;
using System;
using Microsoft.Extensions.Configuration;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Stores;
using Domain.AppService;
using DemoAngular.Middleware.SecurityHeadersMiddleware;

namespace DemoAngular
{
    public class Startup
    {
		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<BitlyDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b=> b.MigrationsAssembly("DemoAngular"));
			});

			services.AddAntiforgery();

			services.AddMvc();

			AddBusinessServices(services);
		}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			else
			{
				//TODO: app.UseExceptionHandler("/Home/Error");
			}

			app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder()
				.AddDefaultSecurePolicy());

			app.UseStaticFiles();

			//authentication
			var options = new CookieAuthenticationOptions
			{
				CookieName = "tinkoff.test.bitly",
				ExpireTimeSpan = TimeSpan.FromDays(14),
				SlidingExpiration = true
			};
			app.UseCookieAuthentication(options);
			app.UseAutoAuthenticationMiddleware(new AutoAuthenticationOptions { AuthenticationScheme = options.AuthenticationScheme });

			//mvc
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "psevdo_static",
					template: "{History?}",
					defaults: new { controller = "Home", action = "Index" });

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}");
			});
        }

		private void AddBusinessServices(IServiceCollection services)
		{
			services.AddScoped<IBitlyUow, BitlyUow>();
			services.AddScoped(sp => new Func<IBitlyUow>(() => new BitlyUow(sp.GetService<BitlyDbContext>())));

			services.AddTransient<AppServiceFactory>(sp => new AppServiceFactory(sp.GetService<IBitlyUow>()));
		}
	}
}
