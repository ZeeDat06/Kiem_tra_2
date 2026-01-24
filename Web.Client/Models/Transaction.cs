using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Client.Models
{
    [Table("133_Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ngày giao dịch")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vui lòng nhập số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Số tiền")]
        [DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal Amount { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual TransactionCategory? Category { get; set; }
    }
}
