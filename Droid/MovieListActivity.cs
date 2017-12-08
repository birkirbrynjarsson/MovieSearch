using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieSearch.Models;
using MovieSearch.Services;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "Search Results", Theme = "@style/MyTheme")]
    public class MovieListActivity : ListActivity
    {
        private List<MovieListViewModel> _movies;
        private IMovieService _movieService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _movieService = new MovieService();

            // Create your application here
            var jsonStr = this.Intent.GetStringExtra("movieList");
            if (jsonStr != null)
            {
                this._movies = JsonConvert.DeserializeObject<List<MovieListViewModel>>(jsonStr);
            }

            this.ListView.ItemClick += (sender, args) =>
            {
                ShowMovieDetails(args.Position);
            };

            this.ListAdapter = new MovieListAdapter(this, this._movies, this._movieService);
        }

        private async void ShowMovieDetails(int position)
        {
            MovieDetailsViewModel movie = await _movieService.GetMovieById(_movies[position].Id);
            var intent = new Intent(this, typeof(MovieDetailActivity));
            intent.PutExtra("movieDetails", JsonConvert.SerializeObject(movie));
            this.StartActivity(intent);
        }

        private void ShowAlert(int position)
        {
            var movie = this._movies[position];
            var alertBuilder = new AlertDialog.Builder(this);
            alertBuilder.SetTitle("Movie selected");
            alertBuilder.SetMessage(movie.Title);
            alertBuilder.SetCancelable(true);
            alertBuilder.SetPositiveButton("OK", (e, args) => { });
            var dialog = alertBuilder.Create();
            dialog.Show();
        }
    }
}
