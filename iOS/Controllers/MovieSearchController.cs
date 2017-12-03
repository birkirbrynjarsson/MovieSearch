using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using MovieSearch.Models;
using MovieSearch.Services;
using MovieDownload;

namespace MovieSearch.iOS.Controllers
{
    public class MovieSearchController : UIViewController
    {
        private const double X_PADDING = 20;
        private const double Y_PADDING = 80;
        private const double LINE_HEIGHT = 60;

        private IMovieService _movieService;
        private ImageDownloader _imageDownloader;
        private List<MovieListViewModel> _movieList;
        private UIActivityIndicatorView _activityIndicator;

        public MovieSearchController(IMovieService movieService, ImageDownloader imageDownloader)
        {
            _movieService = movieService;
            _imageDownloader = imageDownloader;
            _movieList = new List<MovieListViewModel>();
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "Search";
            this.View.BackgroundColor = UIColor.White;


            var searchField = UITextField();
            var searchLabel = SearchLabel();
            var navigateSearchButton = NavigateSearchButton(searchField);
            _activityIndicator = ActivityIndicator();
            this.View.AddSubviews(new UIView[] {searchField, searchLabel, navigateSearchButton, _activityIndicator});
        }

        private UILabel SearchLabel()
        {
            var searchLabel = new UILabel()
            {
                Frame = new CGRect(X_PADDING, Y_PADDING, this.View.Bounds.Width - 2 * X_PADDING, LINE_HEIGHT),
                Text = ""
            };
            return searchLabel;
        }

        private UITextField UITextField()
        {
            var nameField = new UITextField()
            {
                Frame = new CGRect(X_PADDING, Y_PADDING, this.View.Bounds.Width - 2 * X_PADDING, LINE_HEIGHT),
                Placeholder = "Find movies, TV shows",
                BorderStyle = UITextBorderStyle.RoundedRect
            };
            return nameField;
        }

        private UIButton NavigateSearchButton(UITextField searchField)
        {
            var navigateButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigateButton.Frame = new CGRect(X_PADDING, Y_PADDING + LINE_HEIGHT, this.View.Bounds.Width - 2 * X_PADDING, LINE_HEIGHT);
            navigateButton.SetTitle("Search movies", UIControlState.Normal);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                var searchInput = searchField.Text;
                _activityIndicator.StartAnimating();
                searchField.ResignFirstResponder();
                _movieList = await _movieService.GetMoviesByTitle(searchInput);
                _activityIndicator.StopAnimating();
                this.NavigationController.PushViewController(new MovieListController(this._movieService, this._imageDownloader), true);
            };

            return navigateButton;
        }

        private UIActivityIndicatorView ActivityIndicator()
        {
            var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            activityIndicator.Frame = new CGRect(this.View.Bounds.Width / 2, this.View.Bounds.Height / 2, 0, 0);
            return activityIndicator;
        }
    }
}
