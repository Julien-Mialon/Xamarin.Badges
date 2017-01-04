using Android.Content;

namespace Xamarin.Badges.Droid
{
	public class BadgeService : IBadgeService
	{
		private readonly Context _context;

		public BadgeService(Context context)
		{
			_context = context;
		}

		public void Clear()
		{
			Badger.ApplyBadgeCount(_context, 0);
		}

		public void Set(int badgeCount)
		{
			Badger.ApplyBadgeCount(_context, badgeCount);
		}
	}
}