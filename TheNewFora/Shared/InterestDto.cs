using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFora.Shared
{
    public class InterestDto
    {
        public int ThreadId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public bool BanDeleteFlag { get; set; }
        public int PostCount { get; set; }
    }
}
