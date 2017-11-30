using System;
using DM.MovieApi;

namespace MovieSearch
{
    public class MovieDbSettings : IMovieDbSettings
    {
        public string ApiKey => "080e52331bcbfcde76da15d48c47ee6e";

        public string ApiUrl => "http://api.themoviedb.org/3/";
    }
}
