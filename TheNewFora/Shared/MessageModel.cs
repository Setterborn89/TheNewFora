using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheNewFora.Shared
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string? Message { get; set; } = String.Empty;
        public DateTime Date { get; set; }

        // Relations
        [ForeignKey(nameof(Thread))]
        public int ThreadId { get; set; }
        public ThreadModel? Thread { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
