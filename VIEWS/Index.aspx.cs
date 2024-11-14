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
using WebApplication41.CONTROLLER;

namespace WebApplication41.VIEWS
{
    public partial class Index : System.Web.UI.Page
    {
        // private string connectionString = ConfigurationManager.ConnectionStrings["videojuegosConnectionString"].ConnectionString;
        // Obtener la cadena de conexión desde el archivo de configuración
        // private string connectionString = ConfigurationManager.ConnectionStrings["videojuegosConnectionString"].ConnectionString;
        private string connectionString = ConfigurationManager.ConnectionStrings["examen3pavConnectionString"].ConnectionString;
        //private string connectionString = ConfigurationManager.ConnectionStrings["videojuegospavConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarJuegos();
            if (Session["UsuarioActual"] != null && Session["UsuarioActual"].ToString() == "jchavarin")
            {
                lblUsuario.Text = Session["UsuarioActual"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Por favor, inicia sesión.'); window.location.href='Login.aspx';</script>");
                Response.End();
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            int cantidad;
            decimal costo;
            string urlImagen = null;

            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                Response.Write("<script>alert('Cantidad inválida.');</script>");
                return;
            }

            if (!decimal.TryParse(txtCosto.Text, out costo))
            {
                Response.Write("<script>alert('Costo inválido.');</script>");
                return;
            }

            if (fileImagen.HasFile)
            {
                string nombreArchivo = Path.GetFileName(fileImagen.PostedFile.FileName);
                string rutaArchivo = Server.MapPath("~/Images/") + nombreArchivo;
                try
                {
                    fileImagen.SaveAs(rutaArchivo);
                    urlImagen = "~/Images/" + nombreArchivo;
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al cargar la imagen: " + ex.Message + "');</script>");
                    return;
                }
            }

            ControladorJuegos controlador = new ControladorJuegos();
            try
            {
                bool resultado = controlador.AñadirJuego(nombre, cantidad, (double)costo, urlImagen);
                if (resultado)
                {
                    Response.Write("<script>alert('Juego añadido exitosamente.');</script>");
                    CargarJuegos();
                }
                else
                {
                    Response.Write("<script>alert('Error al añadir el juego.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        private void CargarJuegos()
        {
            ControladorJuegos controlador = new ControladorJuegos();
            var inventario = controlador.ObtenerInventario();
            gvJuegos.DataSource = inventario;
            gvJuegos.DataBind();
        }
    }
}
