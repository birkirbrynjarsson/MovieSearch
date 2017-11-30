using System;
using System.Collections.Generic;
namespace MovieSearch.Models
{
    public class MovieListViewModel
    {
        public String Title { get; set; }
        public int ReleaseYear { get; set; }
        public String RemoteImageUrl { get; set; }
        public String LocalImageUrl { get; set; }
        public List<String> Actors { get; set; }
    }
}
