using FtpDownloader.UI.DataSources.ViewModels;
using System.Windows;

namespace FtpDownloader.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindow_VM viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
