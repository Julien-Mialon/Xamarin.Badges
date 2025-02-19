using Android.App;
using Android.Content;

// Android permissions for badge access for each manufacturer / launcher
//for Android
[assembly: UsesPermission("com.android.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.android.launcher.permission.WRITE_SETTINGS")]
[assembly: UsesPermission("com.android.launcher.permission.INSTALL_SHORTCUT")]
[assembly: UsesPermission("com.android.launcher.permission.UNINSTALL_SHORTCUT")]
//for Samsung
[assembly: UsesPermission("com.sec.android.provider.badge.permission.READ")]
[assembly: UsesPermission("com.sec.android.provider.badge.permission.WRITE")]
//for htc
[assembly: UsesPermission("com.htc.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.htc.launcher.permission.UPDATE_SHORTCUT")]
//for sony
[assembly: UsesPermission("com.sonyericsson.home.permission.BROADCAST_BADGE")]
[assembly: UsesPermission("com.sonymobile.home.permission.PROVIDER_INSERT_BADGE")]
//for apex
[assembly: UsesPermission("com.anddoes.launcher.permission.UPDATE_COUNT")]
//for solid
[assembly: UsesPermission("com.majeur.launcher.permission.UPDATE_BADGE")]
//for huawei
[assembly: UsesPermission("com.huawei.android.launcher.permission.CHANGE_BADGE")]
[assembly: UsesPermission("com.huawei.android.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.huawei.android.launcher.permission.WRITE_SETTINGS")]
//for ZUK
[assembly: UsesPermission("android.permission.READ_APP_BADGE")]
//for OPPO
[assembly: UsesPermission("com.oppo.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.oppo.launcher.permission.WRITE_SETTINGS")]
//for EvMe
[assembly: UsesPermission("me.everything.badger.permission.BADGE_COUNT_READ")]
[assembly: UsesPermission("me.everything.badger.permission.BADGE_COUNT_WRITE")]

namespace Xamarin.Badges.Droid;

public class BadgeService(Context context) : IBadgeService
{
	public void Clear()
	{
		Badger.ApplyBadgeCount(context, 0);
	}

	public void Set(int badgeCount)
	{
		Badger.ApplyBadgeCount(context, badgeCount);
	}
}