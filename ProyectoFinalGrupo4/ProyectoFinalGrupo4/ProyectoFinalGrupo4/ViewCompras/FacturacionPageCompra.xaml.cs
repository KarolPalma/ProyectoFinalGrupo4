using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.ViewCompras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacturacionPageCompra : ContentPage
    {
        RepositoryVentas repositoryVentas = new RepositoryVentas();
        RepositoryAdministradores repositoryAdministradores = new RepositoryAdministradores();
        double totalGlobal;

        public FacturacionPageCompra(double total)
        {
            InitializeComponent();
            totalGlobal = total;
            Administradores administrador = repositoryAdministradores.TraerAdminActual();
            txtNombreAdmin.Text = administrador.nombres + " " + administrador.apellidos;
            txtCorreo.Text = administrador.correo;
            txtLatitud.Text = "14.1";
            txtLongitud.Text = "-87.2167";
            txtTotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(total));

            List<MetodosPago> metodosPago = new List<MetodosPago>();
            metodosPago = repositoryVentas.TraerMetodosPago();
            cmbMetodoPago.ItemsSource = metodosPago;
        }

        public FacturacionPageCompra(double latitud, double longitud, double total)
        {
            InitializeComponent();
            totalGlobal = total;
            Administradores administrador = repositoryAdministradores.TraerAdminActual();
            txtNombreAdmin.Text = administrador.nombres + " " + administrador.apellidos;
            txtCorreo.Text = administrador.correo;
            txtLatitud.Text = "14.1";
            txtLongitud.Text = "-87.2167";
            txtTotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(total));

            List<MetodosPago> metodosPago = new List<MetodosPago>();
            metodosPago = repositoryVentas.TraerMetodosPago();
            cmbMetodoPago.ItemsSource = metodosPago;
        }

        private void Button_Back(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        async private void btnMapa_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Navigation.InsertPageBefore(new MapaPage(location.Latitude, location.Longitude, totalGlobal), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new MapaPage(double.Parse(txtLatitud.Text), double.Parse(txtLongitud.Text), totalGlobal), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception ex)
            {
                Navigation.InsertPageBefore(new MapaPage(double.Parse(txtLatitud.Text), double.Parse(txtLongitud.Text), totalGlobal), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
            }
        }

        private async void btnFinalizarCompra(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(Preferences.Get("idUsuario", "0"));
            double latitud = double.Parse(txtLatitud.Text);
            double longitud = double.Parse(txtLongitud.Text);

            MetodosPago metodosPago = new MetodosPago();

            if (cmbMetodoPago.SelectedItem is null)
            {
                await DisplayAlert("Método de Pago", "Por favor, seleccione un método de pago", "Ok");
            }
            else
            {
                metodosPago = (MetodosPago)cmbMetodoPago.SelectedItem;
                RepositoryVentas repositoryVentas = new RepositoryVentas();
                int idFactura = await repositoryVentas.FacturarCompra(idUsuario, latitud, longitud, metodosPago.idMetodoPago);

                Uri uriFacturaPDF = new Uri(EndPointsAPI.recuperarFacturaCompraGet + "?idFactura=" + idFactura, UriKind.Absolute);
                await AbrirBrowser(uriFacturaPDF).ConfigureAwait(false);

            }

        }

        public async Task AbrirBrowser(Uri uri)
        {
            try
            {
                await DisplayAlert("Factura Final", "La factura se abrirá en su navegador para permitir la descarga", "Ok");
                await Navigation.PopToRootAsync();
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo abrir la factura en el navegador", "Ok");
            }
        }
    }
}