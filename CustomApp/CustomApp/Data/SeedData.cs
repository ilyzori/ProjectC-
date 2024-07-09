using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CustomApp.Data;
using System;
using System.Linq;

namespace CustomApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any products.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Футболка с вашим дизайном",
                        Description = "Создайте свой уникальный дизайн футболки.",
                        Price = 1000,
                        ImageUrl = "/images/custom-tshirt.png"
                    },
                    new Product
                    {
                        Name = "Толстовка с вашим дизайном",
                        Description = "Создайте свою уникальную толстовку.",
                        Price = 2000,
                        ImageUrl = "/images/custom-hoodie.png"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
