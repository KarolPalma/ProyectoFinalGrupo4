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
    public class RepositoryProveedores
    {
        private static readonly HttpClient client = new HttpClient();

        public async void InsertProveedor(Proveedores proveedor, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.insertProveedor;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("nombre", proveedor.nombre);
            webClient.QueryString.Add("contacto", proveedor.contacto);
            webClient.QueryString.Add("telefono", proveedor.telefono);
            webClient.QueryString.Add("correo", proveedor.correo);
            webClient.QueryString.Add("direccion", proveedor.direccion);
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Proveedor guardado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha guardado", "OK");
            }

        }

        public async void UpdateProveedor(Proveedores proveedor, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.updateProveedor;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idProveedor", proveedor.idProveedor + "");
            webClient.QueryString.Add("nombre", proveedor.nombre);
            webClient.QueryString.Add("contacto", proveedor.contacto);
            webClient.QueryString.Add("telefono", proveedor.telefono);
            webClient.QueryString.Add("correo", proveedor.correo);
            webClient.QueryString.Add("direccion", proveedor.direccion);
            webClient.QueryString.Add("estado", proveedor.estado + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Proveedor actualizado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
            }
        }

        public async void DeactivateProveedor(Proveedores proveedor, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.deactivateProveedor;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idProveedor", proveedor.idProveedor + "");
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

        public List<Proveedores> ListProveedor()
        {
            string URL = EndPointsAPI.listarProveedores;
            WebClient webClient = new WebClient();

            List<Proveedores> listarProveedores = new List<Proveedores>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarProveedores = JsonConvert.DeserializeObject<List<Proveedores>>(responseString);
            }

            return listarProveedores;
        }

        public Proveedores BuscarProveedor(int idProveedor)
        {
            string URL = EndPointsAPI.buscarProveedor;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idProveedor", idProveedor + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Proveedores proveedor = new Proveedores();
            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                proveedor = JsonConvert.DeserializeObject<Proveedores>(responseString);
            }

            return proveedor;
        }


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