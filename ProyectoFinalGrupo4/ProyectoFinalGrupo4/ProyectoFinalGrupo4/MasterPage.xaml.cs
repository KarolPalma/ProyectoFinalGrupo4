using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Screens;
using ProyectoFinalGrupo4.ViewVentas;

namespace ProyectoFinalGrupo4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public List<MenuLateral> MenuLateral { get; set; }

        public MasterPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            MenuLateral = new List<MenuLateral>();
            MenuLateral inicio = new MenuLateral() { Titulo = "Inicio", Pagina = typeof(MainPage) };
            MenuLateral.Add(inicio);
            MenuLateral administrador = new MenuLateral() { Titulo = "Módulo de Administradores", Pagina = typeof(AdministradorPage) };
            MenuLateral.Add(administrador);
            MenuLateral cliente = new MenuLateral() { Titulo = "Módulo de Clientes", Pagina = typeof(ClientePage) };
            MenuLateral.Add(cliente);
            MenuLateral producto = new MenuLateral() { Titulo = "Módulo de Productos", Pagina = typeof(ProductoPage) };
            MenuLateral.Add(producto);
            MenuLateral venta = new MenuLateral() { Titulo = "Módulo de Ventas", Pagina = typeof(VentasPage) };
            MenuLateral.Add(venta);
            MenuLateral inventario = new MenuLateral() { Titulo = "Control de Inventario", Pagina = typeof(InventarioPage) };
            MenuLateral.Add(inventario);
            MenuLateral bitacora = new MenuLateral() { Titulo = "Bitacora", Pagina = typeof(BitacoraPage) };
            MenuLateral.Add(bitacora);

            this.lstMenu.ItemsSource = MenuLateral;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(LoginPage)));

            this.lstMenu.ItemSelected += lstMenu_ItemSelected;
        }

        private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e) {

            MenuLateral pagina = e.SelectedItem as MenuLateral;
            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.Pagina));
            IsPresented = false;

        }

    }

}