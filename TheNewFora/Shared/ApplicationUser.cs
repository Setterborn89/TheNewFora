using Microsoft.AspNetCore.Identity;

namespace TheNewFora.Shared
{
    public class ApplicationUser : IdentityUser
    {
        public string JwtToken { get; set; }
        public bool Adminable { get; set; } = false;
        public bool Banned { get; set; }
        public bool Deleted { get; set; }
        public string? PfpUrl { get; set; }
        public List<UserInterestModel>? UserInterests { get; set; } // Interests this user has
        public List<InterestModel>? Interests { get; set; } // Interests created by this user
        public List<ThreadModel>? Threads { get; set; } // Threads created by this user
        public List<MessageModel>? Messages { get; set; } // Messages created by this user
    }
}
