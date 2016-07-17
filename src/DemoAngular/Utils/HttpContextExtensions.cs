using Microsoft.AspNetCore.Http;
using System;

namespace DemoAngular.Utils
{
    public static class HttpContextExtensions
    {
		public static Guid GetUserId(this HttpContext httpContext)
		{
			return Guid.Parse(httpContext.User.Identity.Name);
		}
    }
}
