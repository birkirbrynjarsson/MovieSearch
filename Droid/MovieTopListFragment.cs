
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
using MovieSearch.Models;
using Fragment = Android.Support.V4.App.Fragment;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovieSearch.Droid
{
    public class MovieTopListFragment : Fragment
    {
        private IMovieService _movieService;
        private List<MovieListViewModel> _movies;
        private ListView _listView;

        public MovieTopListFragment(IMovieService movieService)
        {
            this._movieService = movieService;
            if(this._movies == null){
                GetTopMovies();
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.TopList, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            this._listView = view.FindViewById<ListView>(Resource.Id.toplistview);

            this._listView.ItemClick += (sender, args) =>
            {
                ShowMovieDetails(args.Position);
            };

            return view;
        }

        public async Task GetTopMovies(){
            if (_movieService != null){
                this._movies = await _movieService.GetTopRatedMovies();
            }
            this._listView.Adapter = new MovieListAdapter(this.Activity, this._movies, this._movieService);
        }

        public async void ShowMovieDetails(int position)
        {
            MovieDetailsViewModel movie = await _movieService.GetMovieById(_movies[position].Id);
            var intent = new Intent(this.Activity, typeof(MovieDetailActivity));
            intent.PutExtra("movieDetails", JsonConvert.SerializeObject(movie));
            this.StartActivity(intent);
        }
    }
}
