using Domain.AppService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoAngular.Controllers
{

	public class ShortLinkRedirectorController : Controller
	{
		private readonly AppServiceFactory _appServiceFactory;

		public ShortLinkRedirectorController(AppServiceFactory appServiceFactory)
		{
			if (appServiceFactory == null)
				throw new ArgumentNullException(@"appServiceFactory");

			_appServiceFactory = appServiceFactory;
		}

		public async Task<IActionResult> Index(string id)
		{
			var bitlyAppService = _appServiceFactory.GetBitlyAppService();
			var redirectionUrl = await bitlyAppService.GetRedirectionAsync(id);
			return Redirect(redirectionUrl);
		}
	}
}
