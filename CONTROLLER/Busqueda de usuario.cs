using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication41.CONTROLLER
{
    public class Busqueda_de_usuario
    {
        public DataTable BuscarUsuario(string usuario)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["examen3pavConnectionString"].ConnectionString;
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand("SELECT id_usuario, nombre_usuario, correo_electronico, contrasena, fecha_creacion FROM usuarios WHERE nombre_usuario = @usuario", conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataTable tablaUsuario = new DataTable();
                adaptador.Fill(tablaUsuario);
                return tablaUsuario;
            }
        }
    }
}