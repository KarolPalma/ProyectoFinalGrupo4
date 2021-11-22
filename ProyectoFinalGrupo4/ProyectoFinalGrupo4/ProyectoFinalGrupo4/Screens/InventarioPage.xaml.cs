using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Models;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventarioPage : ContentPage
    {
        RepositoryProductos repository = new RepositoryProductos();
        List<Productos> productos = new List<Productos>();

        public InventarioPage()
        {
            InitializeComponent();
            //ESTO ES PARA LLAMAR A TODA LA LISTA
            productos = repository.ListProductos();
            lstProductos.ItemsSource = productos;
        }


        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Productos> listaProductos;
            listaProductos = repository.ListProductos();
            lstProductos.ItemsSource = listaProductos;

            lstProductos.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                lstProductos.ItemsSource = listaProductos.Where(producto => string.Equals(producto.nombre, buscador, StringComparison.OrdinalIgnoreCase)).Where(producto => producto.nombre.ToUpper().Contains(buscador.ToUpper()));
                lstProductos.EndRefresh();
            }
            else
            {
                listaProductos = repository.ListProductos();
                lstProductos.ItemsSource = listaProductos;
                lstProductos.EndRefresh();
            }
        }
    }
}