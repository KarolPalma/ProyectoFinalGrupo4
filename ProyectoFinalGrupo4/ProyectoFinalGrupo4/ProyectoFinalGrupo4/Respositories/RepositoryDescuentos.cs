using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ProyectoFinalGrupo4.Respositories
{
    class RepositoryDescuentos
    {
        private static readonly HttpClient client = new HttpClient();
        public List<Descuentos> LlenarDescuentos()
        {
            string URL = EndPointsAPI.llenarDescuentos;
            WebClient webClient = new WebClient();

            List<Descuentos> descuentos = new List<Descuentos>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                descuentos = JsonConvert.DeserializeObject<List<Descuentos>>(responseString);
            }

            return descuentos;
        }

        public Descuentos TraerDescuento(int idDescuento)
        {
            string URL = EndPointsAPI.traerDescuento;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idDescuento", idDescuento + "");

            Descuentos descuento = new Descuentos();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                descuento = JsonConvert.DeserializeObject<Descuentos>(responseString);
            }

            return descuento;
        }
    }
}
