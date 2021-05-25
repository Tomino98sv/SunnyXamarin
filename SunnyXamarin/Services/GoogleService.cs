using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

namespace SunnyXamarin
{
    public class GoogleService
    {
        static Account account;
        public static AccountStore store;

        public static void CreateAccountStore() 
        {
            store = AccountStore.Create();
        }

        public static void GoogleSignIn()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;
                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl),
                null,
                true
                );

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
            
        }


        public static void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }



        public static async void OnAuthCompleted(Object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            UserObj user = null;
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<UserObj>(userJson);
                }

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }

                await store.SaveAsync(account = e.Account, Constants.AppName);

                Application.Current.Properties.Remove("Id");
                Application.Current.Properties.Remove("FirstName");
                Application.Current.Properties.Remove("LastName");
                Application.Current.Properties.Remove("DisplayName");
                Application.Current.Properties.Remove("EmailAddress");
                Application.Current.Properties.Remove("ProfilePicture");

                Application.Current.Properties.Add("Id", user.Id);
                Application.Current.Properties.Add("FirstName", user.FirstName);
                Application.Current.Properties.Add("LastName", user.LastName);
                Application.Current.Properties.Add("DisplayName", user.Name);
                Application.Current.Properties.Add("EmailAddress", user.Email);
                Application.Current.Properties.Add("ProfilePicture", user.Picture);

                AuthenticationState authentication = new AuthenticationState();
                authentication.notifyComplete();

                Application.Current.MainPage.Navigation.PushAsync(new TabbedPage1(), true);
            }
        }

    }
}
