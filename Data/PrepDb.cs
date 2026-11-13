using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void prepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {

            if(isProduction )
            {
                Console.WriteLine("---> Attempting to migrate");
                try
                {
                    context.Database.Migrate();
                }catch(Exception ex)
                {
                    Console.WriteLine($"---> DB migrate exception {ex.Message}");
                }
                finally
                {
                    
                }
                
            }else{

                if (!context.Platforms.Any())
                {
                    Console.WriteLine("creating new Data");
                    context.AddRange(
                        new Platform(){
                            Name = "Dot Net",
                            Publisher = "Microsoft",
                            Cost = "Free",
                            Description = "Dot Net is a free, cross-platform, open source developer platform for building many different types of applications."
                        },new Platform(){
                            Name = "SQL Server Express",
                            Publisher = "Microsoft",
                            Cost = "Free",
                            Description = "SQL Server Express is a free edition of SQL Server, ideal for development and production for desktop, web, and small server applications."
                        },new Platform(){
                            Name = "Kubernetes",
                            Publisher = "Cloud Native Computing Foundation",
                            Cost = "Free",
                            Description = "Kubernetes is an open-source system for automating deployment, scaling, and management of containerized applications."
                        });
                }
                else
                {
                    Console.WriteLine("Data already exists");
                }
            }

            context.SaveChanges();
        }
    }
}