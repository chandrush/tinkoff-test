using Domain.Stores;

namespace Domain.AppService
{
    public class AppServiceFactory
    {
		private IBitlyUow _biltyUow;

		public AppServiceFactory(IBitlyUow biltyUow)
		{
			_biltyUow = biltyUow;
		}

		public BitlyAppService GetBitlyAppService()
		{
			return new BitlyAppService(_biltyUow);
		}
    }
}
