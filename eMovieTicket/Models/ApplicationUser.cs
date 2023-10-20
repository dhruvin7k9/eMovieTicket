using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eMovieTicket.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Full name is required")]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        /*
        [Display(Name = "User name")]
        [Required(ErrorMessage = "User name is required")]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; }

        
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        */
    }
}
