﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAngular.Middleware.SecurityHeadersMiddleware
{
	/// <summary>
	/// An ASP.NET middleware for adding security headers.
	/// </summary>
	public class SecurityHeadersMiddleware
    {
		private readonly RequestDelegate _next;
		private readonly SecurityHeadersPolicy _policy;

		public SecurityHeadersMiddleware(RequestDelegate next, SecurityHeadersPolicy policy)
		{
			if (next == null)
			{
				throw new ArgumentNullException(nameof(next));
			}

			if (next == null)
			{
				throw new ArgumentNullException(nameof(policy));
			}

			_next = next;
			_policy = policy;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var response = context.Response;

			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}

			var headers = response.Headers;

			foreach (var headerValuePair in _policy.SetHeaders)
			{
				headers[headerValuePair.Key] = headerValuePair.Value;
			}

			foreach (var header in _policy.RemoveHeaders)
			{
				headers.Remove(header);
			}

			await _next(context);
		}
	}
}
