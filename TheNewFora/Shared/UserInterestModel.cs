using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheNewFora.Shared
{
    public class UserInterestModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        [ForeignKey(nameof(Interest))]
        public int InterestId { get; set; }
        public InterestModel? Interest { get; set; }
    }
}
