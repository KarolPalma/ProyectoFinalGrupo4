using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace ProyectoFinalGrupo4.Respositories
{
    public class RepositoryCategorias
    {
        private static readonly HttpClient client = new HttpClient();

        public async void InsertCategoria(Categorias categoria, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.insertCategoria;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("categoria", categoria.categoria);
            webClient.QueryString.Add("descripcion", categoria.descripcion);
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Categoría guardada satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha guardado", "OK");
            }

        }

        public async void UpdateCategoria(Categorias categoria, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.updateCategoria;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idCategoria", categoria.idCategoria + "");
            webClient.QueryString.Add("categoria", categoria.categoria);
            webClient.QueryString.Add("descripcion", categoria.descripcion);
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Categoría actualizada satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
            }
        }

        public async void DeactivateCategoria(Categorias categoria, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.deactivateCategoria;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idCategoria", categoria.idCategoria + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Categoría desactivada satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha desactivado", "OK");
            }

        }

        public List<Categorias> ListCategorias()
        {
            string URL = EndPointsAPI.listarCategorias;
            WebClient webClient = new WebClient();

            List<Categorias> listarCategorias = new List<Categorias>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarCategorias = JsonConvert.DeserializeObject<List<Categorias>>(responseString);
            }

            return listarCategorias;
        }

        public Categorias BuscarCategoria(int idCategoria)
        {
            string URL = EndPointsAPI.buscarCategoria;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idCategoria", idCategoria + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Categorias categoria = new Categorias();
            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                categoria = JsonConvert.DeserializeObject<Categorias>(responseString);
            }

            return categoria;
        }


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