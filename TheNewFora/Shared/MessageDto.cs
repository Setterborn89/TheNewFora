using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFora.Shared
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public string PostedBy { get; set; }
        public bool BanDeleteFlag { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundColor { get; set; }
        public DateTime Date { get; set; }
    }
}
