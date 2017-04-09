using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFTerminaldeServiciosPQR;

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

            certificadoPazSalvo objpazysalvo = new certificadoPazSalvo();
            objpazysalvo.Show();
            this.Close();
        }

        private void btnCupon_Click(object sender, RoutedEventArgs e)
        {

            generarCupon objgenerarCupon = new generarCupon();
            objgenerarCupon.Show();
            this.Close();
        }

        private void btnDuplicado_Click(object sender, RoutedEventArgs e)
        {
            generarCupon objgenerarCupon = new generarCupon();
            objgenerarCupon.Show();
            this.Close();
        }
    }
}
