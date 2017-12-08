
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
using MovieSearch.Services;

namespace MovieSearch.Droid
{
    public class MovieListAdapter : BaseAdapter<MovieListViewModel>
    {
        private static readonly string _imageDbUrl = "http://image.tmdb.org/t/p/original";
        private readonly Activity _context;
        private readonly List<MovieListViewModel> _movies;
        private IMovieService _movieService;

        public MovieListAdapter(Activity context, List<MovieListViewModel> movies, IMovieService movieService)
        {
            this._context = context;
            this._movies = movies;
            this._movieService = movieService;
            GetMovieCast(_movies);
        }

        public override MovieListViewModel this[int position] => this._movies[position];

        public override int Count => this._movies.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView; // re--use an existing view, if one is available

            if (view == null)
            {
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
            }

            var movie = this._movies[position];
            view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title;
            view.FindViewById<TextView>(Resource.Id.releaseYear).Text = "(" + movie.ReleaseYear.ToString() + ")";
            view.FindViewById<RatingBar>(Resource.Id.ratingBar).Rating = (float)movie.Rating;
            view.FindViewById<TextView>(Resource.Id.ratingValue).Text = movie.Rating.ToString();
            var actorsView = view.FindViewById<TextView>(Resource.Id.actors);
            if(actorsView.Text == "" || actorsView.Text == null){
                for (int i = 0; i < movie.Actors.Count; i++)
                {
                    actorsView.Text += movie.Actors[i];
                    if (i != movie.Actors.Count - 1)
                    {
                        actorsView.Text += ", ";
                    }
                }   
            }

            var imageResource = view.FindViewById<ImageView>(Resource.Id.poster);
            if ((movie.RemoteImageUrl != "" || movie.RemoteImageUrl != null))
            {
                Glide.With(this._context).Load(_imageDbUrl + movie.RemoteImageUrl).Into(imageResource);
            }

            return view;
        }

        private async void GetMovieCast(List<MovieListViewModel> movies)
        {
            await _movieService.GetCastInList(movies);
            NotifyDataSetChanged();
        }
    }
}
