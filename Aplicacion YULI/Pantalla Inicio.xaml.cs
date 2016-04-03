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
using MySql.Data.MySqlClient;

namespace Aplicacion_YULI
{
    /// <summary>
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        private Window owner;
        private Usuario usuario;

        public Window2(Window owner, Usuario usuario)
        {
            InitializeComponent();
            this.owner = owner;
            this.usuario = usuario;
        }

        private void botonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Registro_Usuario u = new Registro_Usuario(owner, usuario);
            owner.Content = ((Viewbox)u.Content);
            ((Viewbox)owner.Content).Height = owner.Height;
            ((Viewbox)owner.Content).Width = owner.Width;
            u.Close();
        }

        private void iniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            CuadroMensaje mensaje;
            try
            {
                if (usuario.IniciarSesion(txtUsuario.Text, txtClave.Password))
                {
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Sesión iniciada", 3, "");
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                    txtClave.Password = "";
                    txtUsuario.Text = "";
                    Menu_Principal u = new Menu_Principal();
                    owner.Content = ((Viewbox)u.Content);
                    ((Viewbox)owner.Content).Height = owner.Height;
                    ((Viewbox)owner.Content).Width = owner.Width;
                    u.Close();
                }
                else if (!txtClave.Password.Equals(""))
                {
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Contraseña erronea", 1, "Error de contraseña");
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                    txtClave.Password = "";
                }
                else
                {
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Debe introducir la contraseña", 1, "Error de contraseña");
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                }
            }
            catch (Exception)
            {
                if (txtClave.Password.Equals("") || txtUsuario.Text.Equals(""))
                {
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Debe llenar todos los campos", 1, "Error de llenado de datos");
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                }
                else
                {
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Nombre de usuario erroneo", 1, "Error de usuario");
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                }
                txtClave.Password = "";
                txtUsuario.Text = "";
            }
        }
    }
}
