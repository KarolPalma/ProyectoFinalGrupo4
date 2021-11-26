using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdministradorPage : ContentPage
    {
        RepositoryAdministradores repository = new RepositoryAdministradores();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        List<Administradores> administradores = new List<Administradores>();
        bool estado;

        public AdministradorPage()
        {
            InitializeComponent();

            //ESTO ES PARA LLAMAR A TODA LA LISTA
            administradores = repository.ListAdministradores();
            lstAdministradores.ItemsSource = administradores;
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar Administrador" && Validaciones() == true)
            {
                //Crear Nuevo
                Administradores administrador = new Administradores(txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                    txtTelefono.Text, txtNacimiento.Date, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, true);
                administrador.identificacion = txtIdentificacion.Text;
                administrador.nombres = txtNombres.Text;
                administrador.apellidos = txtApellidos.Text;
                administrador.telefono = txtTelefono.Text;
                administrador.fechaNacimiento = txtNacimiento.Date;
                administrador.usuario = txtUsuario.Text;
                administrador.correo = txtCorreo.Text;
                administrador.clave = txtContrasenia.Text;
                repository.InsertAdministrador(administrador, int.Parse(Preferences.Get("idUsuario", "")));
                limpiar();
            }
            else if (btnGuardar.Text == "Modificar Administrador" && Validaciones() == true)
            {
                //Modifica Administrador
                Administradores administrador = new Administradores(int.Parse(txtIdAdmin.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                    txtTelefono.Text, txtNacimiento.Date, 1, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);

                administrador.idAdmin = int.Parse(txtIdAdmin.Text);
                administrador.identificacion = txtIdentificacion.Text;
                administrador.nombres = txtNombres.Text;
                administrador.apellidos = txtApellidos.Text;
                administrador.telefono = txtTelefono.Text;
                administrador.fechaNacimiento = txtNacimiento.Date;
                administrador.usuario = txtUsuario.Text;
                administrador.correo = txtCorreo.Text;
                administrador.clave = txtContrasenia.Text;

                repository.UpdateAdministrador(administrador, int.Parse(Preferences.Get("idUsuario", "")));
                if (int.Parse(Preferences.Get("idSesionUsuario", "")) == administrador.idAdmin)
                {
                    repositorySesiones.UsuarioLogin(administrador.usuario, administrador.clave);
                }
                limpiar();
            }
            List<Administradores> listaAdminitradores;
            listaAdminitradores = repository.ListAdministradores();
            lstAdministradores.ItemsSource = listaAdminitradores;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var admin = (sender as MenuItem).CommandParameter as Administradores;
            Administradores administrador = repository.BuscarAdministrador(admin.idAdmin);
            txtIdAdmin.Text = administrador.idAdmin.ToString();
            txtIdentificacion.Text = administrador.identificacion;
            txtNombres.Text = administrador.nombres;
            txtApellidos.Text = administrador.apellidos;
            txtTelefono.Text = administrador.telefono;
            txtNacimiento.Date = administrador.fechaNacimiento;
            txtUsuario.Text = administrador.usuario;
            txtCorreo.Text = administrador.correo;
            txtContrasenia.Text = administrador.clave;
            txtConfirmacion.Text = administrador.clave;
            estado = administrador.estado;
            btnGuardar.Text = "Modificar Administrador";
            if (estado == false) //Si el estado del Administrador es INACTIVO
            {
                btnDesactivar.Text = "Activar Administrador";
                btnDesactivar.BackgroundColor = Color.Green;
            }
            else
            {
                btnDesactivar.Text = "Desactivar Administrador";
                btnDesactivar.BackgroundColor = Color.Red;
            }
            btnDesactivar.IsVisible = true;
        }

        private void limpiar()
        {
            txtIdentificacion.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtUsuario.Text = "";
            txtCorreo.Text = "";
            txtContrasenia.Text = "";
            txtConfirmacion.Text = "";
            btnGuardar.Text = "Guardar Administrador";
            btnDesactivar.BackgroundColor = Color.Red;
            btnDesactivar.IsVisible = false;
        }

        async private void btnDesactivar_Clicked(object sender, EventArgs e) //Desactiva o Activa al Administrador
        {
            bool respuesta;
            if (btnDesactivar.Text == "Desactivar Administrador" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea desactivar al administrador? " + txtUsuario.Text, "Si", "No");
                if (respuesta == true)
                {
                    Administradores administrador = new Administradores(int.Parse(txtIdAdmin.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                    txtTelefono.Text, txtNacimiento.Date, 1, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", false);
                    repository.DeactivateAdministrador(administrador, int.Parse(Preferences.Get("idUsuario", "")));
                    btnDesactivar.Text = "Activar Administrador";
                    btnDesactivar.BackgroundColor = Color.Green;
                    if (int.Parse(Preferences.Get("idSesionUsuario", "")) == administrador.idAdmin)
                    {
                        if(!repositorySesiones.UsuarioLogin(administrador.usuario, ""))
                        {
                            await Navigation.PushAsync(new LoginPage());
                        }
                    }
                    limpiar();
                }
            }
            else if (btnDesactivar.Text == "Activar Administrador" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea activar al administrador? " + txtUsuario.Text, "Si", "No");
                if (respuesta == true)
                {
                    Administradores administrador = new Administradores(int.Parse(txtIdAdmin.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                    txtTelefono.Text, txtNacimiento.Date, 1, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);
                    repository.UpdateAdministrador(administrador, int.Parse(Preferences.Get("idUsuario", "")));
                    limpiar();
                }
            }
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Administradores> listaAdministradores;
            listaAdministradores = repository.ListAdministradores();
            lstAdministradores.ItemsSource = listaAdministradores;

            lstAdministradores.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                lstAdministradores.ItemsSource = listaAdministradores.Where(administrador => string.Equals(administrador.usuario, buscador, StringComparison.OrdinalIgnoreCase)).Where(administrador => administrador.usuario.ToUpper().Contains(buscador.ToUpper()));
                lstAdministradores.EndRefresh();
            }
            else
            {
                listaAdministradores = repository.ListAdministradores();
                lstAdministradores.ItemsSource = listaAdministradores;
                lstAdministradores.EndRefresh();
            }
        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(txtIdentificacion.Text) || string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(txtTelefono.Text) ||
            string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text) || string.IsNullOrEmpty(txtConfirmacion.Text))
            {
                DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                respuesta = false;
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