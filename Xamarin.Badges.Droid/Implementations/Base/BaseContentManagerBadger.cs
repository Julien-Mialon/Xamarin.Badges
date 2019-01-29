using Android.Content;
using Android.Net;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations.Base
{
	internal abstract class BaseContentManagerBadger : IBadger
	{
		public virtual bool Set(Context context, ComponentName component, int badgeCount)
		{
			ContentValues contentValues = new ContentValues();
			Put(contentValues, ColumnPackageName, component.PackageName);
			Put(contentValues, ColumnActivityName, component.ClassName);
			Put(contentValues, ColumnCount, badgeCount);
			PutToContent(contentValues, context, component, badgeCount);

			context.ContentResolver.Insert(Uri.Parse(ContentUri), contentValues);
			return true; //we hope at least
		}

		protected virtual void PutToContent(ContentValues contentValues, Context context, ComponentName component, int badgeCount)
		{
		}

		protected void Put(ContentValues contentValues, string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				return;
			}

			contentValues.Put(key, value);
		}

		protected void Put(ContentValues contentValues, string key, int value)
		{
			if (string.IsNullOrEmpty(key))
			{
				return;
			}

			contentValues.Put(key, value);
		}

		protected abstract string ContentUri { get; }
		protected abstract string ColumnPackageName { get; }
		protected abstract string ColumnActivityName { get; }
		protected abstract string ColumnCount { get; }

		public abstract string[] SupportedLaunchers { get; }
	}
}