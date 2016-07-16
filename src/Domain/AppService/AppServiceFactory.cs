using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.AppService
{
    public class AppServiceFactory
    {
		public AppServiceFactory()
		{

		}

		public BitlyAppService GetBitlyAppService()
		{
			return new BitlyAppService();
		}
    }
}
