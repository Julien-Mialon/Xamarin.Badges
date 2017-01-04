using System;
using Android.Content;
using Android.Runtime;
using Xamarin.Badges.Droid.Helpers;
using Xamarin.Badges.Droid.Interfaces;
using Uri = Android.Net.Uri;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class SonyHomeBadger : IBadger
	{
		private const string INTENT_ACTION = "com.sonyericsson.home.action.UPDATE_BADGE";
		private const string INTENT_EXTRA_PACKAGE_NAME = "com.sonyericsson.home.intent.extra.badge.PACKAGE_NAME";
		private const string INTENT_EXTRA_ACTIVITY_NAME = "com.sonyericsson.home.intent.extra.badge.ACTIVITY_NAME";
		private const string INTENT_EXTRA_MESSAGE = "com.sonyericsson.home.intent.extra.badge.MESSAGE";
		private const string INTENT_EXTRA_SHOW_MESSAGE = "com.sonyericsson.home.intent.extra.badge.SHOW_MESSAGE";
		
		private const string PROVIDER_CONTENT_URI = "content://com.sonymobile.home.resourceprovider/badge";
		private const string PROVIDER_COLUMNS_BADGE_COUNT = "badge_count";
		private const string PROVIDER_COLUMNS_PACKAGE_NAME = "package_name";
		private const string PROVIDER_COLUMNS_ACTIVITY_NAME = "activity_name";
		private const string SONY_HOME_PROVIDER_NAME = "com.sonymobile.home.resourceprovider";
		private static readonly Uri BadgeContentUri = Uri.Parse(PROVIDER_CONTENT_URI);
		private AsyncQueryHandler _queryHandler;

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			//Check if the latest Sony badge content provider exists .
			if (context.PackageManager.ResolveContentProvider(SONY_HOME_PROVIDER_NAME, 0) != null)
			{
				return ExecuteBadgeByContentProvider(context, component, badgeCount);
			}
			return ExecuteBadgeByBroadcast(context, component, badgeCount);
		}

		public string[] SupportedLaunchers => new[]
		{
			"com.sonyericsson.home"
		};

		private static bool ExecuteBadgeByBroadcast(Context context, ComponentName componentName, int badgeCount)
		{
			Intent intent = new Intent(INTENT_ACTION);
			intent.PutExtra(INTENT_EXTRA_PACKAGE_NAME, componentName.PackageName);
			intent.PutExtra(INTENT_EXTRA_ACTIVITY_NAME, componentName.ClassName);
			intent.PutExtra(INTENT_EXTRA_MESSAGE, badgeCount.ToString());
			intent.PutExtra(INTENT_EXTRA_SHOW_MESSAGE, badgeCount > 0);
			return BroadcastHelper.SendBroadcast(context, intent);
		}

		/**
		 * Send request to Sony badge content provider to set badge in Sony home launcher.
		 *
		 * @param context       the context to use
		 * @param componentName the componentName to use
		 * @param badgeCount    the badge count
		 */
		private bool ExecuteBadgeByContentProvider(Context context, ComponentName componentName,
												   int badgeCount)
		{
			if (badgeCount < 0)
			{
				return false;
			}

			/*
			 * Insert a badge associated with the specified package and activity names
			 * asynchronously. The package and activity names must correspond to an
			 * activity that holds an intent filter with action
			 * "android.intent.action.MAIN" and category
			 * "android.intent.category.LAUNCHER" in the manifest. Also, it is not
			 * allowed to publish badges on behalf of another client, so the package and
			 * activity names must belong to the process from which the insert is made.
			 * To be able to insert badges, the app must have the PROVIDER_INSERT_BADGE
			 * permission in the manifest file. In case these conditions are not
			 * fulfilled, or any content values are missing, there will be an unhandled
			 * exception on the background thread.
			 */
			ContentValues contentValues = new ContentValues();
			contentValues.Put(PROVIDER_COLUMNS_BADGE_COUNT, badgeCount);
			contentValues.Put(PROVIDER_COLUMNS_PACKAGE_NAME, componentName.PackageName);
			contentValues.Put(PROVIDER_COLUMNS_ACTIVITY_NAME, componentName.ClassName);

			if (_queryHandler == null)
			{
				_queryHandler = new MyAsyncQueryHandler(context.ApplicationContext.ContentResolver);
			}

			// The badge must be inserted on a background thread
			_queryHandler.StartInsert(0, null, BadgeContentUri, contentValues);
			return true;
		}

		private class MyAsyncQueryHandler : AsyncQueryHandler
		{
			public MyAsyncQueryHandler(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
			{
			}

			public MyAsyncQueryHandler(ContentResolver cr) : base(cr)
			{
			}
		}
	}
}