using ECom.BLL.Interfaces;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace ECom.BLL.Services
{

        public static class InvoicePdfGenerator
        {
            public static byte[] Generate(Orders order)
            {
                QuestPDF.Settings.License = LicenseType.Community;

                return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);

                        page.Content().Column(col =>
                        {
                            col.Item().Text($"Invoice #{order.Id}")
                                .FontSize(20)
                                .Bold();

                            col.Item().Text($"Date: {order.OrderDate:d}");
                            col.Item().Text($"Email: {order.BuyerEmail}");

                            col.Item().LineHorizontal(1);

                            foreach (var item in order.OrderItems)
                            {
                                col.Item().Text(
                                    $"{item.ProductName} x{item.Quantity} - {item.Price} EGP"
                                );
                            }

                            col.Item().LineHorizontal(1);

                            col.Item().Text($"Subtotal: {order.SubTotal} EGP")
                                .Bold();

                            col.Item().Text($"Shipping: {order.DeliveryMethod.Price} EGP");

                            col.Item().Text($"Total: {order.SubTotal + order.DeliveryMethod.Price} EGP")
                                .FontSize(16)
                                .Bold();
                        });
                    });
                }).GeneratePdf();
            }
        }
    }
