using System;
using System.Collections.Generic;
using System.Threading;
using MovieDownload;
using MovieSearch.Models;
using MovieSearch.Services;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieListController : UITableViewController
    {
        private List<MovieListViewModel> _movies;
        private IMovieService _movieService;
        private ImageDownloader _imageDownloader;
        public MovieListController(IMovieService movieService, ImageDownloader imageDownloader)
        {
            _movieService = movieService;
            _imageDownloader = imageDownloader;
            _movies = _movieService.GetMovieList();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
            NavigationItem.Title = "Search results";
            this.TableView.RowHeight = 70;
            this.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            this.TableView.Source = new MovieListDataSource(this._movies, _onSelectedMovie);
            GetMoviePosters(_movies);
        }

        private void _onSelectedMovie(int row)
        {
            var okAlertController = UIAlertController.Create(
                "Movie selected",
                this._movies[row].Title,
                UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(okAlertController, true, null);
        }

        private async void GetMoviePosters(List<MovieListViewModel> movies)
        {
            await _imageDownloader.DownloadImagesInList(movies);
            this.TableView.ReloadData();  
        }
    }
}
