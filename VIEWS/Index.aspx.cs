using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication41.VIEWS
{
    public partial class Index : System.Web.UI.Page
    {
        // private string connectionString = ConfigurationManager.ConnectionStrings["videojuegosConnectionString"].ConnectionString;
        // Obtener la cadena de conexión desde el archivo de configuración
        // private string connectionString = ConfigurationManager.ConnectionStrings["videojuegosConnectionString"].ConnectionString;
        private string connectionString = ConfigurationManager.ConnectionStrings["examen3pavConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            int cantidad;
            decimal precio;

            // Validar los campos de cantidad y costo
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                Response.Write("<script>alert('Por favor ingrese una cantidad válida.');</script>");
                return;
            }

            if (!decimal.TryParse(txtCosto.Text, out precio))
            {
                Response.Write("<script>alert('Por favor ingrese un costo válido.');</script>");
                return;
            }

            // Subir la imagen si existe
            string imagenUrl = null;
            if (fileImagen.HasFile)
            {
                string fileName = Path.GetFileName(fileImagen.PostedFile.FileName);
                string filePath = Server.MapPath("~/Images/") + fileName;

                try
                {
                    fileImagen.SaveAs(filePath);
                    imagenUrl = "~/Images/" + fileName; // Ruta de la imagen
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al subir la imagen: " + ex.Message + "');</script>");
                    return;
                }
            }

            // Insertar el producto en la base de datos MySQL
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO productos (Nombre, Cantidad, Precio, ImagenUrl) VALUES (@Nombre, @Cantidad, @Precio, @ImagenUrl)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@ImagenUrl", imagenUrl ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Actualizar la GridView con los productos más recientes
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al agregar el producto: " + ex.Message + "');</script>");
                }
            }
        }

        // Método para cargar los productos desde la base de datos y enlazarlos a la GridView
        private void CargarProductos()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM productos";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvProductos.DataSource = dt;
                    gvProductos.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al cargar los productos: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
