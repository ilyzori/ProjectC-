using CustomApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Применение миграций
            context.Database.Migrate();

            // Очистка таблицы продуктов
            if (context.Products.Any())
            {
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();
            }

            // Добавление новых данных
            var products = new Product[]
            {
                new Product {
                    Name = "Футболка с вашим дизайном",
                    Description = "Создайте свой уникальный дизайн футболки.",
                    Price = 1000,
                    ImageUrl = "/images/1956.750x0.png",
                    DetailedDescription = "Материал: 100% хлопок. Доступны различные размеры."
                },
                new Product {
                    Name = "Футболка с черепом",
                    Description = "Стильная футболка с изображением черепа.",
                    Price = 1200,
                    ImageUrl = "/images/pdfgdpfgjdfjlgkj08gejlfd.png",
                    DetailedDescription = "Материал: 80% хлопок, 20% полиэстер. Доступны различные размеры."
                },
                new Product {
                    Name = "Футболка с медведем",
                    Description = "Футболка с изображением милого медведя.",
                    Price = 1300,
                    ImageUrl = "/images/BP_wu620004_3_tif_1000x1000.jpg",
                    DetailedDescription = "Материал: 90% хлопок, 10% вискоза. Доступны различные размеры."
                },
                new Product {
                    Name = "Футболка с цветами",
                    Description = "Футболка с красивыми цветами.",
                    Price = 1100,
                    ImageUrl = "/images/felpa-nera-semplice.jpg",
                    DetailedDescription = "Материал: 70% хлопок, 30% полиэстер. Доступны различные размеры."
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
