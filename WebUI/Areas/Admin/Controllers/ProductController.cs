using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category).ToList();
            return View(products);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Product product, IFormFile file, List<int> categories)
        {
            product.CreatedDate = DateTime.Now;
            product.PhotoPath = await file.SaveFileAsync(_env.WebRootPath, "product");
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            for (int i = 0; i < categories.Count; i++)
            {
                ProductCategory productCategory = new()
                {
                    CategoryId = categories[i],
                    ProductId = product.Id
                };
                await _context.AddAsync(productCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (id == null) return NotFound();
            var product = _context.Products
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile file, List<int> categories)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (!ModelState.IsValid)
            {
                var dataproduct = _context.Products
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == product.Id);

                return View(dataproduct);
            }


            if (file != null)
                product.PhotoPath = await file.SaveFileAsync(_env.WebRootPath, "product");

            product.UpdateDate = DateTime.Now;
            _context.Update(product);
            await _context.SaveChangesAsync();

            var proCategory = _context.ProductCategories.Where(x => x.ProductId == product.Id).ToList();
            _context.ProductCategories.RemoveRange(proCategory);

            for (int i = 0; i < categories.Count; i++)
            {
                ProductCategory productCategory = new()
                {
                    CategoryId = categories[i],
                    ProductId = product.Id,
                };
                await _context.ProductCategories.AddAsync(productCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
