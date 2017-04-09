using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace WPFTerminaldeServiciosPQR
{
    /// <summary>
    /// Interaction logic for generarCupon.xaml
    /// </summary>
    public partial class generarCupon : Window
    {
        public int valorminimo = 0;
        public generarCupon()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow winPrincipal = new MainWindow();
            winPrincipal.Show();
            this.Close();
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
                    if (item.nuSaldoPendiente == 0)
                        {
                        txtNombre.Text = "";
                        txtNumerofactura.Text = "0";
                        txtValorabonar.Text = "0";
                        txtSaldopendiente.Text = "0";
                        txtFacturasconsaldo.Text = "0";
                        lblmensaje.Content = "";
                        spnl_btnImprimir.IsEnabled = false;
                        MessageBoxResult result = MessageBox.Show("El suscriptor no tiene saldo pendiente");
                    }
                    else
                    {
                        txtNombre.Text = item.vaNombre;
                        txtNumerofactura.Text = item.nuIdCuentaCobro.ToString();
                        txtValorabonar.Text = (item.nuSaldoPendiente - item.nuSaldoPendienteUltimaFactura).ToString();
                        txtSaldopendiente.Text = item.nuSaldoPendiente.ToString();
                        txtFacturasconsaldo.Text = item.nuFacturasconSaldo.ToString();
                        lblmensaje.Content = "Valor minimo para evitar suspension";
                        spnl_btnImprimir.IsEnabled = true;
                        valorminimo = Convert.ToInt32(txtValorabonar.Text);
                    }
                }
            }
            
        }

        private void txtValoranobar_preview(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keypadWindow = new Keypad(textbox, this);
            if (keypadWindow.ShowDialog() == true)
                textbox.Text = keypadWindow.Result;

            if(Convert.ToInt32(txtValorabonar.Text)<valorminimo)
            {
                MessageBoxResult result = MessageBox.Show("Este valor no lo ecxime de la suspension");
                //http://www.wpf-tutorial.com/dialogs/the-messagebox/
            }





        }
    }
}
