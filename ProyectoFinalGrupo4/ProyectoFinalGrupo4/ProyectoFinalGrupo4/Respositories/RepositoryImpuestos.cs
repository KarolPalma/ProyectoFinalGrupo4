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
    class RepositoryImpuestos
    {
        private static readonly HttpClient client = new HttpClient();
        public List<Impuestos> LlenarImpuestos()
        {
            string URL = EndPointsAPI.llenarImpuestos;
            WebClient webClient = new WebClient();

            List<Impuestos> impuestos = new List<Impuestos>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                impuestos = JsonConvert.DeserializeObject<List<Impuestos>>(responseString);
            }

            return impuestos;
        }

        public Impuestos TraerImpuesto(int idImpuesto)
        {
            string URL = EndPointsAPI.traerImpuesto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idImpuesto", idImpuesto + "");

            Impuestos impuesto = new Impuestos();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                impuesto = JsonConvert.DeserializeObject<Impuestos>(responseString);
            }

            return impuesto;
        }
    }
}
