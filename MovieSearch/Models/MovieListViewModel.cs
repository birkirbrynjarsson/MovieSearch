using System;
using System.Collections.Generic;
namespace MovieSearch.Models
{
    public class MovieListViewModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseYear { get; set; }
        public String RemoteImageUrl { get; set; }
        public String LocalImageUrl { get; set; }
        public List<String> Actors { get; set; }
        public double Rating { get; set; }
    }
}
