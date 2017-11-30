using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSearch.Models;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.Services
{
    public class MovieService : IMovieService
    {
        private IApiMovieRequest _movieDbApi;
        private List<MovieListViewModel> _movies;

        public MovieService()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            _movieDbApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            _movies = new List<MovieListViewModel>();
        }

        public async Task<List<MovieListViewModel>> GetMoviesByTitle(string title)
        {
            ApiSearchResponse<MovieInfo> response = await _movieDbApi.SearchByTitleAsync(title);
            if (_movies == null)
            {
                _movies.Clear();
            }
            else
            {
                _movies = new List<MovieListViewModel>();
            }

            foreach (MovieInfo movie in response.Results)
            {
                _movies.Add(new MovieListViewModel
                {
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = ""
                });
            }
            return _movies;
        }

        public List<MovieListViewModel> GetMovieList()
        {
            return _movies;
        }
    }
}
