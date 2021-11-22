using Plugin.SharedTransitions;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.ViewVentas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentasPage : ContentPage
    {
        RepositoryVentas repositoryVentas = new RepositoryVentas();
        public VentasPage()
        {
            InitializeComponent();

            if (repositoryVentas.carritoLleno())
            {
                imgCarrito.Source = "cartPunto.png";
            }

            List<Productos> productos = repositoryVentas.verProductos();

            listViewProductos.ItemsSource = productos;
        }

        private async void listViewProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos producto = (Productos) listViewProductos.SelectedItem;
            await Navigation.PushAsync(new DetallesProducto(producto));
        }

        private async void imgCarrito_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
        }
    }
}