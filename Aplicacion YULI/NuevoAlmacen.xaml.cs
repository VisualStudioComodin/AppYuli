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
    /// Interaction logic for NuevoAlmacen.xaml
    /// </summary>
    public partial class NuevoAlmacen : Window
    {

        private double relacionAncho;
        private double relacionAlto;
        private string id;

        public NuevoAlmacen(double ancho, double alto, string id)
        {
            InitializeComponent();
            this.id = id;
            if (ancho < alto)
            {
                alto = (ancho * (9.0 / 16.0));
            }
            else
            {
                ancho = (alto * (16.0 / 9.0));
            }

            relacionAncho = (this.Width / 1920.0);
            relacionAlto = (this.Height / 1080.0);
            this.Width = relacionAncho * ancho;
            this.Height = relacionAlto * alto;
            this.responsive.Height = this.Height;
            this.responsive.Width = this.Width;

            if (!id.Equals(""))
            {
                Almacen al = new Almacen();
                txtNombre.Text = al.DarNombre(id);
                txtDireccion.Text = al.DarDireccion(id);
                txtPais.Text = al.DarPais(id);
                txtCiudad.Text = al.DarCiudad(id);
                txtTelefono.Text = al.DarTelefono(id);
                registrarBoton.Content = "Guardar";
            }
        }

        private void registrarBoton_Click(object sender, RoutedEventArgs e)
        {
            object[] val = new object[4];
            CuadroMensaje mensaje;
            if (txtNombre.Text.Equals("") || txtDireccion.Text.Equals("") || txtPais.Text.Equals("") || txtCiudad.Text.Equals("") || txtTelefono.Text.Equals(""))
            {
                mensaje = new CuadroMensaje(this.Width, this.Height, "Debe llenar todos los campos", 1, "Error de llenado de datos", true);
                mensaje.Owner = this;
                mensaje.ShowDialog();
            }
            else
            {
                Almacen al = new Almacen();
                int rep = al.Contar(txtNombre.Text);
                if (!id.Equals("") && txtNombre.Text.ToLower().Equals(al.DarNombre(id)))
                    rep = 0;
                bool continuar = false;
                if (rep > 0)
                {
                    mensaje = new CuadroMensaje(this.Width, this.Height, "Ya existe \"" + txtNombre.Text + "\"", 2, "", true);
                    mensaje.Owner = this;
                    mensaje.ShowDialog();
                    continuar = mensaje.DarRespuesta();
                    if (continuar)
                    {
                        rep++;
                        while (al.Contar(txtNombre.Text + "(" + rep + ")") > 0)
                        {
                            rep++;
                        }
                        txtNombre.Text = txtNombre.Text + "(" + rep + ")";
                    }
                }
                else
                    continuar = true;
                if (continuar)
                {
                    val[0] = txtNombre.Text;
                    val[1] = txtDireccion.Text;
                    Contacto c = new Contacto("", txtTelefono.Text, "", txtPais.Text, txtCiudad.Text, "", "", "");
                    val[2] = c;
                    if (id.Equals(""))
                        val[3] = new Dictionary<string, string>();
                    else
                        val[3] = al.DarCantidades(id);
                    mensaje = new CuadroMensaje(this.Width, this.Height, id.Equals("") ? "Almacen registrado" : "Almacen actualizado", 3, "", true);
                    mensaje.Owner = this;
                    mensaje.ShowDialog();
                    if (id.Equals(""))
                        al.CrearNuevoAlmacen(val);
                    else
                    {
                        al.Actualizar(val, id);
                        this.Close();
                    }
                    txtNombre.Text = "";
                    txtDireccion.Text = "";
                    txtPais.Text = "";
                    txtCiudad.Text = "";
                    txtTelefono.Text = "";
                }
            }
        }
    }
}
