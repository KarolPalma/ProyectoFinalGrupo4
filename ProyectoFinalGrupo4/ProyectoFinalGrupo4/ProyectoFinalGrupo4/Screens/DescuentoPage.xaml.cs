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
    public partial class DescuentoPage : ContentPage
    {
        RepositoryDescuentos repository = new RepositoryDescuentos();
        List<Descuentos> descuentos = new List<Descuentos>();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        int idSessionUsuario;
        public DescuentoPage()
        {
            InitializeComponent();

            //ESTO ES PARA LLAMAR A TODA LA LISTA
            descuentos = repository.ListDescuentos();
            lstDescuentos.ItemsSource = descuentos;

            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar Descuento" && Validaciones() == true)
            {
                //Crear Nuevo
                Descuentos descuento = new Descuentos(txtNombre.Text, double.Parse(txtValor.Text));
                descuento.nombreDescuento = txtNombre.Text;
                descuento.valorDescuento = double.Parse(txtValor.Text);
                repository.InsertDescuento(descuento, idSessionUsuario);
                limpiar();
            }
            else if (btnGuardar.Text == "Modificar Descuento" && Validaciones() == true)
            {
                //Modifica Descuento
                Descuentos descuento = new Descuentos(int.Parse(txtIdDescuento.Text), txtNombre.Text, double.Parse(txtValor.Text));
                descuento.idDescuento = int.Parse(txtIdDescuento.Text);
                descuento.nombreDescuento = txtNombre.Text;
                descuento.valorDescuento = double.Parse(txtValor.Text);
                repository.UpdateDescuento(descuento, idSessionUsuario);
                limpiar();
            }
            List<Descuentos> listaDescuentos;
            listaDescuentos = repository.ListDescuentos();
            lstDescuentos.ItemsSource = listaDescuentos;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var descuento = (sender as MenuItem).CommandParameter as Descuentos;
            Descuentos descuentos = repository.BuscarDescuento(descuento.idDescuento);
            txtIdDescuento.Text = descuentos.idDescuento.ToString();
            txtNombre.Text = descuentos.nombreDescuento;
            txtValor.Text = descuentos.valorDescuento.ToString();
            btnGuardar.Text = "Modificar Descuento";
        }

        private void limpiar()
        {
            txtIdDescuento.Text = "";
            txtNombre.Text = "";
            txtValor.Text = "";
            btnGuardar.Text = "Guardar Descuento";
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Descuentos> descuentos;
            descuentos = repository.ListDescuentos();
            lstDescuentos.ItemsSource = descuentos;

            lstDescuentos.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador) && buscador != "Ninguno")
            {
                lstDescuentos.ItemsSource = descuentos.Where(descuento => string.Equals(descuento.nombreDescuento, buscador, StringComparison.OrdinalIgnoreCase)).Where(descuento => descuento.nombreDescuento.ToUpper().Contains(buscador.ToUpper()));
                lstDescuentos.EndRefresh();
            }
            else
            {
                descuentos = repository.ListDescuentos();
                lstDescuentos.ItemsSource = descuentos;
                lstDescuentos.EndRefresh();
            }
        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtValor.Text))
            {
                DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                respuesta = false;
            }
            return respuesta;
        }
    }
}