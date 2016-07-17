using Microsoft.AspNetCore.Builder;
using System;

namespace DemoAngular.Middleware.SecurityHeadersMiddleware
{
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
		{
			if (app == null)
			{
				throw new ArgumentNullException(nameof(app));
			}

			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return app.UseMiddleware<SecurityHeadersMiddleware>(builder.Build());
		}
	}
}
