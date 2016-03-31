using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Windows.Media;

namespace Aplicacion_YULI
{
    class Usuario
    {
        private string[] variables;
        private string tabla;

        public Usuario()
        {
            Inicializar();
        }

        public Usuario(object[] valores)
        {
            Inicializar();
            CrearNuevoUsuario(valores);
        }

        private void Inicializar()
        {
            tabla = "usuarios";
            variables = new string[] { "id", "clave", "nombre", "apellido", "ci", "profesion", "fecha", "permisos", "foto" };
        }

        public void CrearNuevoUsuario(object[] valores)
        {
            BaseDeDatos.bd.Insertar(tabla, variables, valores);
        }

        public string DarNombre(string id)
        {
            return (string)DarValor(id, 2);
        }

        public string DarApellido(string id)
        {
            return (string)DarValor(id, 3);
        }

        public string DarCedula(string id)
        {
            return (string)DarValor(id, 4);
        }

        public string DarProfesion(string id)
        {
            return (string)DarValor(id, 5);
        }

        public DateTime DarFechaDeNacimiento(string id)
        {
            return new MySqlDateTime((string)DarValor(id, 6)).GetDateTime();
        }

        public int DarTipoDePermiso(string id) 
        {
            return int.Parse((string)DarValor(id, 7));
        }
 
        public bool CompararClave(string id, string clave)
        {
            return clave.Equals((string)DarValor(id, 1));
        }

        public ImageSource DarFoto(string id)
        {
            Byte[] bytes = DarFotoBytes(id);
            MemoryStream ms = new MemoryStream(bytes);
            return (ImageSource)new ImageSourceConverter().ConvertFrom(ms);
        }

        private object DarValor(string id, int i)
        {
            return BaseDeDatos.bd.Seleccionar(tabla, variables, ""+variables[0]+"='" + id + "'")[i][0];
        }

        private Byte[] DarFotoBytes(string id)
        {
            return (Byte[])DarValor(id, 8);
        }
    }
}
