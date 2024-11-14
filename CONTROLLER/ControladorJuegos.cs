using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication41.Models.VideojuegosTableAdapters;


namespace WebApplication41.CONTROLLER
{
    public class ControladorJuegos
    {

        public bool AñadirJuego(string nombre, int cantidad, double costo, string imagenUrl)
        {
            try
            {
                using (videojuegosTableAdapter adaptadorJuegos = new videojuegosTableAdapter())
                {
                    adaptadorJuegos.AgregarVideojuegos (nombre, cantidad, Convert.ToDecimal(costo), imagenUrl);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Juego> ObtenerInventario()
        {
            try
            {
                using (videojuegosTableAdapter adaptadorJuegos = new videojuegosTableAdapter())
                {
                    var datos = adaptadorJuegos.GetData();
                    if (datos.Rows.Count > 0)
                    {
                        List<Juego> listaJuegos = new List<Juego>();
                        foreach (DataRow fila in datos.Rows)
                        {
                            Juego juego = new Juego
                            {
                                Nombre = fila["Nombre"].ToString(),
                                Cantidad = Convert.ToString(fila["Cantidad"]),
                                Costo = Convert.ToDouble(fila["precio"]),
                                UrlImagen = fila["Imagen"].ToString()
                            };
                            listaJuegos.Add(juego);
                        }
                        return listaJuegos;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el inventario de juegos", ex);
            }
        }
    }
}