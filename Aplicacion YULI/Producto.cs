using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Aplicacion_YULI
{
    public class Producto
    {
        private string[] variables;
        private string tabla;

        public Producto()
        {
            variables = new string[] {"id", "nombre", "marca", "modelo", "precio", "cantidad", "tags", "pais", "detalle", "observacion", "foto"};
            tabla = "productos";
        }

        public void CrearNuevoProducto(object[] val)
        {
            val[5] = JsonConvert.SerializeObject((string[])val[5]);
            object[] valores = new object[11];
            val.CopyTo(valores, 1);
            BaseDeDatos.bd.Insertar(tabla, variables, valores);
        }

        public DataTable DarTabla()
        {
            return BaseDeDatos.bd.DarTabla(tabla, new string[]{variables[1], variables[2], variables[3], variables[4], variables[5], variables[7]});
        }

        public string DarNombre(string id)
        {
            return (string)DarValor(id, 1);
        }

        public string DarMarca(string id)
        {
            return (string)DarValor(id, 2);
        }

        public string DarModelo(string id)
        {
            return (string)DarValor(id, 3);
        }

        public double DarPrecio(string id)
        {
            return (double)DarValor(id, 4);
        }

        public int DarCantidad(string id)
        {
            return (int)DarValor(id, 5);
        }

        public string[] DarTags(string id)
        {
            return JsonConvert.DeserializeObject<string[]>((string)DarValor(id, 6));
        }

        public string DarPais(string id)
        {
            return (string)DarValor(id, 7);
        }

        public string DarDetalle(string id)
        {
            return (string)DarValor(id, 8);
        }

        public string DarObservacion(string id)
        {
            return (string)DarValor(id, 9);
        }

        public ImageSource DarFoto(string id)
        {
            Byte[] bytes = DarFotoBytes(id);
            MemoryStream ms = new MemoryStream(bytes);
            return (ImageSource)new ImageSourceConverter().ConvertFrom(ms);
        }

        private Byte[] DarFotoBytes(string id)
        {
            return (Byte[])DarValor(id, 10);
        }

        private object DarValor(string id, int i)
        {
            return BaseDeDatos.bd.Seleccionar(tabla, variables, "" + variables[0] + "='" + id + "'")[i][0];
        }
    }
}
