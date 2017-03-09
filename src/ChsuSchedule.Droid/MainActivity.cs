using System;
using System.Net;

using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ChsuSchedule.Droid
{
	[Activity(Label = "Расписание ЧГУ Beta", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);


			LoadApplication(new App());
		}
	}
}

