using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Data;

namespace MoviesAPI.Models
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                MovieProcessor movieProcessor = new MovieProcessor();
                RemoveTokens(context);
                await AddNewToken(context, movieProcessor);
                // Look for any entries.
                if (context.MovieTitles.Any())
                {
                    return;   // DB has been seeded
                }

                await SeedMovieIds(context, movieProcessor);
                
            }
        }

        public static void RemoveTokens(AppDbContext context)
        {
            foreach (var item in context.ApiConfigs.ToList())
            {
                context.ApiConfigs.Remove(item);
                context.SaveChanges();
            }
        }

        public static async Task AddNewToken(AppDbContext context, MovieProcessor movieProcessor)
        {            
            var token = await movieProcessor.LoadToken();
            context.ApiConfigs.Add(token);
            context.SaveChanges();
        }

        public static async Task SeedMovieIds(AppDbContext context, MovieProcessor movieProcessor)
        {
            var token = context.ApiConfigs.First().Token.ToString();

            var ids = await movieProcessor.LoadIds(token);
        }
    }
}
