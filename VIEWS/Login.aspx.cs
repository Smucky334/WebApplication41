using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication41.CONTROLLER;

namespace WebApplication41.VIEWS
{
    public partial class Login : System.Web.UI.Page
    {
        AESCryptography aes = new AESCryptography();
        // Cadena de conexión a la base de datos
        //private string connectionString = ConfigurationManager.ConnectionStrings["videojuegosConnectionString"].ConnectionString;
        private string connectionString = ConfigurationManager.ConnectionStrings["examen3pavConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Por favor, ingresa tu usuario y contraseña.";
                return;
            }

            // Verificar si el usuario existe en la base de datos
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Usuario WHERE Usuario = @Usuario";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", username);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string encryptedPassword = reader["Contrasena"].ToString();

                    // Desencriptar la contraseña almacenada
                    string decryptedPassword = aes.Decrypt(encryptedPassword);

                    if (password == decryptedPassword)
                    {
                        // Contraseña correcta, redirigir al Index
                        Session["User"] = username;  // Guardar el usuario en sesión si lo deseas
                        Response.Redirect("Index.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Usuario o contraseña incorrectos.";
                    }
                }
                else
                {
                    lblMessage.Text = "Usuario no encontrado.";
                }
            }
        }
    }
}