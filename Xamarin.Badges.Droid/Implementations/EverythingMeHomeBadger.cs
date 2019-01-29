using Xamarin.Badges.Droid.Implementations.Base;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class EverythingMeHomeBadger : BaseContentManagerBadger
	{
		protected override string ContentUri => "content://me.everything.badger/apps";
		protected override string ColumnPackageName => "package_name";
		protected override string ColumnActivityName => "activity_name";
		protected override string ColumnCount => "count";

		public override string[] SupportedLaunchers => new string[]
		{
			"me.everything.launcher"
		};
	}
}