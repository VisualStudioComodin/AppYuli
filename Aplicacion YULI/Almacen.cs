using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_YULI
{
    class Almacen
    {
        private string[] variables;
        private string tabla;

        public Almacen()
        {
            variables = new string[] {"id", "nombre", "Direccion", "contacto", "cantidades"};
            tabla = "almacenes";
        }

        public DataTable DarTabla()
        {
            return BaseDeDatos.bd.DarTabla(tabla, new string[] { variables[0]});
        }

        public void Actualizar(object[] valores, string id)
        {
            valores[0] = ((string)valores[0]).ToLower();
            valores[2] = JsonConvert.SerializeObject((Contacto)valores[2]);
            valores[3] = JsonConvert.SerializeObject((Dictionary<string, string>)valores[3]);
            object[] val = new object[5];
            valores.CopyTo(val, 1);
            BaseDeDatos.bd.Actualizar(tabla, variables, val, variables[0] + "='" + id + "'");
        }

        public void CrearNuevoAlmacen(object[] valores)
        {
            valores[0] = ((string)valores[0]).ToLower();
            valores[2] = JsonConvert.SerializeObject((Contacto)valores[2]);
            valores[3] = JsonConvert.SerializeObject((Dictionary<string, string>)valores[3]);
            object[] val = new object[5];
            valores.CopyTo(val, 1);
            BaseDeDatos.bd.Insertar(tabla, variables, val);
        }

        public string DarNombre(string id)
        {
            return (string)DarValor(id, 1);
        }

        public string DarDireccion(string id)
        {
            return (string)DarValor(id, 2);
        }

        public string DarTelefono(string id)
        {
            Contacto c = JsonConvert.DeserializeObject<Contacto>((string)DarValor(id, 3));
            return c.Telefono;
        }

        public Dictionary<string, string> DarCantidades(string id)
        {
            Dictionary<string, string> c = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)DarValor(id, 4));
            return c;
        }

        public string DarPais(string id)
        {
            Contacto c = JsonConvert.DeserializeObject<Contacto>((string)DarValor(id, 3));
            return c.Pais;
        }

        public string DarCiudad(string id)
        {
            Contacto c = JsonConvert.DeserializeObject<Contacto>((string)DarValor(id, 3));
            return c.Ciudad;
        }

        public int DarCantidad(string id, string id_producto)
        {
            Dictionary<string, string> d = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)DarValor(id, 4));
            string val = d[id_producto];
            return int.Parse(val);
        }

        public int Contar(string nombre)
        {
            nombre = nombre.ToLower();
            return BaseDeDatos.bd.Contar(tabla, variables[1] + "='" + nombre + "'");
        }

        private object DarValor(string id, int i)
        {
            return BaseDeDatos.bd.Seleccionar(tabla, variables, "" + variables[0] + "='" + id + "'")[i][0];
        }
    }
}
