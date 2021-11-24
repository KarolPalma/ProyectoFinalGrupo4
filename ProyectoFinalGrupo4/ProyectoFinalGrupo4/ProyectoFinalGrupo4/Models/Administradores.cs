using System;

namespace ProyectoFinalGrupo4.Models
{
    public class Administradores
    {
        public int idAdmin { get; set; }
        public string identificacion { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; } //Al enviar a la BD usar .Date (Solo la fecha)

        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string token { get; set; }
        public bool estado { get; set; }


        public Administradores()
        {

        }

        public Administradores(string identificacion, string nombres, string apellidos, string telefono, DateTime fechaNacimiento, string usuario, string correo, string clave, bool estado)
        {
            this.identificacion = identificacion;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.fechaNacimiento = fechaNacimiento;
            this.usuario = usuario;
            this.correo = correo;
            this.clave = clave;
            this.estado = estado;
        }

        public Administradores(int idAdmin, string identificacion, string nombres, string apellidos, string telefono, DateTime fechaNacimiento, int idUsuario, string usuario, string correo, string clave, string token, bool estado)
        {
            this.idAdmin = idAdmin;
            this.identificacion = identificacion;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.fechaNacimiento = fechaNacimiento;
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.correo = correo;
            this.clave = clave;
            this.token = token;
            this.estado = estado;
        }
    }
}