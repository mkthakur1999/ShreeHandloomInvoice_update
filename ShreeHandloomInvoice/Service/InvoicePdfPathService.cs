using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShreeHandloomInvoice.Services
{
    public static class InvoicePdfPathService
    {
        public static string BuildFullPath(InvoiceModel invoice)
        {
            string root = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ShreeHandloomInvoice",
                "Invoices"
            );

            string year = "2025-26";
            string date = invoice.InvoiceDate.ToString("yyyy-MM-dd");

            string folder = Path.Combine(root, year, date);
            Directory.CreateDirectory(folder);

            string fileName =
                $"INV_{invoice.InvoiceNo}_ORD_{invoice.OrderNo}.pdf";

            return Path.Combine(folder, fileName);
        }
    }
}

