using Xamarin.Auth;

namespace SunnyXamarin.Services
{
    interface IGoogleService
    {
        void CreateAccountStore();
        AccountStore GetAccountStore();
        void GoogleSignIn();
    }
}
