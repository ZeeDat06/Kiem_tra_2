using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Client.Models
{
    [Table("133_News")]
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(200)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Tác giả")]
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Member? Author { get; set; }

        [Display(Name = "Ngày đăng")]
        [DataType(DataType.DateTime)]
        public DateTime PublishedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ghim tin")]
        public bool IsPinned { get; set; }
    }
}
