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
    public partial class FacturasVentasPage : ContentPage
    {
        RepositoryClientes repoCliente = new RepositoryClientes();
        RepositoryAdministradores repoAdministrador = new RepositoryAdministradores();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        int idSessionUsuario;
        RepositoryFacturas repository = new RepositoryFacturas();
        List<Facturas> facturas = new List<Facturas>();
        public FacturasVentasPage()
        {
            InitializeComponent();
            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
            //ESTO ES PARA LLAMAR A TODA LA LISTA
            facturas = repository.ListFacturasVentas();
            lstFacturasVentas.ItemsSource = facturas;
        }

        async private void FacturasVentas_Clicked(object sender, EventArgs e)
        {
            Uri uriFacturaPDF = new Uri(EndPointsAPI.recuperarReporteVentasGet + "?idUsuario=" + idSessionUsuario, UriKind.Absolute);
            await AbrirBrowser(uriFacturaPDF).ConfigureAwait(false);
        }

        public async Task AbrirBrowser(Uri uri)
        {
            try
            {
                await DisplayAlert("Reporte del día", "La lista de facturas de venta del día se abrirá en su navegador para permitir la descarga", "Ok");
                await Navigation.PopToRootAsync();
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo abrir la lista de facturas de ventas del día en el navegador", "Ok");
            }
        }
    }
}