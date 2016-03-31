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
using MySql.Data.MySqlClient;

namespace Aplicacion_YULI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double aspectRatio = 0.0;

        public MainWindow()
        {
            InitializeComponent();
            Registro_Usuario reg = new Registro_Usuario();
            reg.Show();
        }

        private void Inventario_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aspectRatio = this.ActualWidth / this.ActualHeight;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged)
            {
                this.Width = e.NewSize.Height * aspectRatio;
            }
            else if(e.WidthChanged)
            {
                this.Height = e.NewSize.Width * (1 / aspectRatio);
            }
        }
    }
}
