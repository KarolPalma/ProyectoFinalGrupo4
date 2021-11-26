using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProveedorPage : ContentPage
    {
        RepositoryProveedores repository = new RepositoryProveedores();
        List<Proveedores> proveedores = new List<Proveedores>();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        int idSessionUsuario;
        bool estado;

        public ProveedorPage()
        {
            InitializeComponent();
            //ESTO ES PARA LLAMAR A TODA LA LISTA
            proveedores = repository.ListProveedor();
            lstProveedores.ItemsSource = proveedores;

            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar Proveedor" && Validaciones() == true)
            {
                //Crear Nuevo
                Proveedores proveedor = new Proveedores(txtNombre.Text, txtContacto.Text, txtTelefono.Text, txtCorreo.Text, txtDireccion.Text);
                proveedor.nombre = txtNombre.Text;
                proveedor.contacto = txtContacto.Text;
                proveedor.telefono = txtTelefono.Text;
                proveedor.correo = txtCorreo.Text;
                proveedor.direccion = txtDireccion.Text;
                repository.InsertProveedor(proveedor, idSessionUsuario);
                limpiar();
            }
            else if (btnGuardar.Text == "Modificar Proveedor" && Validaciones() == true)
            {
                //Modifica Proveedor
                Proveedores proveedor = new Proveedores(int.Parse(txtIdProveedor.Text), txtNombre.Text, txtContacto.Text, txtTelefono.Text, txtCorreo.Text, txtDireccion.Text, true);

                proveedor.idProveedor = int.Parse(txtIdProveedor.Text);
                proveedor.nombre = txtNombre.Text;
                proveedor.contacto = txtContacto.Text;
                proveedor.telefono = txtTelefono.Text;
                proveedor.correo = txtCorreo.Text;
                proveedor.direccion = txtDireccion.Text;
                repository.UpdateProveedor(proveedor, idSessionUsuario);
                limpiar();
            }
            List<Proveedores> listaProveedores;
            listaProveedores = repository.ListProveedor();
            lstProveedores.ItemsSource = listaProveedores;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var provedor = (sender as MenuItem).CommandParameter as Proveedores;
            Proveedores proveedor = repository.BuscarProveedor(provedor.idProveedor);
            txtIdProveedor.Text = proveedor.idProveedor.ToString();
            txtNombre.Text = proveedor.nombre;
            txtContacto.Text = proveedor.contacto;
            txtTelefono.Text = proveedor.telefono;
            txtCorreo.Text = proveedor.correo;
            txtDireccion.Text = provedor.direccion;
            estado = proveedor.estado;
            btnGuardar.Text = "Modificar Proveedor";
            if (estado == false) //Si el estado del Proveedor es INACTIVO
            {
                btnDesactivar.Text = "Activar Proveedor";
                btnDesactivar.BackgroundColor = Color.Green;
            }
            else
            {
                btnDesactivar.Text = "Desactivar Proveedor";
                btnDesactivar.BackgroundColor = Color.Red;
            }
            btnDesactivar.IsVisible = true;
        }

        private void limpiar()
        {
            txtIdProveedor.Text = "";
            txtNombre.Text = "";
            txtContacto.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            btnGuardar.Text = "Guardar Proveedor";
            btnDesactivar.BackgroundColor = Color.Red;
            btnDesactivar.IsVisible = false;
        }

        async private void btnDesactivar_Clicked(object sender, EventArgs e) //Desactiva o Activa al Proveedor
        {
            bool respuesta;
            if (btnDesactivar.Text == "Desactivar Proveedor" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea desactivar al proveedor? " + txtNombre.Text, "Si", "No");
                if (respuesta == true)
                {
                    Proveedores proveedor = new Proveedores(int.Parse(txtIdProveedor.Text), txtNombre.Text, txtContacto.Text, txtTelefono.Text, txtCorreo.Text, txtDireccion.Text, false);
                    repository.DeactivateProveedor(proveedor, idSessionUsuario);
                    btnDesactivar.Text = "Activar Proveedor";
                    btnDesactivar.BackgroundColor = Color.Green;
                    limpiar();
                }
            }
            else if (btnDesactivar.Text == "Activar Proveedor" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea activar al proveedor? " + txtNombre.Text, "Si", "No");
                if (respuesta == true)
                {
                    Proveedores proveedor = new Proveedores(int.Parse(txtIdProveedor.Text), txtNombre.Text, txtContacto.Text, txtTelefono.Text, txtCorreo.Text, txtDireccion.Text, true);
                    repository.UpdateProveedor(proveedor, idSessionUsuario);
                    limpiar();
                }
            }
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Proveedores> listaProveedores;
            listaProveedores = repository.ListProveedor();
            lstProveedores.ItemsSource = listaProveedores;

            lstProveedores.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                lstProveedores.ItemsSource = listaProveedores.Where(proveedor => string.Equals(proveedor.nombre, buscador, StringComparison.OrdinalIgnoreCase)).Where(proveedor => proveedor.nombre.ToUpper().Contains(buscador.ToUpper()));
                lstProveedores.EndRefresh();
            }
            else
            {
                listaProveedores = repository.ListProveedor();
                lstProveedores.ItemsSource = listaProveedores;
                lstProveedores.EndRefresh();
            }
        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtContacto.Text) || string.IsNullOrEmpty(txtTelefono.Text) ||
            string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtDireccion.Text))
            {
                DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                respuesta = false;
            }

            return respuesta;
        }

    }
}