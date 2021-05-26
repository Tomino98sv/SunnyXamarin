using System;
using Xamarin.Auth;

namespace SunnyXamarin
{
    public class AuthenticationState
    {
        public static OAuth2Authenticator Authenticator;

        public delegate void GoogleSignInCompletetionHandler(object source, EventArgs args);
        public static event GoogleSignInCompletetionHandler GoogleSignInEventCompletion;

        public void NotifyComplete()
        { 
            OnGoogleSignInDone();
        }

        protected virtual void OnGoogleSignInDone()
        {
            if (GoogleSignInEventCompletion != null) //if there is some subscribers
            {
                GoogleSignInEventCompletion(this, EventArgs.Empty);
            }
        }

    }
}
