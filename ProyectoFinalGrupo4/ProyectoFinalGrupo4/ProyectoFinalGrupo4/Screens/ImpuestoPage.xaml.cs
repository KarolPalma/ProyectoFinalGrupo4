using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Models;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImpuestoPage : ContentPage
    {
        RepositoryImpuestos repository = new RepositoryImpuestos();
        List<Impuestos> impuestos = new List<Impuestos>();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        int idSessionUsuario;

        public ImpuestoPage()
        {
            InitializeComponent();

            //ESTO ES PARA LLAMAR A TODA LA LISTA
            impuestos = repository.ListImpuestos();
            lstImpuestos.ItemsSource = impuestos;

            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar Impuesto" && Validaciones() == true)
            {
                //Crear Nuevo
                Impuestos impuesto = new Impuestos(txtNombre.Text, double.Parse(txtValor.Text));
                impuesto.nombreImpuesto = txtNombre.Text;
                impuesto.valorImpuesto = double.Parse(txtValor.Text);
                repository.InsertImpuesto(impuesto, idSessionUsuario);
                limpiar();
            }
            else if (btnGuardar.Text == "Modificar Impuesto" && Validaciones() == true)
            {
                //Modifica Impuesto
                Impuestos impuesto = new Impuestos(int.Parse(txtIdImpuesto.Text), txtNombre.Text, double.Parse(txtValor.Text));
                impuesto.idImpuesto = int.Parse(txtIdImpuesto.Text);
                impuesto.nombreImpuesto = txtNombre.Text;
                impuesto.valorImpuesto = double.Parse(txtValor.Text);
                repository.UpdateImpuesto(impuesto, idSessionUsuario);
                limpiar();
            }
            List<Impuestos> listaImpuestos;
            listaImpuestos = repository.ListImpuestos();
            lstImpuestos.ItemsSource = listaImpuestos;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var impuesto = (sender as MenuItem).CommandParameter as Impuestos;
            Impuestos impuestos = repository.BuscarImpuesto(impuesto.idImpuesto);
            txtIdImpuesto.Text = impuestos.idImpuesto.ToString();
            txtNombre.Text = impuestos.nombreImpuesto;
            txtValor.Text = impuestos.valorImpuesto.ToString();
            btnGuardar.Text = "Modificar Impuesto";
        }

        private void limpiar()
        {
            txtIdImpuesto.Text = "";
            txtNombre.Text = "";
            txtValor.Text = "";
            btnGuardar.Text = "Guardar Impuesto";
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Impuestos> impuestos;
            impuestos = repository.ListImpuestos();
            lstImpuestos.ItemsSource = impuestos;

            lstImpuestos.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador) && buscador != "Ninguno")
            {
                lstImpuestos.ItemsSource = impuestos.Where(impuesto => string.Equals(impuesto.nombreImpuesto, buscador, StringComparison.OrdinalIgnoreCase)).Where(impuesto => impuesto.nombreImpuesto.ToUpper().Contains(buscador.ToUpper()));
                lstImpuestos.EndRefresh();
            }
            else
            {
                impuestos = repository.ListImpuestos();
                lstImpuestos.ItemsSource = impuestos;
                lstImpuestos.EndRefresh();
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