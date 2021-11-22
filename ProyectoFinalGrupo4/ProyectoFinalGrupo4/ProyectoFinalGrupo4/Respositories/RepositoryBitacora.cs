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
    class RepositoryBitacora
    {
        private static readonly HttpClient client = new HttpClient();
        public List<Bitacora> ListarBitacora()
        {
            string URL = EndPointsAPI.listarBitacora;
            WebClient webClient = new WebClient();

            List<Bitacora> bitacora = new List<Bitacora>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                bitacora = JsonConvert.DeserializeObject<List<Bitacora>>(responseString);
            }

            return bitacora;
        }

        public List<Bitacora> BuscarRegistros(DateTime fecha)
        {   
            string URL = EndPointsAPI.buscarBitacora;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("fecha", fecha.ToString("yyyy-MM-dd"));

            List<Bitacora> bitacora = new List<Bitacora>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                bitacora = JsonConvert.DeserializeObject<List<Bitacora>>(responseString);
            }

            return bitacora;
        }
    }
}
