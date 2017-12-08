
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
using Com.Bumptech.Glide;
using MovieSearch.Models;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieDetailActivity", Theme = "@style/MyTheme")]
    public class MovieDetailActivity : Activity
    {
        private readonly string _imageDbUrl = "http://image.tmdb.org/t/p/original";
        private MovieDetailsViewModel _movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var jsonStr = this.Intent.GetStringExtra("movieDetails");
            if (jsonStr != null)
            {
                this._movie = JsonConvert.DeserializeObject<MovieDetailsViewModel>(jsonStr);
            }

            // Create your application here
            SetContentView(Resource.Layout.MovieDetailView);

            // Find UI Objects
            var title = FindViewById<TextView>(Resource.Id.detailsTitle);
            var releaseYear = FindViewById<TextView>(Resource.Id.detailsYear);
            var duration = FindViewById<TextView>(Resource.Id.detailsDuration);
            var genre = FindViewById<TextView>(Resource.Id.detailsGenre);
            var poster = FindViewById<ImageView>(Resource.Id.detailsPoster);
            var info = FindViewById<TextView>(Resource.Id.detailsInfo);
            var rating = FindViewById<RatingBar>(Resource.Id.detailsRatingBar);
            var ratingValue = FindViewById<TextView>(Resource.Id.detailsRatingValue);
            var actors = FindViewById<TextView>(Resource.Id.detailsCast);

            title.Text = _movie.Title;
            releaseYear.Text = _movie.ReleaseYear.ToString();
            duration.Text = _movie.Runtime.ToString() + " min";

            // Genre List
            genre.Text = "";
            for (int i = 0; i < _movie.Genres.Count && i < 3; i++){
                genre.Text += _movie.Genres[i];
                if(i != _movie.Genres.Count -1 && i != 2){
                    genre.Text += ", ";
                }
            }

            // Poster image
            if (_movie.RemoteImageUrl != "" || _movie.RemoteImageUrl != null)
            {
                Glide.With(this).Load(_imageDbUrl + _movie.RemoteImageUrl).Into(poster);
            }

            info.Text = _movie.Description;
            rating.Rating = (float)_movie.Rating;
            ratingValue.Text = _movie.Rating.ToString();

            // Actor List
            actors.Text = "";
            for (int i = 0; i < _movie.Actors.Count && i < 10; i++)
            {
                actors.Text += _movie.Actors[i];
                if (i != _movie.Actors.Count - 1 || i != 9)
                {
                    actors.Text += "\n";
                }
            }
        }
    }
}
