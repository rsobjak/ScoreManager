using Android.App;
using Android.Content;
using Android.Content.PM;

namespace ScoreManagerApp;

[Activity(Exported = true, NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = CALLBACK_SCHEME)]
public class ScoreManagerWebAuthenticatorCallbackActivity : WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "scoremanager";
}