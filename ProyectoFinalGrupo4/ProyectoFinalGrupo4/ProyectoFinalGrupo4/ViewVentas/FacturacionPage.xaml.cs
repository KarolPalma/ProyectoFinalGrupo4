using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.ViewVentas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacturacionPage : ContentPage
    {
        RepositoryVentas repositoryVentas = new RepositoryVentas();
        RepositoryClientes repositoryClientes = new RepositoryClientes();
        public FacturacionPage(double total)
        {
            InitializeComponent();
            Clientes cliente = repositoryClientes.TraerClienteActual();
            txtNombreCliente.Text = cliente.nombres + " " + cliente.apellidos;
            txtCorreo.Text = cliente.correo;
            txtLatitud.Text = cliente.latitud + "";
            txtLongitud.Text = cliente.longitud + "";
            txtTotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(total));

            List<MetodosPago> metodosPago = new List<MetodosPago>();
            metodosPago = repositoryVentas.TraerMetodosPago();
            cmbMetodoPago.ItemsSource = metodosPago;
        }

        /*async private void btnAbrirMapa(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Navigation.InsertPageBefore(new MapaPage(double.Parse(txtLatitud.Text), double.Parse(txtLongitud.Text)), Navigation.NavigationStack[0]);
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
        }*/
    }
}