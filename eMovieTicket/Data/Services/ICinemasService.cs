using eMovieTicket.Data.Base;
using eMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMovieTicket.Data.Services
{
    public interface ICinemasService:IEntityBaseRepository<Cinema>
    {
    }
}
