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
    }
}
