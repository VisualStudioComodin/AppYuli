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
    /// Lógica de interacción para Menu_Principal.xaml
    /// </summary>
    public partial class Menu_Principal : Window
    {
        Window owner;
        Usuario usuario;

        public Menu_Principal(Window owner, Usuario usuario)
        {
            InitializeComponent();
            this.owner = owner;
            this.usuario = usuario;
            nombreUsuario.Text = "BIENVENIDO: "+usuario.DarNombre() + " " + usuario.DarApellido();
        }

        private void boton_realizar_venta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void boton_intentario_Click(object sender, RoutedEventArgs e)
        {
            Window1 u = new Window1(owner, usuario);
            owner.Content = ((Viewbox)u.Content);
            ((Viewbox)owner.Content).Height = owner.Height;
            ((Viewbox)owner.Content).Width = owner.Width;
            u.Close();
        }
    }
}
