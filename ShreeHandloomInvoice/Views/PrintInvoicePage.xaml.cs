using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShreeHandloomInvoice.Views
{
    /// <summary>
    /// Interaction logic for PrintInvoicePage.xaml
    /// </summary>
    public partial class PrintInvoicePage : Page
    {
        public PrintInvoicePage(object viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;

        }

        //public PrintInvoicePage(object viewModel) : this()
        //{
        //    this.DataContext = viewModel;
        //}
    }
}
