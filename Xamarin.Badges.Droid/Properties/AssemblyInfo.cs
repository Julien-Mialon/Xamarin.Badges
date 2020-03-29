using System.Reflection;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Xamarin.Badges")]
[assembly: AssemblyDescription("Access to application icon badge for Xamarin Android")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Ideine")]
[assembly: AssemblyProduct("Xamarin.Badges")]
[assembly: AssemblyCopyright("Copyright © Ideine 2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

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