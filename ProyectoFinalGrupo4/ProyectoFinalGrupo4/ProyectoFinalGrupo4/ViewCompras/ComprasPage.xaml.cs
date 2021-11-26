using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.ViewCompras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComprasPage : ContentPage
    {
        RepositoryVentas repositoryVentas = new RepositoryVentas();
        RepositoryCategorias repositoryCategorias = new RepositoryCategorias();

        public ComprasPage()
        {
            InitializeComponent();

            if (repositoryVentas.carritoLleno())
            {
                imgCarrito.Source = "cartPunto.png";
            }
            else
            {
                imgCarrito.Source = "cart.png";
            }

            List<Productos> productosTodos = repositoryVentas.verProductos();

            listViewProductos.ItemsSource = productosTodos;
            cmbCategoriasFiltrado.ItemsSource = repositoryCategorias.LlenarCategorias();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (repositoryVentas.carritoLleno())
            {
                imgCarrito.Source = "cartPunto.png";
            }
            else
            {
                imgCarrito.Source = "cart.png";
            }

            List<Productos> productosTodos = repositoryVentas.verProductos();
            listViewProductos.ItemsSource = productosTodos;
            cmbCategoriasFiltrado.ItemsSource = repositoryCategorias.LlenarCategorias();
        }

        private async void listViewProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos producto = (Productos)listViewProductos.SelectedItem;
            await Navigation.PushAsync(new DetallesProductoCompra(producto));
        }

        private async void imgCarrito_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPageCompra());
        }

        private void cmbCategoriasFiltrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            Categorias categoriaFiltro = (Categorias)cmbCategoriasFiltrado.SelectedItem;
            List<Productos> productosTodos = repositoryVentas.verProductos();
            List<Productos> productos = new List<Productos>();

            if (!(cmbCategoriasFiltrado.SelectedItem is null))
            {
                foreach (Productos producto in productosTodos)
                {
                    if (producto.idCategoria == categoriaFiltro.idCategoria)
                    {
                        productos.Add(producto);
                    }
                }

                listViewProductos.ItemsSource = productos;
            }

        }
    }
}