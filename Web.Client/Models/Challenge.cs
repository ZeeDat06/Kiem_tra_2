using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Client.Models
{
    [Table("133_Challenges")]
    public class Challenge
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn người tạo")]
        [Display(Name = "Người tạo")]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual Member? Creator { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(200)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Chế độ")]
        public ChallengeMode Mode { get; set; }

        [StringLength(500)]
        [Display(Name = "Phần thưởng")]
        public string? RewardDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Lệ phí")]
        [DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal EntryFee { get; set; } = 0;

        [Display(Name = "Trạng thái")]
        public ChallengeStatus Status { get; set; } = ChallengeStatus.Open;

        // Navigation properties
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

        // Computed property for Mini-game pot
        [NotMapped]
        public decimal TotalPot => Participants.Count * EntryFee;

        [NotMapped]
        public string StatusDisplay
        {
            get
            {
                return Status switch
                {
                    ChallengeStatus.Open => "Đang nhận",
                    ChallengeStatus.Closed => "Đã chốt",
                    ChallengeStatus.Completed => "Đã trao giải",
                    _ => ""
                };
            }
        }

        [NotMapped]
        public string StatusBadgeClass
        {
            get
            {
                return Status switch
                {
                    ChallengeStatus.Open => "badge bg-success",
                    ChallengeStatus.Closed => "badge bg-warning text-dark",
                    ChallengeStatus.Completed => "badge bg-secondary",
                    _ => "badge bg-light"
                };
            }
        }

        [NotMapped]
        public string ModeDisplay
        {
            get
            {
                return Mode switch
                {
                    ChallengeMode.Singles => "Đơn",
                    ChallengeMode.Doubles => "Đôi",
                    ChallengeMode.MiniGame => "Mini-game",
                    _ => ""
                };
            }
        }
    }
}
