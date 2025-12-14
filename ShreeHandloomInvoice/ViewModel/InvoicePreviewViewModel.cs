using ShreeHandloomInvoice.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ShreeHandloomInvoice.Command;
using System.Windows.Input;

namespace ShreeHandloomInvoice.ViewModel
{
    public class InvoicePreviewViewModel : BaseViewModel
    {
        private InvoiceModel _invoice;
        public InvoiceModel Invoice
        {
            get => _invoice;
            set => SetProperty(ref _invoice, value);
        }
        public ICommand PrintCommand { get; }

        public InvoicePreviewViewModel(InvoiceModel invoice)
        {
            Invoice = invoice ?? new InvoiceModel();
            PrintCommand = new RelayCommand(PrintInvoice);
        }

        private void PrintInvoice()
        {
            try
            {
                var page = new PrintInvoicePage(Invoice); // ✅ SAME OBJECT
                var pd = new PrintDialog();
                pd.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

                var pageSize = new Size(793, 1122);
                page.Measure(pageSize);
                page.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
                page.UpdateLayout();

                pd.PrintVisual(page, "Invoice Print");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print error: " + ex.Message);
            }
        }

        //private void PrintInvoiceold()
        //{
        //    try
        //    {
        //        var page = new PrintInvoicePage(InvoiceModel); // page ctor that accepts model
        //        var pd = new PrintDialog();
        //        pd.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

        //        //if (pd.ShowDialog() != true);

        //        var pageSize = new Size(793, 1122);
        //        page.Measure(pageSize);
        //        page.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
        //        page.UpdateLayout();

        //        pd.PrintVisual(page, "Invoice Print");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print error: " + ex.Message);
        //    }
        //}
    }
}
