using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Stores;
using Domain.AppService;
using Domain.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using DemoAngular.Utils;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAngular.Controllers
{
    [Route("api/[controller]")]
    public class BitlyController : Controller
    {
		private static readonly Regex _urlMatchRegex = new Regex(@"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*");

		private readonly AppServiceFactory _appServiceFactory;

		public BitlyController(AppServiceFactory appServiceFactory)
		{
			if (appServiceFactory == null)
				throw new ArgumentNullException(@"appServiceFactory");

			_appServiceFactory = appServiceFactory;
		}

        // GET: api/values
        [HttpGet]
		[Authorize]
		public async Task<IEnumerable<Link>> Get()
        {
			var bitlyAppService = _appServiceFactory.GetBitlyAppService();
			var links = await bitlyAppService.GetLinksAsync(HttpContext.GetUserId());
			return links;
        }

        // POST api/values
        [HttpPost]
		[Authorize]
        public async Task<IActionResult> Post([FromBody]string url)
        {
			if (url == null || !_urlMatchRegex.IsMatch(url))
				return BadRequest("Сокращаемая строка не соответствует формату URL!");

			var bitlyAppService = _appServiceFactory.GetBitlyAppService();
			var shortenUrl = await bitlyAppService.ShortenLinkAsync(url, HttpContext.GetUserId());
			var fullUrl = String.Concat( HttpContext.Request.Scheme, "://", HttpContext.Request.Host, "/", shortenUrl);
			return Ok(fullUrl);
        }
    }
}
