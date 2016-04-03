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
using System.Windows.Shapes;

namespace Aplicacion_YULI
{
    /// <summary>
    /// Interaction logic for CuadroMensaje.xaml
    /// </summary>
    public partial class CuadroMensaje : Window
    {
        private bool res;
        private double relacionAncho;
        private double relacionAlto;

        public CuadroMensaje(double ancho, double alto, string mensaje, int tipoDeMensaje, string titulo)
        {
            InitializeComponent();
            ancho = (ancho / 100.0) * 50.0;
            alto = (alto / 100.0) * 50.0;
            if (ancho < alto)
            {
                alto = (ancho * (9.0 / 16.0));
            }
            else
            {
                ancho = (alto * (16.0 / 9.0));
            }

            relacionAncho = (1247.0 / 1920.0);
            relacionAlto = (584.0 / 1080.0);
            this.Width = relacionAncho * ancho;
            this.Height = relacionAlto * alto;
            if (tipoDeMensaje == 1)
            {
                ERROR.Visibility = Visibility.Visible;
                ADVERTENCIA.Visibility = Visibility.Hidden;
                ACEPTAR.Visibility = Visibility.Hidden;
                mensajeErrorCuadro.Text = mensaje;
                tituloError.Text = titulo;
            }
            else if (tipoDeMensaje == 2)
            {
                ADVERTENCIA.Visibility = Visibility.Visible;
                ERROR.Visibility = Visibility.Hidden;
                ACEPTAR.Visibility = Visibility.Hidden;
                mensajeAdvertenciaCuadro.Text = mensaje;
            }
            else if (tipoDeMensaje == 3)
            {
                ADVERTENCIA.Visibility = Visibility.Hidden;
                ERROR.Visibility = Visibility.Hidden;
                ACEPTAR.Visibility = Visibility.Visible;
                mensajeAceptarCuadro.Text = mensaje;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            res = true;
            this.Close();
        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            res = true;
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            res = true;
            this.Close();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            res = false;
            this.Close();
        }

        public bool DarRespuesta()
        {
            return res;
        }
    }
}
