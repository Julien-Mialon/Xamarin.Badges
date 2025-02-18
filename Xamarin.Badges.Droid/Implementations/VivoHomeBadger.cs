using Xamarin.Badges.Implementations.Base;

namespace Xamarin.Badges.Implementations;

internal class VivoHomeBadger : BaseIntentBadger
{
	protected override string IntentName => "launcher.action.CHANGE_APPLICATION_NOTIFICATION_NUM";
	protected override string PackageParameterName => "packageName";
	protected override string ClassParameterName => "className";
	protected override string CountParameterName => "notificationNum";

	public override string[] SupportedLaunchers => new[]
	{
		"com.vivo.launcher"
	};
}