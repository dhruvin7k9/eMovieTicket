using eMovieTicket.Data.Static;
using eMovieTicket.Data.ViewModels;
using eMovieTicket.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMovieTicket.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Logo = "https://i.ytimg.com/vi/4l_FUDXk9-U/maxresdefault.jpg",
                            Name = "Cinema 1",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Logo = "https://sapianlabs.com/wp-content/uploads/2020/02/inox-logo.jpeg",
                            Name = "Cinema 2",
                            Description = "This is the description of the second cinema"
                        },
                        new Cinema()
                        {
                            Logo = "https://fotos.e-consulta.com/cinepolis_5.jpg?",
                            Name = "Cinema 3",
                            Description = "This is the description of the third cinema"
                        },
                        new Cinema()
                        {
                            Logo = "https://thumbs.dreamstime.com/b/raj-mandir-cinema-jaipur-india-22622247.jpg",
                            Name = "Cinema 4",
                            Description = "This is the description of the fourth cinema"
                        }

                    });
                    context.SaveChanges();
                }

                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "3 idiots",
                            Description = "This is the 3 idiots movie description",
                            Price = 100.0,
                            ImageURL = "https://globalfreeonlineads.com/wp-content/uploads/2018/02/3-Idiots1.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(30),
                            MovieCategory = MovieCategory.Documentary,
                            Protagonist = "Amir Khan",
                            CinemaId = 3,
                        },
                        new Movie()
                        {
                            Name = "Krish 3",
                            Description = "This is the Krish 3 description",
                            Price = 150.0,
                            ImageURL = "https://i.pinimg.com/originals/99/88/cd/9988cd9e4eb6cd662cd9c34e15e6463e.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            MovieCategory = MovieCategory.Action,
                            Protagonist = "Hritik Roshan",
                            CinemaId = 1,
                        },
                        new Movie()
                        {
                            Name = "Shollay",
                            Description = "This is the Sholay description",
                            Price = 200.0,
                            ImageURL = "https://image.tmdb.org/t/p/w500/4xb59qrcrzykO836WTewAhYkgOY.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            MovieCategory = MovieCategory.Horror,
                            Protagonist = "Amitabh Bachhan",
                            CinemaId = 4, 
                        },
                        new Movie()
                        {
                            Name = "Jawan",
                            Description = "This is the Jawan description",
                            Price = 250.0,
                            ImageURL = "https://assets.gadgets360cdn.com/pricee/assets/product/202206/Jawan-poster_1655912386.jpg",
                            StartDate = DateTime.Now.AddDays(20),
                            EndDate = DateTime.Now.AddDays(25),
                            MovieCategory = MovieCategory.Documentary,
                            Protagonist = "SRK",
                            CinemaId = 1,
                        },
                        new Movie()
                        {
                            Name = "Kung Fu Panda 3",
                            Description = "This is the Kung Fu Panda 3 description",
                            Price = 220,
                            ImageURL = "https://image.tmdb.org/t/p/original/v3iwugzv5kmsK4zmyEtzG5S7lOf.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.Cartoon,
                            Protagonist = "Jack Black",
                            CinemaId = 3,   
                        },
                        new Movie()
                        {
                            Name = "Baghban",
                            Description = "This is the Baghban movie description",
                            Price = 300,
                            ImageURL = "https://www.moviemeter.nl/images/cover/21000/21913.jpg",
                            StartDate = DateTime.Now.AddDays(20),
                            EndDate = DateTime.Now.AddDays(30),
                            MovieCategory = MovieCategory.Drama,
                            Protagonist = "Amitabh Bachhan",
                            CinemaId = 2
                        }
                    });
                    context.SaveChanges();
                }
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var userStore = serviceScope.ServiceProvider.GetRequiredService<IUserStore<ApplicationUser>>();
                IUserEmailStore<ApplicationUser> emailStore = GetEmailStore(userManager, userStore);

                string adminUserEmail = "admin@emovietickets.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Name = "Admin",
                        EmailConfirmed = true,
                    };
                    await userStore.SetUserNameAsync(newAdminUser, "admin", CancellationToken.None);
                    await emailStore.SetEmailAsync(newAdminUser, adminUserEmail, CancellationToken.None);
                    await userManager.CreateAsync(newAdminUser, "AdminUserPassword123##");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user1@emovietickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        Name = "App user",
                        EmailConfirmed = true,
                    };
                    await userStore.SetUserNameAsync(newAppUser, "usertest", CancellationToken.None);
                    await emailStore.SetEmailAsync(newAppUser, appUserEmail, CancellationToken.None);
                    await userManager.CreateAsync(newAppUser, "dullUser@1");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

        private static IUserEmailStore<ApplicationUser> GetEmailStore(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)userStore;
        }
    }
}