using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Screens;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaPage : ContentPage
    {
        private double latitud1;
        private double longitud1;

        int idCliente = 1;
        string identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton;
        double latitud, longitud;

        public MapaPage()
        {
            InitializeComponent();
            /*Position p = new Position(lati, longi);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);*/
            Position p = new Position(14.0650, -87.1715);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
        }

        public MapaPage(int id, string identidad, string nombre, string apellido, string tel, string dire, string usu, string email, string button)
        {
            InitializeComponent();

            idCliente = id;
            identificacion = identidad;
            nombres = nombre;
            apellidos = apellido;
            telefono = tel;
            direccion = dire;
            usuario = usu;
            correo = email;
            boton = button;

            Position p = new Position(14.0650, -87.1715);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
        }

        public MapaPage(int id, string identidad, string nombre, string apellido, string tel, string dire, string usu, string email, string button,
            double lati1, double longi1)
        {
            InitializeComponent();

            idCliente = id;
            identificacion = identidad;
            nombres = nombre;
            apellidos = apellido;
            telefono = tel;
            direccion = dire;
            usuario = usu;
            correo = email;
            boton = button;

            latitud1 = lati1;
            longitud1 = longi1;
            Position p = new Position(latitud1, longitud1);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = p,
                Label = nombre + " " + apellido,
                Address = direccion,
            };
            map.Pins.Add(pin);
        }

        public MapaPage(String nombre, String direccion, double latitud1, double longitud1)
        {
            InitializeComponent();
            this.latitud1 = latitud1;
            this.longitud1 = longitud1;
            Position p = new Position(latitud1, longitud1);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = p,
                Label = nombre,
                Address = direccion,
            };
            map.Pins.Add(pin);
            //codigo = 0;
        }

        async private void map_MapClicked(object sender, MapClickedEventArgs e)
        {

            _ = DisplayAlert("Coordenadas", $"Latitud: {e.Position.Latitude}, Longitud: {e.Position.Longitude}", "Aceptar");
            latitud = double.Parse(($"{e.Position.Latitude}"));
            longitud = double.Parse(($"{e.Position.Longitude}"));
            //await Navigation.PushAsync(new ClientePage(idCliente, identificacion, nombres, apellidos, telefono, direccion, latitud, longitud, usuario, correo, boton));
            Navigation.InsertPageBefore(new ClientePage(idCliente, identificacion, nombres, apellidos, telefono, direccion, latitud, longitud, usuario, correo, boton), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync();
           

        }
    }
}