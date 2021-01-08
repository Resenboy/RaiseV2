using System;

using Android.App;
using Android.Content;
using Newtonsoft.Json.Linq;
using Raise.Auth;
using Raise.Utils;
using Raise.Views;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookLogin), typeof(FacebookAuth))]
namespace Raise.Auth
{
    public class FacebookAuth : PageRenderer
    {
        public FacebookAuth(Context context) : base(context)
        {
            var activity = Context as Activity;
            var auth = new OAuth2Authenticator(
                clientId: "211520939455390",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/v3.0/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

            auth.Completed += Auth_Completed;

            activity.StartActivity(auth.GetUI(activity));
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var accessToken = e.Account.Properties["access_token"].ToString();
                var expiresIn = Convert.ToDouble(e.Account.Properties["expires_in"]);
                var espiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                var resquest = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=email,first_name,last_name,gender,picture,birthday,address"), null, e.Account);
                var response = await resquest.GetResponseAsync();
                var obj = JObject.Parse(response.GetResponseText());
                GuidGenerate.E_MAIL = obj["email"].ToString();
                var name = obj["first_name"].ToString() + " " + obj["last_name"].ToString();
                var picture = obj["picture"]["data"]["url"].ToString();

                await AppShell.NavigateToProfile(string.Format("{0}|{1}", name, picture));
            }
        }
    }
}