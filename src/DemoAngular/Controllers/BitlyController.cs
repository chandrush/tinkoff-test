using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Stores;
using Domain.AppService;
using Domain.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAngular.Controllers
{
    [Route("api/[controller]")]
    public class BitlyController : Controller
    {
		private readonly AppServiceFactory _appServiceFactory;

		public BitlyController(AppServiceFactory appServiceFactory)
		{
			if (appServiceFactory == null)
				throw new ArgumentNullException(@"appServiceFactory");

			_appServiceFactory = appServiceFactory;
		}

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Link>> Get()
        {
			Guid userId;
			if (!Guid.TryParse(ControllerContext.HttpContext.User.Identity.Name, out userId))
				return null; //TODO: сообщение об ошибке аутентификации

			var bitlyAppService = _appServiceFactory.GetBitlyAppService();
			var links = await bitlyAppService.GetLinksAsync(userId);
			return links;
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
			//var bitlyAppService = _appServiceFactory.GetBitlyAppService();
			//bitlyAppService.ShortenLink(value);

			return "shorten url";
        }
    }
}
