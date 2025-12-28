using System;

namespace ECom.BLL.DTOs
{
    public static class EmailStringBody
    {


        public static string BuildInvoice(InvoiceEmailDto invoice)
        {
            var rows = string.Join("", invoice.Items.Select(item => $@"
        <tr>
            <td>{item.ProductName}</td>
            <td align='center'>{item.Quantity}</td>
            <td align='right'>{item.Price:C}</td>
        </tr>
    "));

            return $@"
<!DOCTYPE html>
<html>
<body style='font-family:Arial;background:#f4f6f8;padding:30px;'>

<table width='600' align='center' style='background:#fff;border-radius:8px;padding:20px'>
    <tr>
        <td>
            <h2>🧾 Invoice - Order #{invoice.OrderId}</h2>
            <p>Date: {invoice.OrderDate}</p>

            <table width='100%' border='1' cellpadding='8' cellspacing='0'>
                <thead>
                    <tr style='background:#0d6efd;color:white'>
                        <th>Product</th>
                        <th>Qty</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>

            <p><b>Delivery:</b> {invoice.DeliveryMethod}</p>
            <p><b>Shipping:</b> {invoice.DeliveryPrice:C}</p>

            <h3>Total: {invoice.Total:C}</h3>

            <hr />
            <p style='font-size:12px;color:#777'>
                Thank you for shopping with us ❤️
            </p>
        </td>
    </tr>
</table>

</body>
</html>";
        }









        public static string Build(
            string email,
            string token,
            string component,
            string title,
            string message,
            string buttonText
        )
        {
            string encodedToken = Uri.EscapeDataString(token);
            string encodedEmail = Uri.EscapeDataString(email);

            string url = component switch
            {
                "active" => $"http://localhost:4200/activate-account?email={encodedEmail}&token={encodedToken}",
                "reset-password" => $"http://localhost:4200/reset-password?email={encodedEmail}&token={encodedToken}",
                _ => $"http://localhost:4200"
            };

            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{title}</title>
</head>
<body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

    <table width='100%' cellpadding='0' cellspacing='0'>
        <tr>
            <td align='center' style='padding:40px 0;'>

                <table width='600' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:8px; overflow:hidden;'>

                    <tr>
                        <td style='background:#0d6efd; padding:20px; text-align:center;'>
                            <h1 style='color:#ffffff; margin:0; font-size:24px;'>E-Commerce</h1>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:30px; color:#333;'>
                            <h2 style='margin-top:0;'>Hello {email},</h2>

                            <p style='font-size:15px; line-height:1.6;'>
                                {message}
                            </p>

                            <div style='text-align:center; margin:30px 0;'>
                                <a href='{url}'
                                   style='background:#0d6efd;
                                          color:#ffffff;
                                          padding:14px 28px;
                                          text-decoration:none;
                                          border-radius:6px;
                                          font-size:16px;
                                          display:inline-block;'>
                                    {buttonText}
                                </a>
                            </div>

                            <p style='font-size:13px; color:#666;'>
                                If you did not request this, please ignore this email.
                            </p>

                            <p style='font-size:13px; color:#666;'>
                                This link will expire for security reasons.
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td style='background:#f1f1f1; padding:15px; text-align:center; font-size:12px; color:#777;'>
                            © {DateTime.Now.Year} E-Commerce. All rights reserved.
                        </td>
                    </tr>

                </table>

            </td>
        </tr>
    </table>

</body>
</html>";
        }
    }
}
