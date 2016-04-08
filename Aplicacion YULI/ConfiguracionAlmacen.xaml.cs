using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ConfiguracionAlmacen.xaml
    /// </summary>
    public partial class ConfiguracionAlmacen : Window
    {
        private DataTable tabla;
        private double relacionAncho;
        private double relacionAlto;

        public ConfiguracionAlmacen(double ancho, double alto)
        {
            InitializeComponent();

            ancho = (ancho / 100.0) * 80.0;
            alto = (alto / 100.0) * 80.0;

            if (ancho < alto)
                alto = ancho * (9.0 / 16.0);
            else
                ancho = alto * (16.0 / 9.0);

            relacionAncho = (this.Width / 1920.0);
            relacionAlto = (this.Height / 1080.0);
            this.Width = relacionAncho * ancho;
            this.Height = relacionAlto * alto;
            this.responsive.Height = this.Height;
            this.responsive.Width = this.Width;
        }

        private void tablaProductos_Initialized(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            DataTable dat = new Almacen().DarTabla();
            tabla = dat;
            FilaDeAlmacenes almacenes = new FilaDeAlmacenes();
            string[] nombres = new string[] { "nombre", "direccion", "telefono", "pais", "ciudad" };

            for (int i = 0; i < 5; i++)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = almacenes.header[i];
                textColumn.Binding = new Binding(nombres[i]);
                tablaProductos.Columns.Add(textColumn);
                tablaProductos.Columns[i + 1].Width = 306;
            }
            tablaProductos.Columns.Move(0, 5);

            for (int i = 0; i < dat.Select().Length; i++)
            {
                object[] linea = dat.Select()[i].ItemArray;
                almacenes = new FilaDeAlmacenes(linea);
                tablaProductos.Items.Add(almacenes);
               
            }
        }

        private void tablaProductos_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = tablaProductos.SelectedIndex;
            object[] linea = tabla.Select()[i].ItemArray;
            string id = linea[0].ToString();
            NuevoAlmacen al = new NuevoAlmacen(this.Width, this.Height, id);
            al.Owner = this;
            al.ShowDialog();
            tablaProductos.Items.RemoveAt(i);
            tablaProductos.Items.Insert(i, new FilaDeAlmacenes(linea));
            tablaProductos.SelectedIndex = i;
            tablaProductos.Focus();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
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

        private void botonAdd_Click(object sender, RoutedEventArgs e)
        {
            NuevoAlmacen al = new NuevoAlmacen(Width, Height, "");
            al.Owner = this;
            al.ShowDialog();
            Almacen alm = new Almacen();
            DataTable dat = alm.DarTabla();
            object[] nuevos = new object[dat.Rows.Count - tabla.Rows.Count];
            int k = 0;
            int tamOriginal = tabla.Rows.Count;
            for (int i = 0; i < dat.Rows.Count; i++)
            {
                string id = dat.Select()[i].ItemArray[0].ToString();
                int j = 0;
                bool existe = false;
                while (j < tabla.Rows.Count && !existe)
                {
                    existe = tabla.Select()[j].ItemArray[0].ToString().Equals(id);
                    j++;
                }
                if (!existe)
                {
                    nuevos[k] = id;
                    tabla.Rows.Add(new object[] { id });
                    k++;
                }
            }
            FilaDeAlmacenes almacenes;
            for (int i = tamOriginal; i < dat.Select().Length; i++)
            {
                object[] linea = dat.Select()[i].ItemArray;
                almacenes = new FilaDeAlmacenes(linea);
                tablaProductos.Items.Add(almacenes);
            }
        }
    }

    public class FilaDeAlmacenes
    {
        public string[] header;
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }

        public FilaDeAlmacenes(object[] val)
        {
            Almacen al = new Almacen();
            nombre = al.DarNombre(val[0].ToString());
            direccion = al.DarDireccion(val[0].ToString());
            telefono = al.DarTelefono(val[0].ToString());
            pais = al.DarPais(val[0].ToString());
            ciudad = al.DarCiudad(val[0].ToString());
            Inicializar();
        }

        public FilaDeAlmacenes()
        {
            Inicializar();
        }

        private void Inicializar()
        {
            header = new string[] { "Nombre", "Dirección", "Teléfono", "País", "Ciudad"};
        }
    }
}
