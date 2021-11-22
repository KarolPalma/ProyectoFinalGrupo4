using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Respositories
{
    public class RepositoryCategorias
    {
        private static readonly HttpClient client = new HttpClient();
        public List<Categorias> LlenarCategorias()
        {
            string URL = EndPointsAPI.llenarCategorias;
            WebClient webClient = new WebClient();

            List<Categorias> categorias = new List<Categorias>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                categorias = JsonConvert.DeserializeObject<List<Categorias>>(responseString);
            }

            return categorias;
        }
    }
}