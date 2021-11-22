using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Respositories
{
    class RepositoryVentas
    {
        private static readonly HttpClient client = new HttpClient();
        public List<Productos> verProductos()
        {
            string URL = EndPointsAPI.verProductos;
            WebClient webClient = new WebClient();

            List<Productos> listarProductos = new List<Productos>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarProductos = JsonConvert.DeserializeObject<List<Productos>>(responseString);
            }

            return listarProductos;
        }

        public async void InsertCarrito(Carrito carrito)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.verificarInCarrito;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", carrito.idUsuario + "");
            webClient.QueryString.Add("idProducto", carrito.idProducto + "");
            webClient.QueryString.Add("unidadesLlevadas", carrito.unidadesLlevadas + "");
            webClient.QueryString.Add("idDescuento", carrito.idDescuento + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("Existe"))
            {
                await App.Current.MainPage.DisplayAlert("Carrito", "Este producto ya está en el carrito, si desea añadir más unidades del mismo puede editar su pedido en el carrito", "OK");
            }
            else if (responseString.Equals("No Existe"))
            {
                //INSERTA LA INFORMACION
                URL = EndPointsAPI.insertCarrito;
                data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                responseString = UnicodeEncoding.UTF8.GetString(data);

                if (responseString.Equals("1"))
                {
                    await App.Current.MainPage.DisplayAlert("Éxito", "El producto ha sido añadido a su carrito", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se ha añadido", "OK");
                }

            }
        }

        public bool carritoLleno()
        {
            bool carritoLleno = false;
            string URL = EndPointsAPI.verificarInCarrito;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", Preferences.Get("idUsuario", "0"));

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("Existe"))
            {
                carritoLleno = true;
            }

            return carritoLleno;
        }

        public List<Carrito> TraerCarrito()
        {
            string URL = EndPointsAPI.traerCarrito;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", Preferences.Get("idUsuario", "0"));

            List<Carrito> carrito = new List<Carrito>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                carrito = JsonConvert.DeserializeObject<List<Carrito>>(responseString);
            }

            return carrito;

        }

        public async void ActualizarCarrito(Carrito carrito)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.actualizarCarrito;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", carrito.idUsuario + "");
            webClient.QueryString.Add("idProducto", carrito.idProducto + "");
            webClient.QueryString.Add("unidadesLlevadas", carrito.unidadesLlevadas + "");
            webClient.QueryString.Add("idDescuento", carrito.idDescuento + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Actualizado correctamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
            }
        }

        public async void EliminarDelCarrito(int idUsuario, int idProducto)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.eliminarDelCarrito;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", idUsuario + "");
            webClient.QueryString.Add("idProducto", idProducto + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "El producto se ha eliminado de su carrito", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha eliminado", "OK");
            }

        }

        public List<MetodosPago> TraerMetodosPago()
        {
            string URL = EndPointsAPI.traerMetodosPago;
            WebClient webClient = new WebClient();

            List<MetodosPago> metodosPago = new List<MetodosPago>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                metodosPago = JsonConvert.DeserializeObject<List<MetodosPago>>(responseString);
            }

            return metodosPago;

        }
    }
}
