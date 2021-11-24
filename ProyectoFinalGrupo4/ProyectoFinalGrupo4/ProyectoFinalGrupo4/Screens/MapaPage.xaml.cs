using ProyectoFinalGrupo4.ViewVentas;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaPage : ContentPage
    {
        private double latitud1;
        private double longitud1;

        int idCliente = 1, registro;
        string identificacion, nombres, apellidos, telefono, direccion, usuario, correo, boton;
        double latitud, longitud, total;

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

        public MapaPage(double latitud, double longitud, double total)
        {
            InitializeComponent();
            this.latitud = latitud;
            this.longitud = longitud;
            this.total = total;
            Position p = new Position(latitud, longitud);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = p,
                Label = "Lugar de entrega actual",
            };
            map.Pins.Add(pin);
            idCliente = -1;
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

        //REGISTRO
        public MapaPage(int id, string identidad, string nombre, string apellido, string tel, string dire, string usu, string email, int regist)
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
            registro = regist;

            Position p = new Position(14.0650, -87.1715);
            MapSpan mapSpan = new MapSpan(p, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
        }

        //PERFIL
        public MapaPage(string nombre, string apellido, int regist, double lati1, double longi1)
        {
            InitializeComponent();

            nombres = nombre;
            apellidos = apellido;
            registro = regist;

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

        //REGISTRO
        public MapaPage(int id, string identidad, string nombre, string apellido, string tel, string dire, string usu, string email, int regist,
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
            registro = regist;

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

            if (idCliente != -1)
            {
                if (registro == 1)
                {
                    Navigation.InsertPageBefore(new RegistroPage(idCliente, identificacion, nombres, apellidos, telefono, direccion, latitud, longitud, usuario, correo), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else if (registro == 2)
                {
                    Navigation.InsertPageBefore(new PerfilPage(latitud, longitud), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new ClientePage(idCliente, identificacion, nombres, apellidos, telefono, direccion, latitud, longitud, usuario, correo, boton), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                Navigation.InsertPageBefore(new FacturacionPage(latitud, longitud, total), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
            }


        }
    }
}