using WebUI.Models.Common;

namespace WebUI.Models;

public class ArticleCategory : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Article> Articles { get; set; }
}