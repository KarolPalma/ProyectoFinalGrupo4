using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoPage : ContentPage
    {
        RepositoryProductos repository = new RepositoryProductos();
        List<Productos> productos = new List<Productos>();
        RepositoryCategorias repoCategoria = new RepositoryCategorias();
        List<Categorias> categorias = new List<Categorias>();
        RepositoryProveedores repoProveedor = new RepositoryProveedores();
        List<Proveedores> proveedores = new List<Proveedores>();
        RepositoryImpuestos repoImpuesto = new RepositoryImpuestos();
        List<Impuestos> impuestos = new List<Impuestos>();
        String cmbSelected = "";
        int usuarioActual = 2;
        bool estado;
        string foto;

        public ProductoPage()
        {
            InitializeComponent();
            //ESTO ES PARA LLAMAR A TODA LA LISTA
            productos = repository.ListProductos();
            lstProductos.ItemsSource = productos;
            categorias = repoCategoria.LlenarCategorias();
            cmbCategoria.ItemsSource = categorias;
            proveedores = repoProveedor.LlenarProveedores();
            cmbProveedor.ItemsSource = proveedores;
            impuestos = repoImpuesto.LlenarImpuestos();
            cmbImpuesto.ItemsSource = impuestos;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar Producto" && Validaciones() == true)
            {
                //Crear Nuevo
                var selectedCategoriaItem = cmbCategoria.SelectedItem as Categorias;
                var provinceIdCategoria = selectedCategoriaItem.idCategoria;
                var selectedProveedorItem = cmbProveedor.SelectedItem as Proveedores;
                var provinceIdProveedor = selectedProveedorItem.idProveedor;
                var selectedImpuestoItem = cmbImpuesto.SelectedItem as Impuestos;
                var provinceIdImpuesto = selectedImpuestoItem.idImpuesto;
                Productos producto = new Productos(txtNombre.Text, txtDescripcion.Text, provinceIdCategoria, provinceIdProveedor, txtCantidadUnidad.Text,
                    int.Parse(txtUnidadAlmacen.Text), int.Parse(txtCantidadMin.Text), int.Parse(txtCantidadMax.Text), foto, double.Parse(txtPrecio.Text), provinceIdImpuesto);

                producto.nombre = txtNombre.Text;
                producto.descripcion = txtDescripcion.Text;
                producto.cantidadUnidad = txtCantidadUnidad.Text;
                producto.unidadesAlmacen = int.Parse(txtUnidadAlmacen.Text);
                producto.cantidadMinima = int.Parse(txtCantidadMin.Text);
                producto.cantidadMaxima = int.Parse(txtCantidadMax.Text);
                producto.precio = double.Parse(txtPrecio.Text);
                bool ingresado = await repository.InsertProducto(producto, usuarioActual);
                if (ingresado)
                {
                    await DisplayAlert("Éxito", "Producto ingresado satisfactoriamente", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No se ingreso el producto", "OK");
                }
                limpiar();

            }
            else if (btnGuardar.Text == "Modificar Producto" && Validaciones() == true)
            {
                var selectedCategoriaItem = cmbCategoria.SelectedItem as Categorias;
                var provinceIdCategoria = selectedCategoriaItem.idCategoria;
                var provinceCategoria = selectedCategoriaItem.categoria;
                var selectedProveedorItem = cmbProveedor.SelectedItem as Proveedores;
                var provinceIdProveedor = selectedProveedorItem.idProveedor;
                var provinceProveedor = selectedProveedorItem.nombre;
                var selectedImpuestoItem = cmbImpuesto.SelectedItem as Impuestos;
                var provinceIdImpuesto = selectedImpuestoItem.idImpuesto;
                var provinceImpuesto = selectedImpuestoItem.nombreImpuesto;
                Productos producto = new Productos(int.Parse(txtIdProducto.Text), txtNombre.Text, txtDescripcion.Text, provinceIdCategoria, provinceCategoria,
                    provinceIdProveedor, provinceProveedor, txtCantidadUnidad.Text, int.Parse(txtUnidadAlmacen.Text), int.Parse(txtCantidadMin.Text),
                    int.Parse(txtCantidadMax.Text), foto, true, double.Parse(txtPrecio.Text), provinceIdImpuesto);
                producto.nombre = txtNombre.Text;
                producto.descripcion = txtDescripcion.Text;
                producto.cantidadUnidad = txtCantidadUnidad.Text;
                producto.unidadesAlmacen = int.Parse(txtUnidadAlmacen.Text);
                producto.cantidadMinima = int.Parse(txtCantidadMin.Text);
                producto.cantidadMaxima = int.Parse(txtCantidadMax.Text);
                producto.precio = double.Parse(txtPrecio.Text);
                bool ingresado = await repository.UpdateProducto(producto, usuarioActual);
                if (ingresado)
                {
                    await DisplayAlert("Éxito", "Producto actualizado satisfactoriamente", "OK");
                }
                else
                {
                    await DisplayAlert("Imagen", "La imagen es demasiado grande, no pudo ser guardada", "OK");
                }
                limpiar();
            }
        }

        async private void btnDesactivar_Clicked(object sender, EventArgs e)
        {
            bool respuesta;
            if (btnDesactivar.Text == "Desactivar Producto" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea desactivar al producto? " + txtNombre.Text, "Si", "No");
                if (respuesta == true)
                {
                    var selectedCategoriaItem = cmbCategoria.SelectedItem as Categorias;
                    var provinceIdCategoria = selectedCategoriaItem.idCategoria;
                    var provinceCategoria = selectedCategoriaItem.categoria;
                    var selectedProveedorItem = cmbProveedor.SelectedItem as Proveedores;
                    var provinceIdProveedor = selectedProveedorItem.idProveedor;
                    var provinceProveedor = selectedProveedorItem.nombre;
                    var selectedImpuestoItem = cmbImpuesto.SelectedItem as Impuestos;
                    var provinceIdImpuesto = selectedImpuestoItem.idImpuesto;
                    var provinceImpuesto = selectedImpuestoItem.nombreImpuesto;
                    Productos producto = new Productos(int.Parse(txtIdProducto.Text), txtNombre.Text, txtDescripcion.Text, provinceIdCategoria, provinceCategoria,
                        provinceIdProveedor, provinceProveedor, txtCantidadUnidad.Text, int.Parse(txtUnidadAlmacen.Text), int.Parse(txtCantidadMin.Text),
                        int.Parse(txtCantidadMax.Text), foto, false, double.Parse(txtPrecio.Text), provinceIdImpuesto);
                    repository.DeactivateProducto(producto, usuarioActual);
                    btnDesactivar.Text = "Activar Producto";
                    btnDesactivar.BackgroundColor = Color.Green;
                    limpiar();
                }
            }
            else if (btnDesactivar.Text == "Activar Producto" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea activar al producto? " + txtNombre.Text, "Si", "No");
                if (respuesta == true)
                {
                    var selectedCategoriaItem = cmbCategoria.SelectedItem as Categorias;
                    var provinceIdCategoria = selectedCategoriaItem.idCategoria;
                    var provinceCategoria = selectedCategoriaItem.categoria;
                    var selectedProveedorItem = cmbProveedor.SelectedItem as Proveedores;
                    var provinceIdProveedor = selectedProveedorItem.idProveedor;
                    var provinceProveedor = selectedProveedorItem.nombre;
                    var selectedImpuestoItem = cmbImpuesto.SelectedItem as Impuestos;
                    var provinceIdImpuesto = selectedImpuestoItem.idImpuesto;
                    var provinceImpuesto = selectedImpuestoItem.nombreImpuesto;
                    Productos producto = new Productos(int.Parse(txtIdProducto.Text), txtNombre.Text, txtDescripcion.Text, provinceIdCategoria, provinceCategoria,
                        provinceIdProveedor, provinceProveedor, txtCantidadUnidad.Text, int.Parse(txtUnidadAlmacen.Text), int.Parse(txtCantidadMin.Text),
                        int.Parse(txtCantidadMax.Text), foto, true, double.Parse(txtPrecio.Text), provinceIdImpuesto);
                    repository.UpdateProducto(producto, usuarioActual);
                    limpiar();
                }
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var product = (sender as MenuItem).CommandParameter as Productos;
            txtIdProducto.Text = product.idProducto.ToString();
            Productos producto = repository.BuscarProductos(product.idProducto);
            txtIdProducto.Text = producto.idProducto.ToString();
            txtNombre.Text = producto.nombre;
            txtDescripcion.Text = producto.descripcion;
            cmbCategoria.SelectedIndex = producto.idCategoria - 1;
            cmbProveedor.SelectedIndex = producto.idProveedor - 1;
            txtCantidadUnidad.Text = producto.cantidadUnidad;
            txtUnidadAlmacen.Text = producto.unidadesAlmacen.ToString();
            txtCantidadMin.Text = producto.cantidadMinima.ToString();
            txtCantidadMax.Text = producto.cantidadMaxima.ToString();
            imgProducto.IsVisible = true;
            imgProducto.Source = producto.fotoSource;
            foto = producto.foto;
            txtPrecio.Text = producto.precio.ToString();
            cmbImpuesto.SelectedIndex = producto.idImpuesto - 1;
            estado = producto.estado;
            btnGuardar.Text = "Modificar Producto";
            if (estado == false) //Si el estado del Producto es INACTIVO
            {
                btnDesactivar.Text = "Activar Producto";
                btnDesactivar.BackgroundColor = Color.Green;
            }
            else
            {
                btnDesactivar.Text = "Desactivar Producto";
                btnDesactivar.BackgroundColor = Color.Red;
            }
            btnDesactivar.IsVisible = true;
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

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbCategoria.SelectedIndex != -1)
            {
                cmbSelected = cmbCategoria.Items[cmbCategoria.SelectedIndex];
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex != -1)
            {
                cmbSelected = cmbProveedor.Items[cmbProveedor.SelectedIndex];
            }
        }

        private void cmbImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbImpuesto.SelectedIndex != -1)
            {
                cmbSelected = cmbImpuesto.Items[cmbImpuesto.SelectedIndex];
            }
        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || cmbCategoria.SelectedIndex == -1 || cmbProveedor.SelectedIndex == -1 || string.IsNullOrEmpty(txtCantidadUnidad.Text) ||
                string.IsNullOrEmpty(txtUnidadAlmacen.Text) || string.IsNullOrEmpty(txtCantidadMin.Text) || string.IsNullOrEmpty(txtCantidadMax.Text) || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(foto))
            {
                DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                respuesta = false;
            }

            return respuesta;
        }

        private void limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            txtCantidadUnidad.Text = "";
            txtUnidadAlmacen.Text = "";
            txtCantidadMin.Text = "";
            txtCantidadMax.Text = "";
            txtPrecio.Text = "";
            cmbImpuesto.SelectedIndex = -1;
            btnGuardar.Text = "Guardar Producto";
            btnDesactivar.BackgroundColor = Color.Red;
            btnDesactivar.IsVisible = false;
            imgProducto.IsVisible = false;

            List<Productos> listaProductos;
            listaProductos = repository.ListProductos();
            lstProductos.ItemsSource = listaProductos;
        }

        async private void btnSeleccionarFoto_Clicked(object sender, EventArgs e)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                var resultado = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecciona una Fotografía" });
                var stream = await resultado.OpenReadAsync();
                stream.CopyTo(memory);
                byte[] bytes = memory.ToArray();
                foto = Convert.ToBase64String(bytes);
                Stream stream2 = new MemoryStream(bytes);
                imgProducto.IsVisible = true;
                imgProducto.Source = ImageSource.FromStream(() => stream2);
            }
        }

    }
}