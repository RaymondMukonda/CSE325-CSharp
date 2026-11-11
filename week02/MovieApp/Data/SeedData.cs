using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp.Data;

public static class SeedData
{
    public static void Initialize(MvcMovieContext context)
    {
        context.Database.EnsureCreated();

        if (context.Movies.Any())
        {
            return;
        }

        context.Movies.AddRange(
            new Movie
            {
                Title = "Inception",
                Genre = "Sci-Fi",
                Price = 14.99m,
                Rating = "PG-13",
                ReleaseDate = new DateTime(2010, 7, 16)
            },
            new Movie
            {
                Title = "The Dark Knight",
                Genre = "Action",
                Price = 16.99m,
                Rating = "PG-13",
                ReleaseDate = new DateTime(2008, 7, 18)
            },
            new Movie
            {
                Title = "Interstellar",
                Genre = "Drama",
                Price = 15.49m,
                Rating = "PG-13",
                ReleaseDate = new DateTime(2014, 11, 7)
            },
            new Movie
            {
                Title = "The Matrix",
                Genre = "Action",
                Price = 13.99m,
                Rating = "R",
                ReleaseDate = new DateTime(1999, 3, 31)
            }
        );

        context.SaveChanges();
    }
}
