using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFora.Shared
{
    public class UserDto
    {
        public string? Username { get; set; } = String.Empty;
        public string? OldPassword { get; set; } = String.Empty;
        public string? Password { get; set; } = String.Empty;
        public string? ConfirmPassword { get; set; } = String.Empty;
        public TokenDto? Token { get; set; }
    }
}
