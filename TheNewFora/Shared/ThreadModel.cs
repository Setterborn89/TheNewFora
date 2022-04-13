using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheNewFora.Shared
{
    public class ThreadModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public List<MessageModel>? Messages { get; set; }

        // Relations
        [ForeignKey(nameof(Interest))]
        public int InterestId { get; set; }
        public InterestModel? Interest { get; set; }
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
