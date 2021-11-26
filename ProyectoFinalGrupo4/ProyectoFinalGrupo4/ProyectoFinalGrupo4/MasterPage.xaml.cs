using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Screens;
using ProyectoFinalGrupo4.ViewCompras;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public List<MenuLateral> MenuLateral { get; set; }

        public MasterPage()
        {

            InitializeComponent();
            MenuLateral = new List<MenuLateral>();
            MenuLateral inicio = new MenuLateral() { Titulo = "Inicio", Pagina = typeof(MainPage) };
            MenuLateral.Add(inicio);

            this.lstMenu.ItemsSource = MenuLateral;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(LoginPage)));

            this.lstMenu.ItemSelected += lstMenu_ItemSelected;
        }

        public MasterPage(int rol)
        {
            InitializeComponent();
            if (rol == 1) //Si es un Administrador
            {
                MenuLateral = new List<MenuLateral>();
                MenuLateral inicio = new MenuLateral() { Titulo = "Inicio", Pagina = typeof(ComprasPage) };
                MenuLateral.Add(inicio);

                MenuLateral administrador = new MenuLateral() { Titulo = "Módulo de Administradores", Pagina = typeof(AdministradorPage) };
                MenuLateral.Add(administrador);

                MenuLateral cliente = new MenuLateral() { Titulo = "Módulo de Clientes", Pagina = typeof(ClientePage) };
                MenuLateral.Add(cliente);

                MenuLateral producto = new MenuLateral() { Titulo = "Módulo de Productos", Pagina = typeof(ProductoPage) };
                MenuLateral.Add(producto);

                MenuLateral proveedor = new MenuLateral() { Titulo = "Módulo de Proveedores", Pagina = typeof(ProveedorPage) };
                MenuLateral.Add(proveedor);

                MenuLateral inventario = new MenuLateral() { Titulo = "Control de Inventario", Pagina = typeof(InventarioPage) };
                MenuLateral.Add(inventario);

                MenuLateral bitacora = new MenuLateral() { Titulo = "Bitacora", Pagina = typeof(BitacoraPage) };
                MenuLateral.Add(bitacora);

                MenuLateral categoria = new MenuLateral() { Titulo = "Módulo de Categorías", Pagina = typeof(CategoriaPage) };
                MenuLateral.Add(categoria);

                MenuLateral impuesto = new MenuLateral() { Titulo = "Módulo de Impuestos", Pagina = typeof(ImpuestoPage) };
                MenuLateral.Add(impuesto);

                MenuLateral descuento = new MenuLateral() { Titulo = "Módulo de Descuentos", Pagina = typeof(DescuentoPage) };
                MenuLateral.Add(descuento);

                MenuLateral facturaCompras = new MenuLateral() { Titulo = "Módulo Facturas Compras", Pagina = typeof(FacturasComprasPage) };
                MenuLateral.Add(facturaCompras);

                MenuLateral faccturaVentas = new MenuLateral() { Titulo = "Módulo Facturas Ventas", Pagina = typeof(FacturasVentasPage) };
                MenuLateral.Add(faccturaVentas);

                MenuLateral facturaUsuario = new MenuLateral() { Titulo = "Mis Facturas", Pagina = typeof(FacturasUsuario) };
                MenuLateral.Add(facturaUsuario);

                MenuLateral perfil = new MenuLateral() { Titulo = "Mi cuenta", Pagina = typeof(PerfilPage) };
                MenuLateral.Add(perfil);

                MenuLateral cerrarSesion = new MenuLateral() { Titulo = "Cerrar Sesión", Pagina = typeof(LoginPage) };
                MenuLateral.Add(cerrarSesion);

                this.lstMenu.ItemsSource = MenuLateral;
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ComprasPage)));
                this.lstMenu.ItemSelected += lstMenu_ItemSelected;
            }
            else if (rol == 2) //Si es un Cliente
            {
                MenuLateral = new List<MenuLateral>();
                MenuLateral inicio = new MenuLateral() { Titulo = "Inicio", Pagina = typeof(VentasPage) };
                MenuLateral.Add(inicio);
                MenuLateral facturaUsuario = new MenuLateral() { Titulo = "Mis Facturas", Pagina = typeof(FacturasUsuario) };
                MenuLateral.Add(facturaUsuario);
                MenuLateral perfil = new MenuLateral() { Titulo = "Mi Cuenta", Pagina = typeof(PerfilPage) };
                MenuLateral.Add(perfil);
                MenuLateral cerrarSesion = new MenuLateral() { Titulo = "Cerrar Sesión", Pagina = typeof(LoginPage) };
                MenuLateral.Add(cerrarSesion);

                this.lstMenu.ItemsSource = MenuLateral;
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(VentasPage)));
                this.lstMenu.ItemSelected += lstMenu_ItemSelected;
            }
        }

        private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            MenuLateral pagina = e.SelectedItem as MenuLateral;
            if (pagina.Titulo == "Cerrar Sesión")
            {
                Preferences.Set("idUsuario", "");
                Preferences.Set("usuario", "");
                Preferences.Set("idRol", "");
                Preferences.Set("idSesionUsuario", "");
                Preferences.Set("token", "");
            }

            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.Pagina));
            IsPresented = false;
        }

    }

}