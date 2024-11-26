using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider, IFormFile file)
        {   
            if(file == null)
            {
                ModelState.AddModelError("error", "Images is not valid!");
                return View();
            }
            if(!ModelState.IsValid)
                return View();
            
            //var path = "/uploads/" + Guid.NewGuid() + file.FileName;
            //// using directive
            //using FileStream fileStream = new(_env.WebRootPath + path, FileMode.Create);
            
            //await file.CopyToAsync(fileStream);

            slider.PhotoPath = await FileHelper.SaveFileAsync(file, _env.WebRootPath, "slider");

            slider.CreatedDate = DateTime.UtcNow;
            await _context.Sliders.AddAsync(slider);    
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            //First
            //SingleOrDefault, SIngle
            // Find
            var slider = await _context.Sliders.FindAsync(id);
            if(slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Slider slider, IFormFile file)
        {
            if(file != null)
            {
                slider.PhotoPath = await file.SaveFileAsync(_env.WebRootPath, "slider");
            }
            slider.UpdateDate = DateTime.Now;

            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //First
            //SingleOrDefault, SIngle
            // Find
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Slider slider)
        {
            _context.Sliders.Remove(slider);
            System.IO.File.Delete(_env.WebRootPath + slider.PhotoPath);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
