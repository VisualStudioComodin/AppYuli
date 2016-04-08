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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Window owner;
        Usuario usuario;

        public Window1(Window owner, Usuario usuario)
        {
            InitializeComponent();
            this.owner = owner;
            this.usuario = usuario;
        }

        private void ChangeText(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void tablaProductos_Initialized(object sender, EventArgs e)
        {

            DataTable dat = new Producto().DarTabla();
            FilaDeProductos productos = new FilaDeProductos();

            for (int i = 0; i < dat.Columns.Count; i++)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = productos.header[i];
                textColumn.Binding = new Binding(dat.Columns[i].ColumnName);
                tablaProductos.Columns.Add(textColumn);
                tablaProductos.Columns[i + 1].Width = 255.3;
            }
            tablaProductos.Columns.Move(0, 6);

            for (int i = 0; i < dat.Select().Length; i++)
            {
                object[] linea = dat.Select()[i].ItemArray;
                productos = new FilaDeProductos(linea);
                tablaProductos.Items.Add(productos);
            }
        }

        private void atrasBoton_Click(object sender, RoutedEventArgs e)
        {
            Menu_Principal u = new Menu_Principal(owner, usuario);
            owner.Content = ((Viewbox)u.Content);
            ((Viewbox)owner.Content).Height = owner.Height;
            ((Viewbox)owner.Content).Width = owner.Width;
            u.Close();
        }

        private void tablaProductos_MouseLeave(object sender, MouseEventArgs e)
        {
            tablaProductos.SelectedIndex = -1;
        }

        private void botonConfigAlmacen_Click(object sender, RoutedEventArgs e)
        {
            //NuevoAlmacen al = new NuevoAlmacen(owner.Width, owner.Height);
            ConfiguracionAlmacen al = new ConfiguracionAlmacen(owner.Width, owner.Height);
            al.Owner = owner;
            al.ShowDialog();
        }
    }

    public class FilaDeProductos
    {
        public string[] header;
        public string nombre { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string precio { get; set; }
        public string cantidad { get; set; }
        public string pais { get; set; }

        public FilaDeProductos(object[] val)
        {
            nombre = val[0].ToString();
            marca = val[1].ToString();
            modelo = val[2].ToString();
            precio = val[3].ToString();
            cantidad = val[4].ToString();
            pais = val[5].ToString();
            Inicializar();
        }

        public FilaDeProductos()
        {
            Inicializar();
        }

        private void Inicializar()
        {
            header = new string[] { "Nombre", "Marca", "Modelo", "Precio (Bs)", "Cantidad total", "País de origen" };
        }
    }
}
