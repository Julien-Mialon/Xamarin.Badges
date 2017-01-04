using UIKit;

namespace Xamarin.Badges.iOS
{
	public class BadgeService : IBadgeService
	{
		public void Clear()
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		public void Set(int badgeCount)
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeCount;
		}
	}
}
