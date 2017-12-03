using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using MovieDownload;
using MovieSearch.Models;
using MovieSearch.Services;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieTopListController : UITableViewController
    {
        private List<MovieListViewModel> _movies;
        private IMovieService _movieService;
        private ImageDownloader _imageDownloader;
        public bool refreshList;

        public MovieTopListController(IMovieService movieService, ImageDownloader imageDownloader)
        {
            _movieService = movieService;
            _imageDownloader = imageDownloader;
            _movies = new List<MovieListViewModel>();
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
            refreshList = true;
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
            NavigationItem.Title = "Search results";
            this.TableView.RowHeight = 85;
            this.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            this.TableView.Source = new MovieListDataSource(this._movies, _onSelectedMovie);
            if (this._movies == null || this._movies.Count == 0 || refreshList){
                await GetTopMovies(_movies);
                GetMovieCast(_movies);
                GetMoviePosters(_movies);
                refreshList = false;
            }
        }

        private void _onSelectedMovie(int row)
        {
            var details = _movieService.GetDetailsFromListView(this._movies[row]);
            this.NavigationController.PushViewController(new MovieDetailController(details, this._movieService), true);
        }

        private async Task GetTopMovies(List<MovieListViewModel> movies)
        {
            await _movieService.GetTopRatedMovies(this._movies);
            this.TableView.ReloadData();
        }

        private async void GetMoviePosters(List<MovieListViewModel> movies)
        {
            await _imageDownloader.DownloadImagesInList(movies);
            this.TableView.ReloadData();
        }

        private async void GetMovieCast(List<MovieListViewModel> movies)
        {
            await _movieService.GetCastInList(movies);
            this.TableView.ReloadData();
        }
    }
}
