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
    class RepositoryImpuestos
    {
        private static readonly HttpClient client = new HttpClient();

        public async void InsertImpuesto(Impuestos impuesto, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.insertImpuesto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("nombreImpuesto", impuesto.nombreImpuesto);
            webClient.QueryString.Add("valorImpuesto", impuesto.valorImpuesto + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Impuesto guardado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha guardado", "OK");
            }

        }

        public async void UpdateImpuesto(Impuestos impuesto, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.updateImpuesto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idImpuesto", impuesto.idImpuesto + "");
            webClient.QueryString.Add("nombreImpuesto", impuesto.nombreImpuesto);
            webClient.QueryString.Add("valorImpuesto", impuesto.valorImpuesto + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Impuesto actualizado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
            }
        }

        public async void DeactivateImpuesto(Impuestos impuesto, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.deactivateImpuesto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idImpuesto", impuesto.idImpuesto + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Impuesto desactivado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha desactivado", "OK");
            }

        }

        public List<Impuestos> ListImpuestos()
        {
            string URL = EndPointsAPI.listarImpuestos;
            WebClient webClient = new WebClient();

            List<Impuestos> listarImpuestos = new List<Impuestos>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarImpuestos = JsonConvert.DeserializeObject<List<Impuestos>>(responseString);
                listarImpuestos.RemoveAll(impuesto => impuesto.nombreImpuesto == "Ninguno");
            }

            return listarImpuestos;
        }

        public Impuestos BuscarImpuesto(int idImpuesto)
        {
            string URL = EndPointsAPI.buscarImpuesto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idImpuesto", idImpuesto + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Impuestos impuesto = new Impuestos();
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
