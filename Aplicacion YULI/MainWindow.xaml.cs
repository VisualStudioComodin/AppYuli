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

namespace Aplicacion_YULI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private Viewbox responsive;
        public Usuario usuarios;

        public MainWindow()
        {
            InitializeComponent();
            usuarios = new Usuario();
           
            // Window2 u = new Window2(this.ventana, usuarios);   // Pantalla Inicio

            //Menu_Principal u = new Menu_Principal(this.ventana, new Usuario() { id = "1234" });  // Menu Principal

            Window1 u = new Window1(this.ventana, usuarios); // Inventario

            ventana.Content = null;
            responsive = ((Viewbox)u.Content);
            responsive.Height = this.Height;
            responsive.Width = this.Width;
            ventana.AddChild(responsive);
            u.Close();
        }

        private void ventana_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                ((Viewbox)ventana.Content).Width = this.Width;
                ((Viewbox)ventana.Content).Height = this.Height;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
