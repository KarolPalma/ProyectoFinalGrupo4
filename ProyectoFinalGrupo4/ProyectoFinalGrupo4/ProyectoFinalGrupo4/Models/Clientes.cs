using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Models
{
    public class Clientes
    {
        public int idCliente { get; set; }
        public string identificacion { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; } //Al enviar a la BD usar .Date (Solo la fecha)
        public String direccion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }

        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string token { get; set; }
        public bool estado { get; set; }


        public Clientes()
        {

        }

        public Clientes(string identificacion, string nombres, string apellidos, string telefono, DateTime fechaNacimiento, string direccion, double latitud, double longitud, string usuario, string correo, string clave, string token, bool estado)
        {
            this.identificacion = identificacion;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.fechaNacimiento = fechaNacimiento;
            this.direccion = direccion;
            this.latitud = latitud;
            this.longitud = longitud;
            this.usuario = usuario;
            this.correo = correo;
            this.clave = clave;
            this.token = token;
            this.estado = estado;
        }

        public Clientes(int idCliente, string identificacion, string nombres, string apellidos, string telefono, DateTime fechaNacimiento, string direccion, double latitud, double longitud, int idUsuario, string usuario, string correo, string clave, string token, bool estado)
        {
            this.idCliente = idCliente;
            this.identificacion = identificacion;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.fechaNacimiento = fechaNacimiento;
            this.direccion = direccion;
            this.latitud = latitud;
            this.longitud = longitud;
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.correo = correo;
            this.clave = clave;
            this.token = token;
            this.estado = estado;
        }
    }
}