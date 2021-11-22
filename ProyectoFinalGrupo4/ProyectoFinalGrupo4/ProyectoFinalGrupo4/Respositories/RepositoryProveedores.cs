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
    public class RepositoryProveedores
    {
        private static readonly HttpClient client = new HttpClient();
        public List<Proveedores> LlenarProveedores()
        {
            string URL = EndPointsAPI.llenarProveedores;
            WebClient webClient = new WebClient();

            List<Proveedores> proveedores = new List<Proveedores>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                proveedores = JsonConvert.DeserializeObject<List<Proveedores>>(responseString);
            }

            return proveedores;
        }
    }
}