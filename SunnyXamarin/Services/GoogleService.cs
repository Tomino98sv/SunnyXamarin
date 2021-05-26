using Newtonsoft.Json;
using SunnyXamarin.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace SunnyXamarin
{
    public class GoogleService : IGoogleService
    {
        private Account _account;
        private AccountStore _store;
        private UserObj _user;

        public void CreateAccountStore()
        {
            _store = AccountStore.Create();
        }

        public AccountStore GetAccountStore() 
        {
            if (_store == null)
            {
                _store = AccountStore.Create();
            }
            return _store;
        }

        public void GoogleSignIn()
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

            _account = _store.FindAccountsForService(Constants.AppName).FirstOrDefault();

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


        public void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }



        public async void OnAuthCompleted(Object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            _user = null;
            if (e.IsAuthenticated)
            {
                _user = await GetUserInfo_Request(e);

                if (_account != null)
                {
                    _store.Delete(_account, Constants.AppName);
                }

                await _store.SaveAsync(_account = e.Account, Constants.AppName);

                SaveUserProperties();

                Application.Current.MainPage.Navigation.PushAsync(new NavTabs(), true);
                AuthenticationState authentication = new AuthenticationState();
                authentication.NotifyComplete();

            }
        }

        private async Task<UserObj> GetUserInfo_Request(AuthenticatorCompletedEventArgs e)
        {
            UserObj user = null;

            var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
            var response = await request.GetResponseAsync();
            if (response != null)
            {
                string userJson = await response.GetResponseTextAsync();
                user = JsonConvert.DeserializeObject<UserObj>(userJson);
                return user;
            }

            return user;
        }

        private void SaveUserProperties()
        {
            Application.Current.Properties.Remove("Id");
            Application.Current.Properties.Remove("FirstName");
            Application.Current.Properties.Remove("LastName");
            Application.Current.Properties.Remove("DisplayName");
            Application.Current.Properties.Remove("EmailAddress");
            Application.Current.Properties.Remove("ProfilePicture");

            Application.Current.Properties.Add("Id", _user.Id);
            Application.Current.Properties.Add("FirstName", _user.FirstName);
            Application.Current.Properties.Add("LastName", _user.LastName);
            Application.Current.Properties.Add("DisplayName", _user.Name);
            Application.Current.Properties.Add("EmailAddress", _user.Email);
            Application.Current.Properties.Add("ProfilePicture", _user.Picture);
        }

    }
}
