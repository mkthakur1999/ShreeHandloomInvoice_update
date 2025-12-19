using System;
using Microsoft.Data.Sqlite;
namespace ShreeHandloomInvoice.Data
{
    public class InvoiceRepository
    {
        public int InsertInvoice(InvoiceModel invoice)
        {
            using var con = DbHelper.GetConnection();

            string sql = @"
            INSERT INTO Invoices
            (InvoiceNo, OrderNo, InvoiceDate, CustomerName, TotalAmount)
            VALUES
            (@InvoiceNo, @OrderNo, @InvoiceDate, @CustomerName, @TotalAmount);

            SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@InvoiceNo", invoice.InvoiceNo);
            cmd.Parameters.AddWithValue("@OrderNo", invoice.OrderNo);
            cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@CustomerName", invoice.BuyerName);
            cmd.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void UpdatePdfPath(int invoiceId, string pdfPath)
        {
            using var con = DbHelper.GetConnection();

            string sql = @"
            UPDATE Invoices
            SET PdfPath = @PdfPath,
                IsPdfGenerated = 1
            WHERE InvoiceId = @InvoiceId";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@PdfPath", pdfPath);
            cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
