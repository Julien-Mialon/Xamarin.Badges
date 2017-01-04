using Android.Content;
using Xamarin.Badges.Droid.Helpers;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class XiaomiHomeBadger : IBadger
	{
		private const string INTENT_ACTION = "android.intent.action.APPLICATION_MESSAGE_UPDATE";
		private const string EXTRA_UPDATE_APP_COMPONENT_NAME = "android.intent.extra.update_application_component_name";
		private const string EXTRA_UPDATE_APP_MSG_TEXT = "android.intent.extra.update_application_message_text";

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			Intent intent = new Intent(INTENT_ACTION);
			intent.PutExtra(EXTRA_UPDATE_APP_COMPONENT_NAME, component.PackageName + "/" + component.ClassName);
			intent.PutExtra(EXTRA_UPDATE_APP_MSG_TEXT, badgeCount == 0 ? "" : badgeCount.ToString());
			return BroadcastHelper.SendBroadcast(context, intent);
		}

		public string[] SupportedLaunchers => new[]
		{
			"com.miui.miuilite",
			"com.miui.home",
			"com.miui.miuihome",
			"com.miui.miuihome2",
			"com.miui.mihome",
			"com.miui.mihome2"
		};
	}
}