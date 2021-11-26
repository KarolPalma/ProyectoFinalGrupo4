using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Dependencies;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacturasUsuario : ContentPage
    {
        RepositoryClientes repoCliente = new RepositoryClientes();
        RepositoryAdministradores repoAdministrador = new RepositoryAdministradores();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        int idSessionUsuario;
        RepositoryFacturas repository = new RepositoryFacturas();
        List<Facturas> facturas = new List<Facturas>();

        public FacturasUsuario()
        {
            InitializeComponent();
            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
            //ESTO ES PARA LLAMAR A TODA LA LISTA SI ES CLIENTE O ADMINISTRADOR

            if (int.Parse(rol) == 1)
            {
                facturas = repository.ListFacturasComprasUsuario();
                lstMisFacturas.ItemsSource = facturas;
            }
            else
            {
                facturas = repository.ListFacturasVentasUsuario();
                lstMisFacturas.ItemsSource = facturas;
            }
        }

        async private void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            Facturas item = (Facturas)menu.BindingContext;
            int idFactura = (int)item.idFactura;
            if (int.Parse(rol) == 1)
            {
                Uri uriFacturaPDF = new Uri(EndPointsAPI.recuperarFacturaCompraGet + "?idFactura=" + idFactura, UriKind.Absolute);
                await AbrirBrowser(uriFacturaPDF).ConfigureAwait(false);
            }
            else
            {
                Uri uriFacturaPDF = new Uri(EndPointsAPI.recuperarFacturaGet + "?idFactura=" + idFactura, UriKind.Absolute);
                await AbrirBrowser(uriFacturaPDF).ConfigureAwait(false);
            }
        }

        public async Task AbrirBrowser(Uri uri)
        {
            try
            {
                await DisplayAlert("Mi Factura", "La factura se abrirá en su navegador para permitir la descarga", "Ok");
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