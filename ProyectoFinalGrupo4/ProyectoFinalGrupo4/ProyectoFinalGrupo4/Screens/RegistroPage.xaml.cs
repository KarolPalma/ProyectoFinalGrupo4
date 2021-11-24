using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        Clientes cliente = new Clientes();
        RepositoryClientes repository = new RepositoryClientes();
        List<Clientes> clientes = new List<Clientes>();
        int usuarioActual = 1, idCliente;

        string identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton;
        int registro = 1;

        public RegistroPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            txtIdCliente.Text = "1";
        }

        public RegistroPage(int id, string identidad, string nombre, string apellido, string tel, string dire, double lat,
            double longi, string usu, string email)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //ESTO ES PARA LLAMAR A TODA LA LISTA

            txtIdCliente.Text = id.ToString();
            txtIdentificacion.Text = identidad;
            txtNombres.Text = nombre;
            txtApellidos.Text = apellido;
            txtTelefono.Text = tel;
            txtDireccion.Text = dire;
            txtLatitud.Text = lat.ToString();
            txtLongitud.Text = longi.ToString();
            txtUsuario.Text = usu;
            txtCorreo.Text = email;
        }

        async private void btnGuardarCliente_Clicked(object sender, EventArgs e)
        {

            if (btnGuardar.Text == "Registrarse" && Validaciones() == true)
            {
                //Crear Nuevo
                Clientes cliente = new Clientes(txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                    txtTelefono.Text, txtNacimiento.Date, txtDireccion.Text, Double.Parse(txtLatitud.Text),
                    Double.Parse(txtLongitud.Text), txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);

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
                repository.InsertCliente(cliente, usuarioActual);
                limpiar();
                await Navigation.PushAsync(new LoginPage());
            }
        }

        async private void btnAbrirMapa(object sender, EventArgs e)
        {
            idCliente = int.Parse(txtIdCliente.Text);
            identificacion = txtIdentificacion.Text;
            nombres = txtNombres.Text;
            apellidos = txtApellidos.Text;
            telefono = txtTelefono.Text;
            direccion = txtDireccion.Text;
            usuario = txtUsuario.Text;
            correo = txtCorreo.Text;
            boton = btnGuardar.Text;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Navigation.InsertPageBefore(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, registro, double.Parse(txtLatitud.Text), double.Parse(txtLongitud.Text)), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, registro), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception ex)
            {
                Navigation.InsertPageBefore(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, registro), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
            }
        }

        private void limpiar()
        {
            txtIdentificacion.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtLatitud.Text = "";
            txtLongitud.Text = "";
            txtUsuario.Text = "";
            txtCorreo.Text = "";
            txtContrasenia.Text = "";
            txtConfirmacion.Text = "";
            btnGuardar.Text = "Registrarse";
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