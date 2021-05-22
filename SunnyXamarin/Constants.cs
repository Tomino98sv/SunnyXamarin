using System;
using System.Collections.Generic;
using System.Text;

namespace SunnyXamarin
{
    class Constants
    {
        public static string AppName = "SunnyXamarin";

        public static string iOSClientId = "573525883733-939tecvabj77msc9083roap02mebbi21.apps.googleusercontent.com";
        public static string AndroidClientId = "573525883733-iqsdcu2d5ho8sr1movlk9bmtl8r5m4k0.apps.googleusercontent.com";

        //Scope - what kind of information trying to pull from Google (foo particular user)
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        public static string iOSRedirectUrl = "com.googleusercontent.apps.573525883733-939tecvabj77msc9083roap02mebbi21:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.573525883733-iqsdcu2d5ho8sr1movlk9bmtl8r5m4k0:/oauth2redirect";

    }
}
