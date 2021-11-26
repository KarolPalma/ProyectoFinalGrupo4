using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Respositories
{
    class RepositoryFacturas
    {
        public List<Facturas> ListFacturasCompras()
        {
            string URL = EndPointsAPI.listarFacturasCompras;
            WebClient webClient = new WebClient();

            List<Facturas> listarFacturasCompras = new List<Facturas>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarFacturasCompras = JsonConvert.DeserializeObject<List<Facturas>>(responseString);
            }

            return listarFacturasCompras;
        }

        public List<Facturas> ListFacturasComprasUsuario()
        {
            string URL = EndPointsAPI.listarFacturasComprasUsuario;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", Preferences.Get("idUsuario", "0") + "");

            List<Facturas> listarFacturasCompras = new List<Facturas>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarFacturasCompras = JsonConvert.DeserializeObject<List<Facturas>>(responseString);
            }

            return listarFacturasCompras;
        }

        public List<Facturas> ListFacturasVentas()
        {
            string URL = EndPointsAPI.listarFacturasVentas;
            WebClient webClient = new WebClient();

            List<Facturas> listarFacturasVentas = new List<Facturas>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarFacturasVentas = JsonConvert.DeserializeObject<List<Facturas>>(responseString);
            }

            return listarFacturasVentas;
        }

        public List<Facturas> ListFacturasVentasUsuario()
        {
            string URL = EndPointsAPI.listarFacturasVentasUsuario;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", Preferences.Get("idUsuario", "0") + "");

            List<Facturas> listarFacturasVentas = new List<Facturas>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarFacturasVentas = JsonConvert.DeserializeObject<List<Facturas>>(responseString);
            }

            return listarFacturasVentas;
        }

    }
}
