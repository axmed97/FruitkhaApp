namespace WebUI.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}
