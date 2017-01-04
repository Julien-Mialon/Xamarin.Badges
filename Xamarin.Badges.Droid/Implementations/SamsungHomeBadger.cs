using System;
using Android.Content;
using Android.Database;
using Android.OS;
using Uri = Android.Net.Uri;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class SamsungHomeBadger : DefaultBadger
	{
		private const string CONTENT_URI = "content://com.sec.badge/apps?notify=true";
		private static readonly string[] ContentProjection = { "_id", "class" };

		public override bool Set(Context context, ComponentName component, int badgeCount)
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1) //new samsung >= 5.1 use default badger system
			{
				//use default badger
				return base.Set(context, component, badgeCount);
			}

			Uri contentUri = Uri.Parse(CONTENT_URI);
			ContentResolver resolver = context.ContentResolver;
			ICursor cursor = null;
			try
			{
				cursor = resolver.Query(contentUri, ContentProjection, "package=?", new[] { component.PackageName }, null);
				if (cursor == null)
				{
					return false;
				}

				string entryActivityName = component.ClassName;
				bool entryActivityExist = false;
				while (cursor.MoveToNext())
				{
					int id = cursor.GetInt(0);
					ContentValues contentValues = GetContentValues(component, badgeCount, false);
					resolver.Update(contentUri, contentValues, "_id=?", new [] { id.ToString() });
					if (entryActivityName.Equals(cursor.GetString(cursor.GetColumnIndex("class"))))
					{
						entryActivityExist = true;
					}
				}

				if (!entryActivityExist)
				{
					ContentValues contentValues = GetContentValues(component, badgeCount, true);
					resolver.Insert(contentUri, contentValues);
				}

				return true;
			}
			catch (Exception)
			{
				//ignored

				return false;
			}
			finally
			{
				if (cursor != null && !cursor.IsClosed)
				{
					cursor.Close();
				}
			}
		}

		private ContentValues GetContentValues(ComponentName component, int badgeCount, bool isInsert)
		{
			ContentValues contentValues = new ContentValues();
			if (isInsert)
			{
				contentValues.Put("package", component.PackageName);
				contentValues.Put("class", component.ClassName);
			}

			contentValues.Put("badgecount", badgeCount);

			return contentValues;
		}

		public override string[] SupportedLaunchers => new[]
		{
			"com.sec.android.app.launcher",
			"com.sec.android.app.twlauncher"
		};
	}
}