using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSearch.Models;

namespace MovieSearch.Services
{
    public interface IMovieService
    {
        Task<List<MovieListViewModel>> GetMoviesByTitle(string title);
        List<MovieListViewModel> GetMovieList();
        Task GetCastInList(List<MovieListViewModel> movies);
        Task<List<String>> GetCastByMovieId(int id);
        MovieDetailsViewModel GetDetailsFromListView(MovieListViewModel movie);
        Task<MovieDetailsViewModel> GetMovieById(int id);
        Task<List<MovieListViewModel>> GetTopRatedMovies();
        Task GetTopRatedMovies(List<MovieListViewModel> movieList);
    }
}
