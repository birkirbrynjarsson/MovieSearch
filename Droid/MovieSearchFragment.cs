using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MovieSearch.Services;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearch.Droid
{
    [Activity(Label = "Movie Search", Theme = "@style/MyTheme")]
    public class MovieSearchFragment : Fragment
    {
        private IMovieService _movieService;

        public MovieSearchFragment(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.MovieSearch, container, false);

            var searchButton = view.FindViewById<Button>(Resource.Id.searchButton);
            var searchInput = view.FindViewById<TextView>(Resource.Id.searchInput);

            searchButton.Click += async (object sender, EventArgs e) =>
            {
                await _movieService.GetMoviesByTitle(searchInput.Text);
                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieService.GetMovieList()));
                this.StartActivity(intent);
            };

            return view;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}
