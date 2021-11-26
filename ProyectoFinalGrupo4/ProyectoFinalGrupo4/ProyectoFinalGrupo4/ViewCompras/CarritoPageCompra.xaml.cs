using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.ViewCompras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarritoPageCompra : ContentPage
    {
        RepositoryVentas repositoryVentas = new RepositoryVentas();
        RepositoryProductos repositoryProductos = new RepositoryProductos();
        RepositoryDescuentos repositoryDescuentos = new RepositoryDescuentos();
        RepositoryImpuestos repositoryImpuestos = new RepositoryImpuestos();
        double total = 0;
        List<Productos> productosGlobal = new List<Productos>();

        public CarritoPageCompra()
        {
            InitializeComponent();
            List<Carrito> carritoLista = repositoryVentas.TraerCarrito();
            List<Productos> productos = new List<Productos>();

            double subtotal = 0;
            double impuestos = 0;
            double descuentos = 0;
            total = 0;

            foreach (Carrito carrito in carritoLista)
            {
                Productos producto = repositoryProductos.BuscarProductos(carrito.idProducto);
                Descuentos descuento = repositoryDescuentos.TraerDescuento(carrito.idDescuento);
                Impuestos impuesto = repositoryImpuestos.TraerImpuesto(producto.idImpuesto);

                producto.nombreImpuesto = impuesto.nombreImpuesto;
                producto.porcentajeImpuesto = impuesto.porcentajeImpuesto;
                producto.unidadesCarrito = carrito.unidadesLlevadas;
                producto.idDescuento = carrito.idDescuento;

                if (!(descuento is null))
                {
                    producto.SetPrecioConDescuento(descuento.porcentajeDescuento);
                }
                else
                {
                    producto.SetPrecioConDescuento(0);
                }

                if (producto.precioConImpuesto == producto.precioConDescuento)
                {
                    producto.precioImpuestoVisualizacion = "";
                }
                else
                {
                    producto.precioImpuestoVisualizacion = "L." + string.Format("{0:#.00}", Convert.ToDecimal(producto.precioConImpuesto));
                }

                subtotal += (producto.precio * producto.unidadesCarrito);
                impuestos += ((producto.precioConImpuesto - producto.precio) * producto.unidadesCarrito);
                descuentos += ((producto.precioConDescuento - producto.precioConImpuesto) * producto.unidadesCarrito) * (-1);
                total += producto.precioConDescuento * producto.unidadesCarrito;

                productos.Add(producto);
                productosGlobal.Add(producto);
            }

            if (carritoLista.Count == 0)
            {
                btnRealizarPedido.IsEnabled = false;
            }

            listViewProductos.ItemsSource = productos;
            txtSubtotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(subtotal));
            txtImpuestos.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(impuestos));
            txtDescuentos.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(descuentos));
            txtTotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(total));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Carrito> carritoLista = repositoryVentas.TraerCarrito();
            List<Productos> productos = new List<Productos>();

            double subtotal = 0;
            double impuestos = 0;
            double descuentos = 0;
            total = 0;

            foreach (Carrito carrito in carritoLista)
            {
                Productos producto = repositoryProductos.BuscarProductos(carrito.idProducto);
                Descuentos descuento = repositoryDescuentos.TraerDescuento(carrito.idDescuento);
                Impuestos impuesto = repositoryImpuestos.TraerImpuesto(producto.idImpuesto);

                producto.nombreImpuesto = impuesto.nombreImpuesto;
                producto.porcentajeImpuesto = impuesto.porcentajeImpuesto;
                producto.unidadesCarrito = carrito.unidadesLlevadas;
                producto.idDescuento = carrito.idDescuento;

                if (!(descuento is null))
                {
                    producto.SetPrecioConDescuento(descuento.porcentajeDescuento);
                }
                else
                {
                    producto.SetPrecioConDescuento(0);
                }

                if (producto.precioConImpuesto == producto.precioConDescuento)
                {
                    producto.precioImpuestoVisualizacion = "";
                }
                else
                {
                    producto.precioImpuestoVisualizacion = "L." + string.Format("{0:#.00}", Convert.ToDecimal(producto.precioConImpuesto));
                }

                subtotal += (producto.precio * producto.unidadesCarrito);
                impuestos += ((producto.precioConImpuesto - producto.precio) * producto.unidadesCarrito);
                descuentos += ((producto.precioConDescuento - producto.precioConImpuesto) * producto.unidadesCarrito) * (-1);
                total += producto.precioConDescuento * producto.unidadesCarrito;

                productos.Add(producto);
                productosGlobal.Add(producto);
            }


            listViewProductos.ItemsSource = productos;
            txtSubtotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(subtotal));
            txtImpuestos.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(impuestos));
            txtDescuentos.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(descuentos));
            txtTotal.Text = "L." + string.Format("{0:#.00}", Convert.ToDecimal(total));

            if (carritoLista.Count == 0)
            {
                btnRealizarPedido.IsEnabled = false;
            }

        }

        private async void listViewProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos producto = (Productos)listViewProductos.SelectedItem;
            await Navigation.PushAsync(new DetallesProductoCarritoCompra(producto));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private async void Realizar_Pedido(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacturacionPageCompra(total));
        }
    }
}