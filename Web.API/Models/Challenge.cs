using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models
{
    [Table("133_Challenges")]
    public class Challenge
    {
        [Key]
        public int Id { get; set; }

        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual Member? Creator { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public ChallengeMode Mode { get; set; }

        [StringLength(500)]
        public string? RewardDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EntryFee { get; set; } = 0;

        public ChallengeStatus Status { get; set; } = ChallengeStatus.Open;

        // Navigation properties
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
