using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using MovieSearch.Models;
using MovieSearch.iOS.Views;

namespace MovieSearch.iOS.Controllers
{
    public class MovieListDataSource : UITableViewSource
    {
        private readonly List<MovieListViewModel> _movies;
        public readonly Action<int> _onSelectedMovie;
        public readonly NSString MovieListCellId = new NSString("MovieListCell");

        public MovieListDataSource(List<MovieListViewModel> movies, Action<int> onSelectedMovie)
        {
            this._movies = movies;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MovieCell)tableView.DequeueReusableCell(this.MovieListCellId);
            if(cell == null)
            {
                cell = new MovieCell(this.MovieListCellId);
            }
            var movie = this._movies[indexPath.Row];
            cell.UpdateCell(movie);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movies.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(indexPath.Row);
        }
    }
}
