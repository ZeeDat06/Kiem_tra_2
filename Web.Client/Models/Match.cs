using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Client.Models
{
    [Table("133_Matches")]
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Thời gian thi đấu")]
        [DataType(DataType.DateTime)]
        public DateTime PlayedAt { get; set; } = DateTime.Now;

        [Display(Name = "Kèo đấu")]
        public int? ChallengeId { get; set; }

        [ForeignKey("ChallengeId")]
        public virtual Challenge? Challenge { get; set; }

        [Display(Name = "Thể thức")]
        public MatchFormat Format { get; set; }

        [Display(Name = "Tính điểm xếp hạng")]
        public bool IsRanked { get; set; }

        // Đội 1 - Player 1 (Bắt buộc)
        [Required(ErrorMessage = "Vui lòng chọn người chơi 1")]
        [Display(Name = "Người chơi 1")]
        public int Player1Id { get; set; }

        [ForeignKey("Player1Id")]
        public virtual Member? Player1 { get; set; }

        // Đội 2 - Player 2 (Bắt buộc)
        [Required(ErrorMessage = "Vui lòng chọn người chơi 2")]
        [Display(Name = "Người chơi 2")]
        public int Player2Id { get; set; }

        [ForeignKey("Player2Id")]
        public virtual Member? Player2 { get; set; }

        // Đội 1 - Player 3 (chỉ dùng cho Doubles)
        [Display(Name = "Người chơi 3 (Đội 1)")]
        public int? Player3Id { get; set; }

        [ForeignKey("Player3Id")]
        public virtual Member? Player3 { get; set; }

        // Đội 2 - Player 4 (chỉ dùng cho Doubles)
        [Display(Name = "Người chơi 4 (Đội 2)")]
        public int? Player4Id { get; set; }

        [ForeignKey("Player4Id")]
        public virtual Member? Player4 { get; set; }

        [Display(Name = "Điểm Đội 1")]
        public int ScoreTeam1 { get; set; }

        [Display(Name = "Điểm Đội 2")]
        public int ScoreTeam2 { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        [NotMapped]
        public string FormatDisplay => Format == MatchFormat.Singles ? "Đơn" : "Đôi";
    }
}
