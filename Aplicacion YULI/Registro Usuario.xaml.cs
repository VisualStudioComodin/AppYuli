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
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;

namespace Aplicacion_YULI
{
    /// <summary>
    /// Lógica de interacción para Registro_Usuario.xaml
    /// </summary>
    public partial class Registro_Usuario : Window
    {
        private Byte[] fotoBytes;
        private ImageSource imagenPorDefecto;
        private Window owner;
        private Usuario usuario;

        public Registro_Usuario(Window owner, Usuario usuario)
        {
            InitializeComponent();
            this.owner = owner;
            imagenPorDefecto = fotoUsuario.Source;
            comboBoxPermisos.Items.Add("Limitados");
            comboBoxPermisos.Items.Add("Especiales");
            comboBoxPermisos.Items.Add("Totales");
            this.usuario = usuario;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.responsive.Height = this.Height;
            this.responsive.Width = this.Width;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void registrarBoton_Click(object sender, RoutedEventArgs e)
        {
            object[] valores = new object[9];
            valores[0] = txtUsuario.Text.Equals("") ? null : txtUsuario.Text.ToLower();
            valores[1] = txtClave.Password.Equals("") ? null : txtClave.Password;
            valores[2] = txtNombre.Text.Equals("") ? null : txtNombre.Text;
            valores[3] = txtApellido.Text.Equals("") ? null : txtApellido.Text;
            valores[4] = txtCedula.Text.Equals("") ? null : txtCedula.Text;
            valores[5] = txtProfesion.Text.Equals("") ? null : txtProfesion.Text;
            valores[6] = calendario.SelectedDate;
            valores[7] = ((comboBoxPermisos.SelectedIndex == -1) ? null : (object)comboBoxPermisos.SelectedIndex);
            valores[8] = fotoBytes;
            CuadroMensaje mensaje;
            try
            {
                bool res = true;
                if (fotoBytes == null)
                {
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "¿Registrar sin fotografía?", 2, "", false);
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                    res = mensaje.DarRespuesta();
                }
                if (res)
                {
                    if (txtClave.Password.Equals(txtClaveRep.Password))
                    {
                        usuario.CrearNuevoUsuario(valores);
                        mensaje = new CuadroMensaje(owner.Width, owner.Height, "Usuario registrado", 3, "", false);
                        mensaje.Owner = owner;
                        mensaje.ShowDialog();
                        txtUsuario.Text = null;
                        txtClave.Password = null;
                        txtNombre.Text = null;
                        txtApellido.Text = null;
                        txtCedula.Text = null;
                        txtProfesion.Text = null;
                        txtClaveRep.Password = null;
                        calendario.DisplayDate = DateTime.Today;
                        calendario.SelectedDate = null;
                        comboBoxPermisos.SelectedIndex = -1;
                        fotoBytes = null;
                        fotoBoton.Padding = new Thickness(115);
                        fotoUsuario.Source = imagenPorDefecto;
                    }
                    else
                    {
                        mensaje = new CuadroMensaje(owner.Width, owner.Height, "Las contraseñas no son iguales", 1, "Error de contraseña", false);
                        mensaje.Owner = owner;
                        mensaje.ShowDialog();
                        txtClave.Password = null;
                        txtClaveRep.Password = null;
                    }
                }
            }
            catch (MySqlException ex)
            {
               
                if(ex.Number == 1048){
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Debe llenar todos los campos", 1, "Error 1048", false);
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                }
                else if(ex.Number == 1062){
                    mensaje = new CuadroMensaje(owner.Width, owner.Height, "Ya existe el usuario: " + txtUsuario.Text, 1, "Error 1062", false);
                    mensaje.Owner = owner;
                    mensaje.ShowDialog();
                }
            }
        }

        private void fotoBoton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Seleccione una fotografía";
            op.Filter = "Todas las imágenes soportadas|*.jpg;*.jpeg;*.png;*.gif|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "PNG (*.png)|*.png|" +
              "GIF (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                fotoUsuario.Source = new BitmapImage(new Uri(op.FileName));
                fotoBoton.Padding = new Thickness(0);
                fotoBytes = ImagenABytes(op.FileName);
            }
        }

        private Byte[] ImagenABytes(string ruta)
        {
            FileStream foto = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            Byte[] arreglo = new Byte[foto.Length];
            BinaryReader reader = new BinaryReader(foto);
            arreglo = reader.ReadBytes(Convert.ToInt32(foto.Length));
            return arreglo;
        }

        private void botonAtras_Click(object sender, RoutedEventArgs e)
        {
            Window2 u = new Window2(owner, usuario);
            owner.Content = ((Viewbox)u.Content);
            ((Viewbox)owner.Content).Height = owner.Height;
            ((Viewbox)owner.Content).Width = owner.Width;
            u.Close();
        }
    }
}
