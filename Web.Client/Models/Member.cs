using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Web.Client.Models
{
    [Table("133_Members")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public virtual IdentityUser? IdentityUser { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Ngày tham gia")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        [Display(Name = "Trình độ")]
        public double RankLevel { get; set; } = 1.0;

        [Display(Name = "Trạng thái")]
        public MemberStatus Status { get; set; } = MemberStatus.Active;

        // Navigation properties
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Challenge> CreatedChallenges { get; set; } = new List<Challenge>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        // Helper properties for display
        [NotMapped]
        public string RankBadge
        {
            get
            {
                if (RankLevel > 5.0) return "PRO";
                if (RankLevel < 2.0) return "Newbie";
                return "";
            }
        }

        [NotMapped]
        public string RankBadgeClass
        {
            get
            {
                if (RankLevel > 5.0) return "badge bg-warning text-dark";
                if (RankLevel < 2.0) return "badge bg-info";
                return "";
            }
        }
    }
}
