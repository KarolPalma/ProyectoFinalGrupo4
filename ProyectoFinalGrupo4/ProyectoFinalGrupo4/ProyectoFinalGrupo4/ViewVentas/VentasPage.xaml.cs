using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.ViewVentas;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentasPage : ContentPage
    {
        RepositoryVentas repositoryVentas = new RepositoryVentas();
        RepositoryCategorias repositoryCategorias = new RepositoryCategorias();

        public VentasPage()
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
            List<Productos> productos = new List<Productos>();

            foreach (Productos producto in productosTodos)
            {
                if (producto.unidadesAlmacen > 0)
                {
                    productos.Add(producto);
                }
            }

            listViewProductos.ItemsSource = productos;
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
            List<Productos> productos = new List<Productos>();

            foreach (Productos producto in productosTodos)
            {
                if (producto.unidadesAlmacen > 0)
                {
                    productos.Add(producto);
                }
            }

            listViewProductos.ItemsSource = productos;
            cmbCategoriasFiltrado.ItemsSource = repositoryCategorias.LlenarCategorias();
        }

        private async void listViewProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos producto = (Productos)listViewProductos.SelectedItem;
            await Navigation.PushAsync(new DetallesProducto(producto));
        }

        private async void imgCarrito_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
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
                    if (producto.unidadesAlmacen > 0 && producto.idCategoria == categoriaFiltro.idCategoria)
                    {
                        productos.Add(producto);
                    }
                }

                listViewProductos.ItemsSource = productos;
            }

        }
    }
}