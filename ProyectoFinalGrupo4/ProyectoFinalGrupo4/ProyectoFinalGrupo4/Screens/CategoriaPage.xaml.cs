using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Models;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriaPage : ContentPage
    {
        RepositoryCategorias repository = new RepositoryCategorias();
        List<Categorias> categorias = new List<Categorias>();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        string usuario, rol, idSesionUsuario;
        int idSessionUsuario;
        bool estado;

        public CategoriaPage()
        {
            InitializeComponent();
            //ESTO ES PARA LLAMAR A TODA LA LISTA
            categorias = repository.ListCategorias();
            lstCategorias.ItemsSource = categorias;

            repositorySesiones.RevisarToken();
            usuario = Preferences.Get("usuario", "");
            rol = Preferences.Get("idRol", "");
            idSesionUsuario = Preferences.Get("idSesionUsuario", "");
            idSessionUsuario = int.Parse(Preferences.Get("idSesionUsuario", ""));
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar Categoría" && Validaciones() == true)
            {
                //Crear Nuevo
                Categorias categoria = new Categorias(txtNombre.Text, txtDescripcion.Text);
                categoria.categoria = txtNombre.Text;
                categoria.descripcion = txtDescripcion.Text;
                repository.InsertCategoria(categoria, idSessionUsuario);
                limpiar();
            }
            else if (btnGuardar.Text == "Modificar Categoría" && Validaciones() == true)
            {
                //Modifica Categoría
                Categorias categoria = new Categorias(int.Parse(txtIdCategoria.Text), txtNombre.Text, txtDescripcion.Text, true);

                categoria.idCategoria = int.Parse(txtIdCategoria.Text);
                categoria.categoria = txtNombre.Text;
                categoria.descripcion = txtDescripcion.Text;

                repository.UpdateCategoria(categoria, idSessionUsuario);
                limpiar();
            }
            List<Categorias> listaCategorias;
            listaCategorias = repository.ListCategorias();
            lstCategorias.ItemsSource = listaCategorias;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var categoria = (sender as MenuItem).CommandParameter as Categorias;
            Categorias categorias = repository.BuscarCategoria(categoria.idCategoria);
            txtIdCategoria.Text = categorias.idCategoria.ToString();
            txtNombre.Text = categorias.categoria;
            txtDescripcion.Text = categorias.descripcion;
            estado = categorias.activo;
            btnGuardar.Text = "Modificar Categoría";
            if (estado == false) //Si el estado de la Categoria es INACTIVO
            {
                btnDesactivar.Text = "Activar Categoría";
                btnDesactivar.BackgroundColor = Color.Green;
            }
            else
            {
                btnDesactivar.Text = "Desactivar Categoría";
                btnDesactivar.BackgroundColor = Color.Red;
            }
            btnDesactivar.IsVisible = true;
        }

        private void limpiar()
        {
            txtIdCategoria.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            btnGuardar.Text = "Guardar Categoría";
            btnDesactivar.BackgroundColor = Color.Red;
            btnDesactivar.IsVisible = false;
        }

        async private void btnDesactivar_Clicked(object sender, EventArgs e) //Desactiva o Activa la Categoria
        {
            bool respuesta;
            if (btnDesactivar.Text == "Desactivar Categoría" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea desactivar a la categoría? " + txtNombre.Text, "Si", "No");
                if (respuesta == true)
                {
                    Categorias categoria = new Categorias(int.Parse(txtIdCategoria.Text), txtNombre.Text, txtDescripcion.Text, false);
                    repository.DeactivateCategoria(categoria, idSessionUsuario);
                    btnDesactivar.Text = "Activar Categoría";
                    btnDesactivar.BackgroundColor = Color.Green;
                    limpiar();
                }
            }
            else if (btnDesactivar.Text == "Activar Categoría" && Validaciones() == true)
            {
                respuesta = await DisplayAlert("¡Atención!", "¿Desea activar a la categoría? " + txtNombre.Text, "Si", "No");
                if (respuesta == true)
                {
                    Categorias categoria = new Categorias(int.Parse(txtIdCategoria.Text), txtNombre.Text, txtDescripcion.Text, true);
                    repository.UpdateCategoria(categoria, idSessionUsuario);
                    limpiar();
                }
            }
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            List<Categorias> listaCategorias;
            listaCategorias = repository.ListCategorias();
            lstCategorias.ItemsSource = listaCategorias;

            lstCategorias.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                lstCategorias.ItemsSource = listaCategorias.Where(cate => string.Equals(cate.categoria, buscador, StringComparison.OrdinalIgnoreCase)).Where(cate => cate.categoria.ToUpper().Contains(buscador.ToUpper()));
                lstCategorias.EndRefresh();
            }
            else
            {
                listaCategorias = repository.ListCategorias();
                lstCategorias.ItemsSource = listaCategorias;
                lstCategorias.EndRefresh();
            }
        }

        private bool Validaciones()
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescripcion.Text))
            {
                DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
                respuesta = false;
            }

            return respuesta;
        }

    }
}