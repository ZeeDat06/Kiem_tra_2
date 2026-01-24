using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models
{
    [Table("133_News")]
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Member? Author { get; set; }

        public DateTime PublishedAt { get; set; } = DateTime.Now;

        public bool IsPinned { get; set; }
    }
}
