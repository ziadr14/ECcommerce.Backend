using ECom.BLL.Interfaces;
using ECom.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECom.BLL.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUser;

        public InvoiceService(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _currentUser = currentUser;
        }

        public async Task<bool> SendInvoicePdfAsync(int orderId)
        {
            var email = _currentUser.BuyerEmail;

            var order = await _unitOfWork.Orders
                .GetAllQueryable()
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod)
                .FirstOrDefaultAsync(o =>
                    o.Id == orderId &&
                    o.BuyerEmail == email);

            if (order == null)
                return false;

            var pdfBytes = InvoicePdfGenerator.Generate(order);

            await _emailService.SendEmailWithAttachmentAsync(
                to: email,
                subject: $"Invoice for Order #{order.Id}",
                body: "<p>Please find your invoice attached.</p>",
                attachmentBytes: pdfBytes,
                fileName: $"Invoice_{order.Id}.pdf"
            );

            return true;
        }
    }
}
