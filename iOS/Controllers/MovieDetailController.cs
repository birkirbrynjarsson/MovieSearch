using System;
using CoreGraphics;
using MovieSearch.Models;
using MovieSearch.Services;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailController : UIViewController
    {
        private double PADDING;
        private double Y_OFFSET;
        private double TITLE_X;
        private double TITLE_Y;
        private double TITLE_WIDTH;
        private double TITLE_HEIGHT;
        private double SUBTITLE_X;
        private double SUBTITLE_Y;
        private double SUBTITLE_WIDTH;
        private double SUBTITLE_HEIGHT;
        private double IMAGE_X;
        private double IMAGE_Y;
        private double IMAGE_WIDTH;
        private double IMAGE_HEIGHT;
        private double DESCRIPTION_X;
        private double DESCRIPTION_Y;
        private double DESCRIPTION_WIDTH;
        private double DESCRIPTION_HEIGHT;

        private MovieDetailsViewModel _movie;
        private IMovieService _movieService;
        private UILabel _movieTitle;
        private UILabel _movieSubtitle;
        private UIImageView _moviePoster;
        private UITextView _movieDescription;

        public MovieDetailController(MovieDetailsViewModel movie, IMovieService movieService)
        {
            _movie = movie;
            _movieService = movieService;
            GetAdditionalMovieDetails();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
            NavigationItem.Title = _movie.Title;
            initializeSizeConstants();
            InitializeSubViews();
        }

        private async void GetAdditionalMovieDetails()
        {
            var localImagePath = this._movie.LocalImageUrl;
            this._movie = await _movieService.GetMovieById(this._movie.Id);
            this._movie.LocalImageUrl = localImagePath;
            this.ViewDidLoad();
        }

        private void InitializeSubViews()
        {
            _movieTitle = MovieTitle();
            _movieSubtitle = MovieSubtitle();
            _moviePoster = MoviePoster();
            _movieDescription = MovieDescription();
            foreach (UIView subview in this.View.Subviews)
            {
                subview.RemoveFromSuperview();
            }
            this.View.AddSubviews(new UIView[] { _movieTitle, _movieSubtitle, _moviePoster, _movieDescription });
        }

        private void initializeSizeConstants()
        {
            Y_OFFSET = 60;
            PADDING = this.View.Bounds.Width * 0.03;
            TITLE_X = PADDING;
            TITLE_Y = Y_OFFSET + PADDING;
            TITLE_WIDTH = this.View.Bounds.Width - 2 * PADDING;
            TITLE_HEIGHT = 30;
            SUBTITLE_X = PADDING;
            SUBTITLE_Y = TITLE_Y + TITLE_HEIGHT;
            SUBTITLE_WIDTH = TITLE_WIDTH;
            SUBTITLE_HEIGHT = 36;
            IMAGE_X = PADDING;
            IMAGE_Y = SUBTITLE_Y + SUBTITLE_HEIGHT;
            IMAGE_WIDTH = this.View.Bounds.Width * 2 / 5 - IMAGE_X;
            IMAGE_HEIGHT = IMAGE_WIDTH * 3 / 2;
            DESCRIPTION_X = IMAGE_X + IMAGE_WIDTH + PADDING;
            DESCRIPTION_Y = IMAGE_Y;
            DESCRIPTION_WIDTH = this.View.Bounds.Width - DESCRIPTION_X - PADDING;
            DESCRIPTION_HEIGHT = IMAGE_HEIGHT;
        }

        private UILabel MovieTitle()
        {
            var titleLabel = new UILabel()
            {
                Frame = new CGRect(TITLE_X, TITLE_Y, TITLE_WIDTH, TITLE_HEIGHT),
                Text = _movie.Title,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.FromName("BodoniSvtyTwoITCTT-Book", 26f)
            };
            return titleLabel;
        }

        private UILabel MovieSubtitle()
        {
            var text = _movie.ReleaseYear.ToString() + " | ";
            text += _movie.Runtime != 0 ? _movie.Runtime.ToString() + " min" : "";
            var subtitle = new UILabel()
            {
                Frame = new CGRect(SUBTITLE_X, SUBTITLE_Y, SUBTITLE_WIDTH, SUBTITLE_HEIGHT),
                Text = text,
                TextColor = UIColor.Gray,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.FromName("Georgia", 16f)
            };
            return subtitle;
        }

        private UIImageView MoviePoster()
        {
            var moviePoster = new UIImageView()
            {
                Frame = new CGRect(IMAGE_X, IMAGE_Y, IMAGE_WIDTH, IMAGE_HEIGHT),
                Image = UIImage.FromFile(_movie.LocalImageUrl),
                ContentMode = UIViewContentMode.ScaleAspectFit
            };
            return moviePoster;
        }

        private UITextView MovieDescription()
        {
            var description = new UITextView()
            {
                Frame = new CGRect(DESCRIPTION_X, DESCRIPTION_Y, DESCRIPTION_WIDTH, DESCRIPTION_HEIGHT),
                Text = _movie.Description,
                TextContainerInset = UIEdgeInsets.Zero
            };
            return description;
        }
    }
}
