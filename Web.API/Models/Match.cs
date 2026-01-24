using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models
{
    [Table("133_Matches")]
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public DateTime PlayedAt { get; set; } = DateTime.Now;

        public int? ChallengeId { get; set; }

        [ForeignKey("ChallengeId")]
        public virtual Challenge? Challenge { get; set; }

        public MatchFormat Format { get; set; }

        public bool IsRanked { get; set; }

        // Đội 1 - Player 1 (Bắt buộc)
        public int Player1Id { get; set; }

        [ForeignKey("Player1Id")]
        public virtual Member? Player1 { get; set; }

        // Đội 2 - Player 2 (Bắt buộc)
        public int Player2Id { get; set; }

        [ForeignKey("Player2Id")]
        public virtual Member? Player2 { get; set; }

        // Đội 1 - Player 3 (chỉ dùng cho Doubles)
        public int? Player3Id { get; set; }

        [ForeignKey("Player3Id")]
        public virtual Member? Player3 { get; set; }

        // Đội 2 - Player 4 (chỉ dùng cho Doubles)
        public int? Player4Id { get; set; }

        [ForeignKey("Player4Id")]
        public virtual Member? Player4 { get; set; }

        public int ScoreTeam1 { get; set; }

        public int ScoreTeam2 { get; set; }

        public string? Notes { get; set; }

        [NotMapped]
        public string FormatDisplay => Format == MatchFormat.Singles ? "Đơn" : "Đôi";
    }
}
