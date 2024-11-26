using WebUI.Models.Common;

namespace WebUI.Models;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string PhotoPath { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    
    public int ArticleCategoryId { get; set; }
    public ArticleCategory ArticleCategory { get; set; }
    
    public ICollection<ArticleTag> ArticleTags { get; set; }
    
}