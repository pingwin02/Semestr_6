using System.Windows;

namespace HostingAxInWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Integration.WindowsFormsHost host1 =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            System.Windows.Forms.Integration.WindowsFormsHost host2 =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            System.Windows.Forms.Integration.WindowsFormsHost host3 =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            AxSHDocVw.AxWebBrowser axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
            AxAcroPDFLib.AxAcroPDF axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();

            host1.Child = axWebBrowser1;

            this.grid1.Children.Add(host1);

            axWebBrowser1.Navigate(@"https://www.google.com");

            host2.Child = axAcroPDF1;

            this.grid2.Children.Add(host2);

            axAcroPDF1.LoadFile(@"C:\Users\Damian\Studia\Semestr_6\KSR\Lab3\HostingAxInWpf\Get_Started_With_Smallpdf.pdf");

            host3.Child = axWindowsMediaPlayer1;

            this.grid3.Children.Add(host3);

            axWindowsMediaPlayer1.URL = @"C:\Users\Damian\Studia\Semestr_6\KSR\Lab3\HostingAxInWpf\Ring01.wav";
        }
    }
}