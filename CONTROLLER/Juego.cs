using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication41.CONTROLLER
{
    public class Juego
    {
        public string Nombre { get; set; }
        public string Cantidad { get; set; }
        // Cambiar el nombre de Precio a ValorUnitario
        public double Costo { get; set; }
        public string UrlImagen { get; set; }

    }
}