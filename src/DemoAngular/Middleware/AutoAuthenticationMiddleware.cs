using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace DemoAngular.Middleware
{
	public class AutoAuthenticationMiddleware
	{
		private readonly RequestDelegate _next;

		private readonly AutoAuthenticationOptions _options;

		public AutoAuthenticationMiddleware(RequestDelegate next, IOptions<AutoAuthenticationOptions> options)
		{
			_next = next;

			if (options == null)
				throw new ArgumentNullException(nameof(options));

			_options = options.Value;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			if (!httpContext.User.Identity.IsAuthenticated)
			{
				var identity = new ClaimsIdentity("AutoAuthentication");
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
				identity.AddClaim(new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()));
				var principal = new ClaimsPrincipal(identity);

				await httpContext.Authentication.SignInAsync(_options.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true});

				httpContext.Response.Redirect(httpContext.Request.Path);
			}
			else
				await _next(httpContext);
		}
	}

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AutoAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAutoAuthenticationMiddleware(this IApplicationBuilder app, AutoAuthenticationOptions options)
        {
			if (app == null)
				throw new ArgumentNullException(nameof(app));
			if (options == null)
				throw new ArgumentNullException(nameof(options));
			
			return app.UseMiddleware<AutoAuthenticationMiddleware>(Options.Create(options));
        }
    }

	public class AutoAuthenticationOptions : IOptions<AutoAuthenticationOptions>
	{
		AutoAuthenticationOptions IOptions<AutoAuthenticationOptions>.Value
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// Схема аутентификации в рамках которой происходит автоматическая аутентификация.
		/// </summary>
		public string AuthenticationScheme { get; set; }
	}
}
