using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs
{
    public class EmailDto
    {


        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public byte[]? AttachmentBytes { get; set; }
        public string? AttachmentName { get; set; }
    }
}
