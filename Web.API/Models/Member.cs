using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Web.API.Models
{
    [Table("133_Members")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public virtual IdentityUser? IdentityUser { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DOB { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

        public double RankLevel { get; set; } = 1.0;

        public MemberStatus Status { get; set; } = MemberStatus.Active;

        // Navigation properties
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Challenge> CreatedChallenges { get; set; } = new List<Challenge>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
