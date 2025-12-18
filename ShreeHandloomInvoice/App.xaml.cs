using ShreeHandloomInvoice.Converter;
using ShreeHandloomInvoice.ViewModel;
using ShreeHandloomInvoice.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ShreeHandloomInvoice;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindow window = new MainWindow();
        NavigationService.MainFrame = window.MainFrame;

        window.MainFrame.Navigate(new InvoiceInputPage
        {
            DataContext = new InvoiceInputViewModel()
        });

        window.WindowState = WindowState.Maximized;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.Show();
    }
}

