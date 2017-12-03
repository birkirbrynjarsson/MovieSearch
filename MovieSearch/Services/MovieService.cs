using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSearch.Models;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Genres;

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
            _movies = new List<MovieListViewModel>();
            if (response.Results == null)
            {
                return _movies;
            }

            foreach (MovieInfo movie in response.Results)
            {
                _movies.Add(new MovieListViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return _movies;
        }

        public async Task GetCastInList(List<MovieListViewModel> movies)
        {
            foreach(MovieListViewModel movie in movies){
                var credits = await _movieDbApi.GetCreditsAsync(movie.Id);
                if (credits.Item != null)
                {
                    if (movie.Actors == null)
                    {
                        movie.Actors = new List<String>();
                    }
                    else
                    {
                        movie.Actors.Clear();
                    }

                    var count = credits.Item.CastMembers.Count;
                    for (int i = 0; i < 3 && i < count; i++)
                    {
                        movie.Actors.Add(credits.Item.CastMembers[i].Name);
                    }
                }
            }
        }

        public async Task<List<String>> GetCastByMovieId(int id)
        {
            ApiQueryResponse<MovieCredit> response = await _movieDbApi.GetCreditsAsync(id);
            List<String> actors = new List<String>();
            if(response == null){
                return actors;
            }
            foreach(MovieCastMember actor in response.Item.CastMembers)
            {
                actors.Add(actor.Name);
            }
            return actors;
        }

        public List<MovieListViewModel> GetMovieList()
        {
            return _movies;
        }

        public MovieDetailsViewModel GetDetailsFromListView(MovieListViewModel movie)
        {
            MovieDetailsViewModel movieDetails = new MovieDetailsViewModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                ReleaseYear = movie.ReleaseYear,
                LocalImageUrl = movie.LocalImageUrl,
                Actors = movie.Actors,
                Genres = new List<String>(),
                Description = "",
                Rating = movie.Rating,
                Runtime = 0,
                Caption = ""
            };
            return movieDetails;
        }

        public async Task<MovieDetailsViewModel> GetMovieById(int id)
        {
            ApiQueryResponse<Movie> response = await _movieDbApi.FindByIdAsync(id);
            if (response.Item == null)
            {
                return new MovieDetailsViewModel();
            }
            var movie = new MovieDetailsViewModel()
            {
                Id = response.Item.Id,
                Title = response.Item.Title,
                ReleaseDate = response.Item.ReleaseDate,
                ReleaseYear = response.Item.ReleaseDate.Year,
                RemoteImageUrl = response.Item.PosterPath,
                LocalImageUrl = "",
                Genres = new List<String>(),
                Description = response.Item.Overview,
                Rating = response.Item.VoteAverage,
                Runtime = response.Item.Runtime,
                Caption = response.Item.Tagline
            };
            movie.Actors = await GetCastByMovieId(movie.Id);
            foreach (Genre genre in response.Item.Genres)
            {
                movie.Genres.Add(genre.Name);
            }
            return movie;
        }

        public async Task<List<MovieListViewModel>> GetTopRatedMovies()
        {
            ApiSearchResponse<MovieInfo> response = await _movieDbApi.GetTopRatedAsync();
            List<MovieListViewModel> movies = new List<MovieListViewModel>();

            if (response.Results == null)
            {
                return movies;
            }

            foreach (MovieInfo movie in response.Results)
            {
                movies.Add(new MovieListViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return movies;
        }

        public async Task GetTopRatedMovies(List<MovieListViewModel> movieList)
        {
            ApiSearchResponse<MovieInfo> response = await _movieDbApi.GetTopRatedAsync();

            if (response.Results == null)
            {
                return;
            }

            foreach (MovieInfo movie in response.Results)
            {
                movieList.Add(new MovieListViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return;
        }
    }
}
