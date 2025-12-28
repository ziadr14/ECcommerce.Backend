using ECom.BLL.DTOs;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto emailDto);
        Task SendEmailAsync(ContactMessageDto dto);

        Task SendEmailWithAttachmentAsync(
    string to,
    string subject,
    string body,
    byte[] attachmentBytes,
    string fileName
);

    }
}
