using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace SunnyXamarin.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "com.googleusercontent.apps.573525883733-iqsdcu2d5ho8sr1movlk9bmtl8r5m4k0" },
        DataPath = "/oauth2redirect")]
    class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var uri = new Uri(Intent.Data.ToString());

            SunnyXamarin.AuthenticationState.Authenticator.OnPageLoading(uri);

            SunnyXamarin.AuthenticationState.GoogleSignInEventCompletion += OnGoogleSignInComplete;

        }

        protected void OnGoogleSignInComplete(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            Finish();
        }
    }
}