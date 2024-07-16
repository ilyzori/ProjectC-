using Microsoft.AspNetCore.Mvc;
using CustomApp.Data;
using System.Linq;
using System.Diagnostics; // Добавьте это пространство имен для использования Debug

namespace CustomApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string keyword, decimal? minPrice, decimal? maxPrice, string category)
        {
            var products = _context.Products.AsQueryable();

            Debug.WriteLine("Начальная загрузка продуктов: " + products.Count());

            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword));
                Debug.WriteLine("Фильтр по ключевому слову: " + keyword + ", количество продуктов: " + products.Count());
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
                Debug.WriteLine("Фильтр по минимальной цене: " + minPrice + ", количество продуктов: " + products.Count());
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
                Debug.WriteLine("Фильтр по максимальной цене: " + maxPrice + ", количество продуктов: " + products.Count());
            }

            if (!string.IsNullOrEmpty(category) && category != "Все")
            {
                products = products.Where(p => p.Category == category);
                Debug.WriteLine("Фильтр по категории: " + category + ", количество продуктов: " + products.Count());
            }

            ViewData["Keyword"] = keyword;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["Category"] = category;

            var productList = products.ToList();
            Debug.WriteLine("Итоговый список продуктов: " + productList.Count());

            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
