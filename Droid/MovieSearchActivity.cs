using Android.App;
using Android.Widget;
using Android.OS;
using System;
using MovieSearch.Models;
using Android.Content;
using Newtonsoft.Json;
using MovieSearch.Services;
using System.Collections.Generic;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme")]
    public class MovieSearchActivity : Activity
    {
        public static IMovieService _service;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MovieSearch);

            // Get our button from the layout resource,
            // and attach an event to it
            Button searchButton = FindViewById<Button>(Resource.Id.searchButton);
            TextView searchInput = FindViewById<TextView>(Resource.Id.searchInput);

            searchButton.Click += async (object sender, EventArgs e) =>
            {
                await _service.GetMoviesByTitle(searchInput.Text);
                var intent = new Intent(this, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_service.GetMovieList()));
                this.StartActivity(intent);
            };
        }
    }
}

