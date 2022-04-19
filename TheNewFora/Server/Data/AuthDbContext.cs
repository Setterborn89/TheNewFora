using TheNewFora.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TheNewFora.Server.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        public DbSet<InterestModel> Interests { get; set; }
        public DbSet<ThreadModel> Threads { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserInterestModel> UsersInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many to many (users can have many interests that in turns have many users)
            modelBuilder.Entity<UserInterestModel>()
                .HasKey(ui => new { ui.UserId, ui.InterestId });
            modelBuilder.Entity<UserInterestModel>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInterests)
                .HasForeignKey(ui => ui.UserId);
            modelBuilder.Entity<UserInterestModel>()
                .HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.InterestId);

            // Restrict deletion of interest on user delete (set user to null instead)
            modelBuilder.Entity<InterestModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interests)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Restrict deletion of thread on user delete (set user to null instead)
            modelBuilder.Entity<ThreadModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Threads)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Restrict deletion of thread on message delete (set user to null instead)
            modelBuilder.Entity<MessageModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Seeding admin role

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, "admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Secrets:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                JwtToken = jwt,
                Adminable = true
            });

            // Seeding some interests

            modelBuilder.Entity<InterestModel>().HasData(new InterestModel
            {
                Id = 1,
                Name = "Dogs",
                UserId = ADMIN_ID
            });
            modelBuilder.Entity<InterestModel>().HasData(new InterestModel
            {
                Id = 2,
                Name = "Cats",
                UserId = ADMIN_ID
            });
            modelBuilder.Entity<InterestModel>().HasData(new InterestModel
            {
                Id = 3,
                Name = "Airplanes",
                UserId = ADMIN_ID
            });
            modelBuilder.Entity<InterestModel>().HasData(new InterestModel
            {
                Id = 4,
                Name = "Pokemon",
                UserId = ADMIN_ID
            });
            modelBuilder.Entity<InterestModel>().HasData(new InterestModel
            {
                Id = 5,
                Name = "Trains",
                UserId = ADMIN_ID
            });
            modelBuilder.Entity<InterestModel>().HasData(new InterestModel
            {
                Id = 6,
                Name = "Music",
                UserId = ADMIN_ID
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
