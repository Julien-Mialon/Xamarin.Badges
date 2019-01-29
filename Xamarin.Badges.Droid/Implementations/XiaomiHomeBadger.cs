using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using Java.Lang.Reflect;
using Xamarin.Badges.Droid.Helpers;
using Xamarin.Badges.Droid.Interfaces;
using Exception = Java.Lang.Exception;
using Object = Java.Lang.Object;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class XiaomiHomeBadger : IBadger
	{
		public const string INTENT_ACTION = "android.intent.action.APPLICATION_MESSAGE_UPDATE";
		public const string EXTRA_UPDATE_APP_COMPONENT_NAME = "android.intent.extra.update_application_component_name";
		public const string EXTRA_UPDATE_APP_MSG_TEXT = "android.intent.extra.update_application_message_text";
		private ResolveInfo _resolveInfo;

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			try
			{
				try
				{
					Class miuiNotificationClass = Class.ForName("android.app.MiuiNotification");
					Object miuiNotification = miuiNotificationClass.NewInstance();
					Field field = miuiNotification.Class.GetDeclaredField("messageCount");
					field.Accessible = true;

					try
					{
						field.Set(miuiNotification, badgeCount == 0 ? "" : badgeCount.ToString());
					}
					catch (Exception)
					{
						field.Set(miuiNotification, badgeCount);
					}
				}
				catch (Exception)
				{
					Intent intent = new Intent(INTENT_ACTION);
					intent.PutExtra(EXTRA_UPDATE_APP_COMPONENT_NAME, component.PackageName + "/" + component.ClassName);
					intent.PutExtra(EXTRA_UPDATE_APP_MSG_TEXT, badgeCount == 0 ? "" : badgeCount.ToString());

					try
					{
						BroadcastHelper.SendBroadcast(context, intent);
					}
					catch (Exception)
					{
						//ignored
					}
				}

				if (Build.Manufacturer.Equals("Xiaomi", StringComparison.InvariantCultureIgnoreCase))
				{
					if (_resolveInfo == null)
					{
						Intent intent = new Intent(Intent.ActionMain);
						intent.AddCategory(Intent.CategoryHome);
						_resolveInfo = context.PackageManager.ResolveActivity(intent, PackageInfoFlags.MatchDefaultOnly);
					}

					if (_resolveInfo == null)
					{
						return true;
					}

					NotificationManager mNotificationManager = (NotificationManager) context.GetSystemService(Context.NotificationService);
					Notification.Builder builder = new Notification.Builder(context)
						.SetContentTitle("")
						.SetContentText("")
						.SetSmallIcon(_resolveInfo.IconResource);
					Notification notification = builder.Build();
					try
					{
						Field field = notification.Class.GetDeclaredField("extraNotification");
						Object extraNotification = field.Get(notification);
						Method method = extraNotification.Class.GetDeclaredMethod("setMessageCount", Integer.Type);
						method.Invoke(extraNotification, badgeCount);
						mNotificationManager.Notify(0, notification);
					}
					catch (Exception)
					{
						return false;
					}
				}
			}
			catch (Exception)
			{
				//ignored
			}

			return true;
		}

		public string[] SupportedLaunchers => new[]
		{
			"com.miui.miuilite",
			"com.miui.home",
			"com.miui.miuihome",
			"com.miui.miuihome2",
			"com.miui.mihome",
			"com.miui.mihome2",
			"com.i.miui.launcher",
		};
	}
}