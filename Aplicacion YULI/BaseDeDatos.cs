using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

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
                val.Append("@" + i);
                if (i < valor.Length - 1)
                {
                    val.Append(", ");
                    var.Append(", ");
                }
            }
            cmd.CommandText = "INSERT INTO " + tabla + "(" + var.ToString() + ") VALUES (" + val.ToString() + ")";
            cmd.Connection = conexion;
            for (int i = 0; i < valor.Length; i++)
            {
                cmd.Parameters.AddWithValue("@" + i, valor[i]);
            }
            cmd.ExecuteNonQuery();
            Desconectar();
        }

        public int Contar(string tabla, string condicion)
        {
            string query = "SELECT Count(*) FROM " + tabla + " WHERE " + condicion;
            int Count = -1;
            Conectar();
            //Create Mysql Command
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar() + "");
            //close Connection
            Desconectar();
            return Count;
        }

        public void Actualizar(string tabla, string[] variables, object[] valores, string condicion)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < variables.Length; i++)
            {
                sb.Append(variables[i] + "=" + "@" + i + "");
                if (i < variables.Length - 1)
                    sb.Append(", ");
            }

            string query = "UPDATE "+tabla+" SET "+sb.ToString()+" WHERE "+condicion;

            //Open connection
            Conectar();
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = conexion;
                for (int i = 1; i < valores.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + i, valores[i]);
                }
                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
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

        public DataTable DarTabla(string tabla, string[] variables)
        {
            DataTable datos = new DataTable();
            StringBuilder var = new StringBuilder();
            for (int i = 0; i < variables.Length; i++)
            {
                var.Append(variables[i]);
                if (i < variables.Length - 1)
                {
                    var.Append(", ");
                }
            }
            Conectar();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT " + var.ToString() + " FROM " + tabla + " WHERE 1";
            cmd.Connection = conexion;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(datos);
            Desconectar();
            return datos;
        }
    }
}
