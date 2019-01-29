using UIKit;

namespace Xamarin.Badges.iOS
{
	public class BadgeService : IBadgeService
	{
		public void Clear()
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				//do not inline
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			});
		}

		public void Set(int badgeCount)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				//do not inline
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeCount;
			});
		}
	}
}
