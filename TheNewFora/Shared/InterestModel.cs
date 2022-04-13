using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheNewFora.Shared
{
    public class InterestModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public List<ThreadModel>? Threads { get; set; }

        // Relations
        public List<UserInterestModel>? UserInterests { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
