using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientePage : ContentPage
    {
        Clientes cliente = new Clientes();
        RepositoryClientes repository = new RepositoryClientes();
        List<Clientes> clientes = new List<Clientes>();
        int usuarioActual = 2, idCliente;
        bool estado;

        string identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton;

        public ClientePage()
        {
            InitializeComponent();

            //ESTO ES PARA LLAMAR A TODA LA LISTA
            clientes = repository.ListClientes();
            lstClientes.ItemsSource = clientes;

            txtIdCliente.Text = "1";
        }

        public ClientePage(int id, string identidad, string nombre, string apellido, string tel, string dire, double lat,
            double longi, string usu, string email, string button)
        {
            InitializeComponent();

            //ESTO ES PARA LLAMAR A TODA LA LISTA
            clientes = repository.ListClientes();
            lstClientes.ItemsSource = clientes;

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
            btnGuardar.Text = button;
        }

        private void btnGuardarCliente_Clicked(object sender, EventArgs e)
        {

            if (btnGuardar.Text == "Guardar Cliente" && Validaciones() == true)
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
            }else if (btnGuardar.Text == "Modificar Cliente" && Validaciones() == true)
            {
                //Modificar Cliente
                Clientes cliente = new Clientes(int.Parse(txtIdCliente.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                    txtTelefono.Text, txtNacimiento.Date, txtDireccion.Text, Double.Parse(txtLatitud.Text),
                    Double.Parse(txtLongitud.Text), usuarioActual, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);

                cliente.idCliente = int.Parse(txtIdCliente.Text);
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
                repository.UpdateCliente(cliente, usuarioActual);
                limpiar();
            }

            List<Clientes> listaClientes;
            listaClientes = repository.ListClientes();
            lstClientes.ItemsSource = listaClientes;
        }

        async private void btnAbrirMapa(object sender, EventArgs e){
            idCliente = int.Parse(txtIdCliente.Text);
            identificacion = txtIdentificacion.Text;
            nombres = txtNombres.Text;
            apellidos = txtApellidos.Text;
            telefono = txtTelefono.Text;
            //fechaNa = txtNacimiento.Date;
            direccion = txtDireccion.Text;
            //latitud = double.Parse(txtLatitud.Text);
            //longitud = double.Parse(txtLongitud.Text);
            usuario = txtUsuario.Text;
            correo = txtCorreo.Text;
            boton = btnGuardar.Text;
            //await Navigation.PushAsync(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton));

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Navigation.InsertPageBefore(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton, double.Parse(txtLatitud.Text), double.Parse(txtLongitud.Text)), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception ex)
            {
                Navigation.InsertPageBefore(new MapaPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var client = (sender as MenuItem).CommandParameter as Clientes;
            txtIdCliente.Text = client.idCliente.ToString();
            Clientes cliente = repository.BuscarCliente(client.idCliente);
            txtIdCliente.Text = cliente.idCliente.ToString();
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
            txtConfirmacion.Text = cliente.clave;
            estado = cliente.estado;
            btnGuardar.Text = "Modificar Cliente";
            if (estado == false) //Si el estado del Cliente es INACTIVO
            {
                btnDesactivar.Text = "Activar Cliente";
                btnDesactivar.BackgroundColor = Color.Green;
            }
            else
            {
                btnDesactivar.Text = "Desactivar Cliente";
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
            txtDireccion.Text = "";
            txtLatitud.Text = "";
            txtLongitud.Text = "";
            txtUsuario.Text = "";
            txtCorreo.Text = "";
            txtContrasenia.Text = "";
            txtConfirmacion.Text = "";
            btnGuardar.Text = "Guardar Cliente";
            btnDesactivar.BackgroundColor = Color.Red;
            btnDesactivar.IsVisible = false;
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Clientes> listaClientes;
            listaClientes = repository.ListClientes();
            lstClientes.ItemsSource = listaClientes;

            lstClientes.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                lstClientes.ItemsSource = listaClientes.Where(clientes => string.Equals(clientes.usuario, buscador, StringComparison.OrdinalIgnoreCase)).Where(clientes => clientes.usuario.ToUpper().Contains(buscador.ToUpper()));
                lstClientes.EndRefresh();
            }
            else
            {
                listaClientes = repository.ListClientes();
                lstClientes.ItemsSource = listaClientes;
                lstClientes.EndRefresh();
            }
        }
        async private void btnDesactivar_Clicked(object sender, EventArgs e)
        { 
            bool respuesta;
            if (btnDesactivar.Text == "Desactivar Cliente" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea desactivar al cliente? " + txtUsuario.Text, "Si", "No");
                if (respuesta == true)
                {
                    Clientes cliente = new Clientes(int.Parse(txtIdCliente.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                        txtTelefono.Text, txtNacimiento.Date, txtDireccion.Text, Double.Parse(txtLatitud.Text),
                        Double.Parse(txtLongitud.Text), usuarioActual, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", false);

                    repository.DeactivateCliente(cliente, usuarioActual);
                    btnDesactivar.Text = "Activar Cliente";
                    btnDesactivar.BackgroundColor = Color.Green;
                    limpiar();
                }
            }
            else if (btnDesactivar.Text == "Activar Cliente" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea activar al cliente? " + txtUsuario.Text, "Si", "No");
                if (respuesta == true)
                {
                    Clientes cliente = new Clientes(int.Parse(txtIdCliente.Text), txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text,
                        txtTelefono.Text, txtNacimiento.Date, txtDireccion.Text, Double.Parse(txtLatitud.Text),
                        Double.Parse(txtLongitud.Text), usuarioActual, txtUsuario.Text, txtCorreo.Text, txtContrasenia.Text, "", true);
                    repository.UpdateCliente(cliente, usuarioActual);
                    limpiar();
                }
            } 
        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(txtIdentificacion.Text) || string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtLatitud.Text) ||
            string.IsNullOrEmpty(txtLongitud.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text) || string.IsNullOrEmpty(txtConfirmacion.Text))
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