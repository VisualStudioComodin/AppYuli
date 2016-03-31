using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Aplicacion_YULI
{
    class BaseDeDatos
    {

        private MySqlConnection conexion;
        private string conector;
        public static BaseDeDatos bd = new BaseDeDatos();

        public BaseDeDatos()
        {
            conexion = new MySqlConnection();
            conector = Properties.Settings.Default.sistema_yuliConnectionString;
        }

        private void Conectar()
        {
            try
            {
                conexion.ConnectionString = conector;
                conexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Desconectar()
        {
            conexion.Close();
        }

        public void Insertar(string tabla, string[] variables, object[] valor)
        {
            Conectar();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            StringBuilder val = new StringBuilder();
            StringBuilder var = new StringBuilder();
            for (int i = 0; i < valor.Length; i++)
            {
                var.Append(variables[i]);
                val.Append("@"+i);
                if (i < valor.Length - 1)
                {
                    val.Append(", ");
                    var.Append(", ");
                }
            }
            cmd.CommandText = "INSERT INTO " + tabla + "("+var.ToString()+") VALUES (" + val.ToString() + ")";
            cmd.Connection = conexion;
            for (int i = 0; i < valor.Length; i++)
            {
                cmd.Parameters.AddWithValue("@"+i, valor[i]);
            }
            cmd.ExecuteNonQuery();
            Desconectar();
        }

        public List<object>[] Seleccionar(string tabla, string[] variables, string condicion)
        {
            Conectar();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM " + tabla + " WHERE " + condicion;
            cmd.Connection = conexion;
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<object>[] list = new List<object>[variables.Length];
            for (int i = 0; i < variables.Length; i++)
                list[i] = new List<object>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                for (int i = 0; i < variables.Length; i++)
                {
                    list[i].Add(dataReader[variables[i]]);
                }
            }
            Desconectar();
            return list;
        }
    }
}
