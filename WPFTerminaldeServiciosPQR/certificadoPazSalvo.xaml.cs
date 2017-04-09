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

namespace WPFTerminaldeServiciosPQR
{
    /// <summary>
    /// Interaction logic for certificadoPazSalvo.xaml
    /// </summary>
    public partial class certificadoPazSalvo : Window
    {
        public certificadoPazSalvo()
        {
            InitializeComponent();
        }

        private void txtNumeromatricula_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keypadWindow = new Keypad(textbox, this);
            if (keypadWindow.ShowDialog() == true)
                textbox.Text = keypadWindow.Result;
            long idsuscriptor = Convert.ToInt64(textbox.Text);
            using (ServiceReferencePQR.WSServicePQRClient clienteSwevicioWeb = new ServiceReferencePQR.WSServicePQRClient())
            {
                var suscriptor = clienteSwevicioWeb.obtenerDatosSuscriptor(idsuscriptor);
                foreach (var item in suscriptor)
                {
                    txtNombre.Text = item.vaNombre;
                    txtDireccion.Text = item.vaDireccion;
                    txtSaldofinanciacion.Text = item.nuSaldoPteFinanciacion.ToString();
                    txtSaldopendiente.Text = item.nuSaldoPendiente.ToString();
                    txtSaldoAcueducto.Text = item.nuSaldoPendienteAcu.ToString();
                    txtSaldoAlcantarillado.Text = item.nuSaldoPendienteAlc.ToString();
                    txtFacturasconsaldo.Text = item.nuFacturasconSaldo.ToString();
                }


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow winPrincipal = new MainWindow();
            winPrincipal.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
