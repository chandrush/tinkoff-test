using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DemoAngular.Middleware;
using System;

namespace DemoAngular
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
		}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
					name: "default",
					template: "{controller=Home}/{action=Index}");
			});
        }
    }
}
