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
    class RepositoryDescuentos
    {
        private static readonly HttpClient client = new HttpClient();

        public async void InsertDescuento(Descuentos descuento, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.insertDescuento;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("nombreDescuento", descuento.nombreDescuento);
            webClient.QueryString.Add("valorDescuento", descuento.valorDescuento + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Descuento guardado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha guardado", "OK");
            }

        }

        public async void UpdateDescuento(Descuentos descuento, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.updateDescuento;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idDescuento", descuento.idDescuento + "");
            webClient.QueryString.Add("nombreDescuento", descuento.nombreDescuento);
            webClient.QueryString.Add("valorDescuento", descuento.valorDescuento + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Descuento actualizado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
            }
        }

        public async void DeactivateDescuento(Descuentos descuento, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.deactivateDescuento;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idDescuento", descuento.idDescuento + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Descuento desactivado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha desactivado", "OK");
            }

        }

        public List<Descuentos> ListDescuentos()
        {
            string URL = EndPointsAPI.listarDescuentos;
            WebClient webClient = new WebClient();

            List<Descuentos> listarDescuentos = new List<Descuentos>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarDescuentos = JsonConvert.DeserializeObject<List<Descuentos>>(responseString);
                listarDescuentos.RemoveAll(descuento => descuento.nombreDescuento == "Ninguno");
            }

            return listarDescuentos;
        }

        public Descuentos BuscarDescuento(int idDescuento)
        {
            string URL = EndPointsAPI.buscarDescuento;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idDescuento", idDescuento + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Descuentos descuento = new Descuentos();
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
