using Microsoft.AspNetCore.Mvc;
using CustomApp.Data;
using System.Linq;
using System.Diagnostics; // �������� ��� ������������ ���� ��� ������������� Debug

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

            Debug.WriteLine("��������� �������� ���������: " + products.Count());

            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword));
                Debug.WriteLine("������ �� ��������� �����: " + keyword + ", ���������� ���������: " + products.Count());
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
                Debug.WriteLine("������ �� ����������� ����: " + minPrice + ", ���������� ���������: " + products.Count());
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
                Debug.WriteLine("������ �� ������������ ����: " + maxPrice + ", ���������� ���������: " + products.Count());
            }

            if (!string.IsNullOrEmpty(category) && category != "���")
            {
                products = products.Where(p => p.Category == category);
                Debug.WriteLine("������ �� ���������: " + category + ", ���������� ���������: " + products.Count());
            }

            ViewData["Keyword"] = keyword;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["Category"] = category;

            var productList = products.ToList();
            Debug.WriteLine("�������� ������ ���������: " + productList.Count());

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
