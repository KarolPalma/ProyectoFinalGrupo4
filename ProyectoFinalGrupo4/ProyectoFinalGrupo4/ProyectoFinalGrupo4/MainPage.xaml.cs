using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Respositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using ProyectoFinalGrupo4.Screens;

namespace ProyectoFinalGrupo4
{
    public partial class MainPage : ContentPage
    {
        Administradores administrador = new Administradores();
        RepositoryAdministradores repository = new RepositoryAdministradores();
        RepositoryClientes repositoryClientes = new RepositoryClientes();
        Clientes cliente = new Clientes();
        RepositoryProductos repositoryProductos = new RepositoryProductos();
        Productos producto = new Productos();
        RepositorySesiones repositorySesiones = new RepositorySesiones();
        RepositoryCategorias repositoryCategorias = new RepositoryCategorias();
        RepositoryProveedores repositoryProveedores = new RepositoryProveedores();
        RepositoryBitacora repositoryBitacora = new RepositoryBitacora();


        public MainPage(string nombre, string rol)
        {
            InitializeComponent();
            prueba1.Text = nombre;
            prueba2.Text = rol;
        }
            public MainPage()
        {
            InitializeComponent();
            
            //List<Productos> productos = new List<Productos>();
            /* ESTO ES PARA LLAMAR A TODA LA LISTA
            administradores = repository.ListAdministradores();
            lstAdministradores.ItemsSource = administradores;

            ESTO PARA UNO SOLO
            administradores.Add(repository.BuscarAdministrador(1));
            lstAdministradores.ItemsSource = administradores;


            clientes = repositoryClientes.ListClientes();
            lstAdministradores.ItemsSource = clientes;

            clientes.Add(repositoryClientes.BuscarCliente(2));
            lstAdministradores.ItemsSource = clientes;
            
            productos = repositoryProductos.ListProductos();
            lstAdministradores.ItemsSource = productos;
            
            productos.Add(repositoryProductos.BuscarProductos(1));
            lstAdministradores.ItemsSource = productos;
            

            Preferences.Set("token", "1");
            if (repositorySesiones.RevisarToken())
            {
                // Preferences.get("idUsuario", "");
                usuario.Text = Preferences.Get("usuario", "");
                rol.Text = Preferences.Get("idRol", "");
                idSesionUsuario.Text = Preferences.Get("idSesionUsuario", "");
                int unu = int.Parse(Preferences.Get("idSesionUsuario", ""));
            }
            else
            {
                Navigation.PopToRootAsync();
            }

            lstAdministradores.ItemsSource = repositoryCategorias.LlenarCategorias(); ;

            lstAdministradores.ItemsSource = repositoryProveedores.LlenarProveedores(); 
            
            lstAdministradores.ItemsSource = repositoryBitacora.ListarBitacora();

            lstAdministradores.ItemsSource = repositoryBitacora.BuscarRegistros(DateTime.Now);
            */

            //repositorySesiones.UsuarioLogin("mguarda", "12345");

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //repositoryProductos.InsertProducto(producto, 1);
            await Navigation.PushAsync(new VentasPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //repositoryProductos.UpdateProducto(producto, 1);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            //repositoryProductos.DeactivateProducto(producto, 1);
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync();
        }
    }
}
