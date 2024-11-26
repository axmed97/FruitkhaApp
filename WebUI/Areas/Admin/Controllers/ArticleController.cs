using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Data;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ArticleController(
    AppDbContext context, 
    IWebHostEnvironment env,
    IHttpContextAccessor contextAccessor) : Controller
{

    // GET
    public IActionResult Index()
    {
        var articles = context.Articles.ToList();
        return View(articles);
    }

    public IActionResult Create()
    {
        var categories = context.ArticleCategories.ToList();
        var tags = context.Tags.ToList();
        ViewBag.Categories = new SelectList( categories, "Id", "Name");
        ViewData["Tags"] = tags;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Article article, IFormFile file, List<int> tags)
    {
        var userId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        article.AppUserId = userId;
        article.CreatedDate = DateTime.Now; 
        article.PhotoPath = await file.SaveFileAsync(env.WebRootPath, "articles");
        await context.Articles.AddAsync(article);
        await context.SaveChangesAsync();

        for (int i = 0; i < tags.Count; i++)
        {
            ArticleTag articleTag = new()
            {
                ArticleId = article.Id,
                TagId = tags[i],
            };
            await context.ArticleTags.AddAsync(articleTag);
        }
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}