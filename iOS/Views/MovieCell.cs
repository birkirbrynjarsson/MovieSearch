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
        private const double CELL_PADDING = 5;
        private const double IMAGE_HEIGHT = 60;
        private const double IMAGE_WIDTH = 40;
        private const double IMAGE_TOTAL_WIDTH = 2 * CELL_PADDING + IMAGE_WIDTH;

        private readonly UIImageView _imageView;
        private readonly UILabel _headingLabel;
        private readonly UILabel _subheadingLabel;

        public MovieCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            this._imageView = new UIImageView()
            {
                Frame = new CGRect(CELL_PADDING, CELL_PADDING, IMAGE_WIDTH, IMAGE_HEIGHT)
            };
            this._headingLabel = new UILabel()
            {
                Frame = new CGRect(IMAGE_TOTAL_WIDTH, CELL_PADDING, this.ContentView.Bounds.Width - IMAGE_TOTAL_WIDTH - CELL_PADDING, IMAGE_HEIGHT),
                //Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                //TextColor = UIColor.FromRGB(127,51, 0),
                BackgroundColor = UIColor.Clear
            };
            this._subheadingLabel = new UILabel()
            {
                Frame = new CGRect(100, 25, 100, 20),
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };
            this.ContentView.AddSubviews((new UIView[] {this._imageView, this._headingLabel, this._subheadingLabel}));
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(MovieListViewModel movie)
        {
            this._imageView.Image = UIImage.FromFile(movie.LocalImageUrl);
            this._headingLabel.Text = movie.Title + " (" + movie.ReleaseYear.ToString() + ")";
            //this._subheadingLabel.Text = movie.ReleaseYear.ToString();
        }
    }
}
