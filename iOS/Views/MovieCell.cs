using System;
using CoreGraphics;
using Foundation;
using MovieSearch.Models;
using MovieDownload;
using UIKit;

namespace MovieSearch.iOS.Views
{
    public class MovieCell : UITableViewCell
    {
        private const double CELL_PADDING = 7;
        private const double IMAGE_HEIGHT = 70;
        private const double IMAGE_WIDTH = 50;
        private const double IMAGE_TOTAL_WIDTH = 2 * CELL_PADDING + IMAGE_WIDTH;
        private const double HEADING_SIZE = 20;
        private const double SUBTITLE_Y = 36;
        private const double RATING_Y = 60;

        private readonly UIImageView _imageView;
        private readonly UILabel _headingLabel;
        private readonly UILabel _subheadingLabel;
        private readonly UILabel _star;
        private readonly UILabel _rating;

        public MovieCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            this._imageView = new UIImageView()
            {
                Frame = new CGRect(CELL_PADDING, CELL_PADDING, IMAGE_WIDTH, IMAGE_HEIGHT),
                ContentMode = UIViewContentMode.ScaleAspectFit
            };
            this._headingLabel = new UILabel()
            {
                Frame = new CGRect(IMAGE_TOTAL_WIDTH, CELL_PADDING, this.ContentView.Bounds.Width - IMAGE_TOTAL_WIDTH - CELL_PADDING, 18),
                Font = UIFont.FromName("BodoniSvtyTwoITCTT-Book", 20f),
                BackgroundColor = UIColor.Clear
            }; 
            this._subheadingLabel = new UILabel()
            {
                Frame = new CGRect(IMAGE_TOTAL_WIDTH, SUBTITLE_Y, this.ContentView.Bounds.Width - IMAGE_TOTAL_WIDTH - CELL_PADDING, 12),
                Font = UIFont.FromName("Helvetica", 12f),
                TextColor = UIColor.Gray,
                BackgroundColor = UIColor.Clear
            };
            this._star = new UILabel()
            {
                Frame = new CGRect(IMAGE_TOTAL_WIDTH, RATING_Y, this.ContentView.Bounds.Width - IMAGE_TOTAL_WIDTH - CELL_PADDING, 14),
                Font = UIFont.FromName("Helvetica-Bold", 17f),
                TextColor = UIColor.Yellow,
                BackgroundColor = UIColor.Clear,
                ShadowOffset = new CGSize(4, -4),
                Text = "★ "
            };
            this._rating = new UILabel()
            {
                Frame = new CGRect(IMAGE_TOTAL_WIDTH + 20, RATING_Y + 1, this.ContentView.Bounds.Width - IMAGE_TOTAL_WIDTH - CELL_PADDING, 14),
                Font = UIFont.FromName("Helvetica-Bold", 14f),
                TextColor = UIColor.Gray,
                BackgroundColor = UIColor.Clear
            };
            this.ContentView.AddSubviews((new UIView[] {this._imageView, this._headingLabel, this._subheadingLabel, this._star, this._rating}));
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(MovieListViewModel movie)
        {
            this._imageView.Image = UIImage.FromFile(movie.LocalImageUrl);
            this._headingLabel.Text = movie.Title + " (" + movie.ReleaseYear.ToString() + ")";
            var castList = "";
            for (int i = 0; i < movie.Actors.Count; i++){
                castList += movie.Actors[i];
                if (i != movie.Actors.Count - 1)
                    castList += ", ";
            }
            this._subheadingLabel.Text = castList;
            this._rating.Text = movie.Rating.ToString();
        }
    }
}
