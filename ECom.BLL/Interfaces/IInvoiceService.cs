using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IInvoiceService
    {
        Task<bool> SendInvoicePdfAsync(int orderId);
    }
}
