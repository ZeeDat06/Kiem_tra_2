using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Client.Models
{
    [Table("133_Participants")]
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kèo đấu")]
        public int ChallengeId { get; set; }

        [ForeignKey("ChallengeId")]
        public virtual Challenge? Challenge { get; set; }

        [Display(Name = "Thành viên")]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member? Member { get; set; }
    }
}
