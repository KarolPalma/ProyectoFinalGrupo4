using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.ViewVentas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesProductoCarrito : ContentPage
    {

        Productos productoGlobal = new Productos();
        RepositoryDescuentos repositoryDescuentos = new RepositoryDescuentos();
        List<Descuentos> descuentos = new List<Descuentos>();

        public DetallesProductoCarrito(Productos producto)
        {
            InitializeComponent();
            productoGlobal = producto;
            imgProducto.Source = producto.fotoSource;
            txtNombreProducto.Text = producto.nombre;
            txtCategoriaProveedor.Text = producto.proveedor + " | " + producto.categoria;
            txtDescripcion.Text = producto.descripcion;
            txtCantidad.Text = "Cantidad Unidad: " + producto.cantidadUnidad;
            txtUnidades.Text = "Unidades Disponibles: " + producto.unidadesAlmacen;
            txtPrecio.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(producto.precioConImpuesto));

            descuentos = repositoryDescuentos.LlenarDescuentos();
            if (descuentos.Count == 1)
            {
                cmbDescuentos.Title = "No hay descuentos aplicables actualmente";
                cmbDescuentos.SelectedItem = descuentos[0];
                cmbDescuentos.IsEnabled = false;
            }
            else
            {
                cmbDescuentos.ItemsSource = descuentos;
            }

            txtCantidadesCarrito.Text = producto.unidadesCarrito + "";
            cmbDescuentos.SelectedIndex = (producto.idDescuento - 1);
        }

        private void Button_Back(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void SumarCantidad(object sender, EventArgs e)
        {
            if (int.Parse(txtCantidadesCarrito.Text) < productoGlobal.unidadesAlmacen)
            {
                int cantidad = int.Parse(txtCantidadesCarrito.Text) + 1;
                txtCantidadesCarrito.Text = cantidad + "";
            }
            if (int.Parse(txtCantidadesCarrito.Text) >= 10)
            {
                txtCantidadesCarrito.WidthRequest = 60;
            }
        }

        private void RestarCantidad(object sender, EventArgs e)
        {
            if (int.Parse(txtCantidadesCarrito.Text) > 1)
            {
                int cantidad = int.Parse(txtCantidadesCarrito.Text) - 1;
                txtCantidadesCarrito.Text = cantidad + "";
            }
            if (int.Parse(txtCantidadesCarrito.Text) < 10)
            {
                txtCantidadesCarrito.WidthRequest = 10;
            }
        }

        private void verImpuesto(object sender, EventArgs e)
        {
            DisplayAlert("Información de Impuesto", productoGlobal.nombreImpuesto + " (" + productoGlobal.porcentajeImpuesto + "%)", "Ok");
        }

        private void ActualizarCarrito(object sender, EventArgs e)
        {
            if (cmbDescuentos.SelectedItem is null)
            {
                cmbDescuentos.SelectedItem = descuentos[0];
            }

            if (productoGlobal.unidadesAlmacen < productoGlobal.unidadesCarrito)
            {
                DisplayAlert("Unidades excedentes", "No hay suficientes unidades en almacén, por favor indique una cantidad menor o espere al reabastecimiento del producto", "Ok");
            }
            else
            {
                int idUsuario = int.Parse(Preferences.Get("idUsuario", "0"));
                int idProducto = productoGlobal.idProducto;
                int unidadesLlevadas = int.Parse(txtCantidadesCarrito.Text);
                Descuentos descuento = (Descuentos)cmbDescuentos.SelectedItem;

                Carrito carrito = new Carrito(idUsuario, idProducto, unidadesLlevadas, descuento.idDescuento);

                RepositoryVentas repositoryVentas = new RepositoryVentas();
                repositoryVentas.ActualizarCarrito(carrito);
            }
        }

        private void EliminarDelCarrito(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(Preferences.Get("idUsuario", "0"));
            int idProducto = productoGlobal.idProducto;

            RepositoryVentas repositoryVentas = new RepositoryVentas();
            repositoryVentas.EliminarDelCarrito(idUsuario, idProducto);

            this.Navigation.PopAsync();
        }

    }
}