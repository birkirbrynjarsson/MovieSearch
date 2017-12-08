
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieSearch.Services;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieDb", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MyTheme.Splash")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //MovieSearchActivity._service = new MovieService();
            //this.StartActivity(typeof(MovieSearchActivity));

            MainActivity._movieService = new MovieService();
            this.StartActivity(typeof(MainActivity));

            this.Finish();
        }
    }
}
