using System;
using System.Windows;
using System.Windows.Navigation;

namespace WPFTerminaldeServiciosPQR
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

        private void btnPazSalvo_Click(object sender, RoutedEventArgs e)
        {
            generarCupon wingenerarCupon = new generarCupon();
            wingenerarCupon.Show();
            this.Close();
        }

        private void btnCupon_Click(object sender, RoutedEventArgs e)
        {
            generarCupon wingenerarCupon = new generarCupon();
            wingenerarCupon.Show();
            this.Close();
        }
    }
}
