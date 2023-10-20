using eMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMovieTicket.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Cinemas = new List<Cinema>();
        }

        public List<Cinema> Cinemas { get; set; }
    }
}
