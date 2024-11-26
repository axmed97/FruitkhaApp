using WebUI.Models.Common;

namespace WebUI.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; set; }
}