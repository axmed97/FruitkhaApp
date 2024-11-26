using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebUI.Models.Common;

namespace WebUI.Models
{
    public class Slider : BaseEntity
    {
        //[Required(ErrorMessage = "Bu xana bos ola bilmez")]
        //[StringLength(maximumLength: 10, MinimumLength = 11)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PhotoPath { get; set; }
    }
}
