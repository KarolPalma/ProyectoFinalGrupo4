
using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Models
{
    public class Proveedores : ContentPage
    {
        public int idProveedor { get; set; }
        public string nombre { get; set; }
        public string contacto { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public bool estado { get; set; }

        public Proveedores()
        {
        }

        public Proveedores(string nombre, string contacto, string telefono, string correo, string direccion)
        {
            this.nombre = nombre;
            this.contacto = contacto;
            this.telefono = telefono;
            this.correo = correo;
            this.direccion = direccion;
        }

        public Proveedores(int idProveedor, string nombre, string contacto, string telefono, string correo, string direccion, bool estado)
        {
            this.idProveedor = idProveedor;
            this.nombre = nombre;
            this.contacto = contacto;
            this.telefono = telefono;
            this.correo = correo;
            this.direccion = direccion;
            this.estado = estado;
        }
    }
}