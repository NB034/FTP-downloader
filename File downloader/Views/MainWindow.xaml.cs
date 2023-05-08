using File_downloader.ViewModels;
using System.Windows;

namespace File_downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new MainWindow_VM();
        }
    }
}
