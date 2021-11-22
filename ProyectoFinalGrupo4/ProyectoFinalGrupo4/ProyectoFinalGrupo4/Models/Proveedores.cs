using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Models
{
    public class Proveedores : ContentPage
    {
        public int idProveedor { get; set; }
        public string nombre { get; set; }
        public string contacto { get; set; }
        public bool telefono { get; set; }
        public int correo { get; set; }
        public string direccion { get; set; }
        public string latitud { get; set; }
        public bool longitud { get; set; }
        public bool estado { get; set; }
    }
}