using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Models;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class PerfilPage : ContentPage
    {
        RepositoryClientes repoCliente = new RepositoryClientes();
        RepositoryAdministradores repoAdministrador = new RepositoryAdministradores();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        String idUsuario, identificacion, nombres, apellidos, telefono, direccion, usuarioM, correo;
        int idSessionUsuario, idUsuarioM, registro = 2;
        double latitud, longitud;
        public PerfilPage()
        {
            InitializeComponent();
            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
            ActividadRol(rol, idSessionUsuario);
        }

        public PerfilPage(double latitud, double longitud)
        {
            InitializeComponent();
            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
            ActividadRol(rol, idSessionUsuario);
            txtLatitud.Text = latitud.ToString();
            txtLongitud.Text = longitud.ToString();
        }

        public void ActividadRol(String rol, int idSUsuario)
        {
            if (int.Parse(rol) == 1) //Administrador
            {
                lblDirecccion.IsVisible = false;
                txtDireccion.IsVisible = false;
                lblLatitud.IsVisible = false;
                txtLatitud.IsVisible = false;
                lblLongitud.IsVisible = false;
                txtLongitud.IsVisible = false;
                btnMapa.IsVisible = false;

                Administradores administrador = repoAdministrador.BuscarAdministrador(idSUsuario);
                txtIdUsuario.Text = administrador.idAdmin.ToString();
                txtIdentificacion.Text = administrador.identificacion;
                txtNombres.Text = administrador.nombres;
                txtApellidos.Text = administrador.apellidos;
                txtTelefono.Text = administrador.telefono;
                txtNacimiento.Date = administrador.fechaNacimiento;
                txtUsuario.Text = administrador.usuario;
                txtCorreo.Text = administrador.correo;
                txtContrasenia.Text = administrador.clave;
            }
            else if (int.Parse(rol) == 2) //Cliente
            {
                Clientes cliente = repoCliente.BuscarCliente(idSUsuario);
                txtIdUsuario.Text = cliente.idCliente.ToString();
                txtIdentificacion.Text = cliente.identificacion;
                txtNombres.Text = cliente.nombres;
                txtApellidos.Text = cliente.apellidos;
                txtTelefono.Text = cliente.telefono;
                txtNacimiento.Date = cliente.fechaNacimiento;
                txtDireccion.Text = cliente.direccion;
                txtLatitud.Text = cliente.latitud.ToString();
                txtLongitud.Text = cliente.longitud.ToString();
                txtUsuario.Text = cliente.usuario;
                txtCorreo.Text = cliente.correo;
                txtContrasenia.Text = cliente.clave;
            }
        }

        async private void btnAbrirMapa(object sender, EventArgs e)
        {
            idUsuarioM = int.Parse(txtIdUsuario.Text);
            identificacion = txtIdentificacion.Text;
            nombres = txtNombres.Text;
            apellidos = txtApellidos.Text;
            latitud = double.Parse(txtLatitud.Text);
            longitud = double.Parse(txtLongitud.Text);
            telefono = txtTelefono.Text;
            direccion = txtDireccion.Text;
            usuarioM = txtUsuario.Text;
            correo = txtCorreo.Text;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Navigation.InsertPageBefore(new MapaPage(nombres, apellidos, registro, latitud, longitud), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new MapaPage(nombres, apellidos, registro, latitud, longitud), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception ex)
            {
                Navigation.InsertPageBefore(new MapaPage(nombres, apellidos, registro, latitud, longitud), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
            }
        }

        private void btnModificar_Clicked(object sender, EventArgs e)
        {
            if (int.Parse(rol) == 1) //Administrador
            {
                if (Validaciones())
                {
                    Administradores administrador = new Administradores(int.Parse(txtIdUsuario.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                        txtTelefono.Text, txtNacimiento.Date, idSessionUsuario, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);

                    administrador.idAdmin = int.Parse(txtIdUsuario.Text);
                    administrador.identificacion = txtIdentificacion.Text;
                    administrador.nombres = txtNombres.Text;
                    administrador.apellidos = txtApellidos.Text;
                    administrador.telefono = txtTelefono.Text;
                    administrador.fechaNacimiento = txtNacimiento.Date;
                    administrador.idUsuario = idSessionUsuario;
                    administrador.usuario = txtUsuario.Text;
                    administrador.correo = txtCorreo.Text;
                    administrador.clave = txtContrasenia.Text;
                    repoAdministrador.UpdateAdministrador(administrador, idSessionUsuario);

                }
            }else if (int.Parse(rol) == 2) //Cliente
            {
                if (Validaciones())
                {
                    Clientes cliente = new Clientes(int.Parse(txtIdUsuario.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                        txtTelefono.Text, txtNacimiento.Date, txtDireccion.Text, Double.Parse(txtLatitud.Text),
                        Double.Parse(txtLongitud.Text), idSessionUsuario, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);
                    cliente.idCliente = int.Parse(txtIdUsuario.Text);
                    cliente.identificacion = txtIdentificacion.Text;
                    cliente.nombres = txtNombres.Text;
                    cliente.apellidos = txtApellidos.Text;
                    cliente.telefono = txtTelefono.Text;
                    cliente.fechaNacimiento = txtNacimiento.Date;
                    cliente.direccion = txtDireccion.Text;
                    cliente.latitud = Double.Parse(txtLatitud.Text);
                    cliente.longitud = Double.Parse(txtLongitud.Text);
                    cliente.usuario = txtUsuario.Text;
                    cliente.correo = txtCorreo.Text;
                    cliente.clave = txtContrasenia.Text;
                    repoCliente.UpdateCliente(cliente, idSessionUsuario);
                }
            }

        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (int.Parse(rol) == 1) //Administrador
            {
                if (string.IsNullOrEmpty(txtIdentificacion.Text) || string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(txtTelefono.Text)
                 || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text) || string.IsNullOrEmpty(txtConfirmacion.Text))
                {
                    DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                    respuesta = false;
                }
            }
            else if(int.Parse(rol) == 2) //Cliente
            {
                if (string.IsNullOrEmpty(txtIdentificacion.Text) || string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtLatitud.Text) ||
                string.IsNullOrEmpty(txtLongitud.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text) || string.IsNullOrEmpty(txtConfirmacion.Text))
                {
                    DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                    respuesta = false;
                }
            }
            else if (!txtConfirmacion.Text.Equals(txtContrasenia.Text))
            {
                DisplayAlert("Llenado de Datos", "La confirmación de la contraseña es incorrecta", "Ok");
                respuesta = false;
            }

            return respuesta;
        }
    }
}