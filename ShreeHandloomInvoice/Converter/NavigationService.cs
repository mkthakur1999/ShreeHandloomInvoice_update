using ShreeHandloomInvoice.ViewModel;
using ShreeHandloomInvoice.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShreeHandloomInvoice.Converter
{
    public static class NavigationService
    {
        public static Frame MainFrame { get; set; }

        public static void Navigate(object viewModel)
        {
            Page page = null;

            if (viewModel is InvoicePreviewViewModel vm)
                page = new InvoicePreviewPage { DataContext = vm };

            MainFrame.Navigate(page);
        }
    }
}
