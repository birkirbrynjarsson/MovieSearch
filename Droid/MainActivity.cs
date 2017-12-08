using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MovieSearch.Models;
using MovieSearch.Services;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearch.Droid
{
    [Activity(Label = "The Movie db", Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
        public static IMovieService _movieService { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            // SetContentView(Resource.Layout.MovieSearch);
            SetContentView(Resource.Layout.MainLayout);
            var toplistFragment = new MovieTopListFragment(_movieService);

            var fragments = new Fragment[]
            {
                new MovieSearchFragment(_movieService),
                toplistFragment
            };

            var navTitles = CharSequence.ArrayFromStringArray(new[] { "Search", "Top Movies" });
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new NavFragmentPagerAdapter(SupportFragmentManager, fragments, navTitles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            tabLayout.TabSelected += async (sender, args) =>
            {
                if (args.Tab.Position == 1)
                {
                    await toplistFragment.GetTopMovies();
                }
            };

            // Get our button from the layout resource,
            // and attach an event to it

            //Button searchButton = FindViewById<Button>(Resource.Id.searchButton);
            //TextView searchInput = FindViewById<TextView>(Resource.Id.searchInput);

            //searchButton.Click += async (object sender, EventArgs e) =>
            //{
            //    await _movieService.GetMoviesByTitle(searchInput.Text);
            //    var intent = new Intent(this, typeof(MovieListActivity));
            //    intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieService.GetMovieList()));
            //    this.StartActivity(intent);
            //};

        }
    }
}

