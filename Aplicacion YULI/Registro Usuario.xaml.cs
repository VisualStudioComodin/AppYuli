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
        private double aspectRatio = 0.0;
        private Byte[] fotoBytes;
        private ImageSource imagenPorDefecto;

        public Registro_Usuario()
        {
            InitializeComponent();
            imagenPorDefecto = fotoUsuario.Source;
            comboBoxPermisos.Items.Add("Limitados");
            comboBoxPermisos.Items.Add("Especiales");
            comboBoxPermisos.Items.Add("Totales");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged)
            {
                this.Width = e.NewSize.Height * aspectRatio;
            }
            else if (e.WidthChanged)
            {
                this.Height = e.NewSize.Width * (1 / aspectRatio);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            aspectRatio = this.ActualWidth / this.ActualHeight;
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
            try
            {
                new Usuario(valores);
                MessageBox.Show("Usuario registrado");
                txtUsuario.Text = null;
                txtClave.Password = null;
                txtNombre.Text = null;
                txtApellido.Text = null;
                txtCedula.Text = null;
                txtProfesion.Text = null;
                calendario.DisplayDate = DateTime.Today;
                calendario.SelectedDate = null;
                comboBoxPermisos.SelectedIndex = -1;
                fotoBytes = null;
                fotoBoton.Padding = new Thickness(115);
                fotoUsuario.Source = imagenPorDefecto;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1048: MessageBox.Show("Debe llenar todos los campos");
                        break;
                    case 1062: MessageBox.Show("Ya existe el usuario: " + txtUsuario.Text);
                        break;
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
    }
}
