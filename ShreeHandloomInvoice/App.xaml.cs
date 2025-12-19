using AdminUtilityDashboard.Views;
using ShreeHandloomInvoice.Converter;
using ShreeHandloomInvoice.ViewModel;
using ShreeHandloomInvoice.Views;
using System.Configuration;
using System.Data;
using System.IO;
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
        CreateInvoiceRootFolder();
        MainWindow window = new MainWindow();
        NavigationService.MainFrame = window.MainFrame;

        window.MainFrame.Navigate(new LoginWindow()
        {
            DataContext = new LoginWindow()
        });

        //window.WindowState = WindowState.Maximized;
        //window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.Show();
    }

    private void CreateInvoiceRootFolder()
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ShreeHandloomInvoice",
            "Invoices"
        );

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}

