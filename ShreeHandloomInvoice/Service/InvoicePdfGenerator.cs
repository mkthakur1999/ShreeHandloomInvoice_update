using ShreeHandloomInvoice.Views;
using System;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Markup;

namespace ShreeHandloomInvoice.Service
{
    public static class InvoicePdfGenerator
    {
        /// <summary>
        /// Generates invoice PDF using existing PrintInvoicePage layout
        /// </summary>
        public static void Generate(InvoiceModel invoice, string pdfPath)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            // Ensure directory exists
            string folder = Path.GetDirectoryName(pdfPath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // Create print page (same as print preview)
            var page = new PrintInvoicePage(invoice);

            Size pageSize = new Size(793, 1122); // A4 @ 96 DPI
            page.Measure(pageSize);
            page.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
            page.UpdateLayout();

            // Convert UI to FixedDocument
            FixedDocument fixedDoc = new FixedDocument();
            fixedDoc.DocumentPaginator.PageSize = pageSize;

            FixedPage fixedPage = new FixedPage
            {
                Width = pageSize.Width,
                Height = pageSize.Height
            };

            fixedPage.Children.Add(page);
            FixedPage.SetLeft(page, 0);
            FixedPage.SetTop(page, 0);

            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(fixedPage);

            fixedDoc.Pages.Add(pageContent);

            // Save as XPS first
            string xpsPath = Path.ChangeExtension(pdfPath, ".xps");

            using (XpsDocument xpsDoc = new XpsDocument(xpsPath, FileAccess.Write))
            {
                XpsDocumentWriter writer =
                    XpsDocument.CreateXpsDocumentWriter(xpsDoc);

                writer.Write(fixedDoc);
            }

            // Convert XPS to PDF (Windows Print to PDF)
            ConvertXpsToPdf(xpsPath, pdfPath);

            // Cleanup temp XPS
            if (File.Exists(xpsPath))
                File.Delete(xpsPath);
        }

        /// <summary>
        /// Converts XPS file to PDF using Microsoft Print to PDF
        /// </summary>
        private static void ConvertXpsToPdf(string xpsPath, string pdfPath)
        {
            PrintQueue queue = GetPdfPrinter();
            if (queue == null)
                throw new Exception("Microsoft Print to PDF printer not found.");

            PrintTicket ticket = queue.DefaultPrintTicket;
            ticket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

            XpsDocument xpsDoc = new XpsDocument(xpsPath, FileAccess.Read);
            FixedDocumentSequence seq = xpsDoc.GetFixedDocumentSequence();

            XpsDocumentWriter writer =
                PrintQueue.CreateXpsDocumentWriter(queue);

            writer.Write(seq, ticket);

            xpsDoc.Close();
        }

        /// <summary>
        /// Gets Microsoft Print to PDF printer
        /// </summary>
        private static PrintQueue GetPdfPrinter()
        {
            LocalPrintServer server = new LocalPrintServer();

            return server.GetPrintQueues()
                .FirstOrDefault(q => q.Name.Contains("Microsoft Print to PDF"));
        }
    }
}
