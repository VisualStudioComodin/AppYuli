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
    public class Usuario
    {
        private string[] variables;
        private string tabla;
        private string id;

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
            this.id = "";
            tabla = "usuarios";
            variables = new string[] { "id", "clave", "nombre", "apellido", "ci", "profesion", "fecha", "permisos", "foto" };
        }

        public bool IniciarSesion(string id, string clave)
        {
            bool res = this.CompararClave(id, clave);
            if (res)
                this.id = id;
            return res;
        }

        public bool SesionIniciada()
        {
            return !this.id.Equals("");
        }

        public void CerrarSesion()
        {
            this.id = "";
        }

        public void CrearNuevoUsuario(object[] valores)
        {
            BaseDeDatos.bd.Insertar(tabla, variables, valores);
        }

        public string DarNombre()
        {
            return (string)DarValor(id, 2);
        }

        public string DarApellido()
        {
            return (string)DarValor(id, 3);
        }

        public string DarCedula()
        {
            return (string)DarValor(id, 4);
        }

        public string DarProfesion()
        {
            return (string)DarValor(id, 5);
        }

        public DateTime DarFechaDeNacimiento()
        {
            return new MySqlDateTime((string)DarValor(id, 6)).GetDateTime();
        }

        public int DarTipoDePermiso() 
        {
            return int.Parse((string)DarValor(id, 7));
        }
 
        private bool CompararClave(string id, string clave)
        {
            return clave.Equals((string)DarValor(id, 1));
        }

        public ImageSource DarFoto()
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
