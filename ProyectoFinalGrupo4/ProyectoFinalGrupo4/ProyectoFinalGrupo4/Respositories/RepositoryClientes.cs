using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

using Xamarin.Forms;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Dependencies;
using System.Net;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace ProyectoFinalGrupo4.Respositories
{
    public class RepositoryClientes : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        //METODOS
        public async void InsertCliente(Clientes cliente, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.verificarIdeCliente;
            WebClient webClient = new WebClient(); 

            webClient.QueryString.Add("identificacion", cliente.identificacion);
            webClient.QueryString.Add("nombres", cliente.nombres);
            webClient.QueryString.Add("apellidos", cliente.apellidos);
            webClient.QueryString.Add("telefono", cliente.telefono);
            webClient.QueryString.Add("nacimiento", cliente.fechaNacimiento.Date.Year + "-" + cliente.fechaNacimiento.Date.Month + "-" + cliente.fechaNacimiento.Date.Day);
            webClient.QueryString.Add("direccion", cliente.direccion);
            webClient.QueryString.Add("latitud", cliente.latitud + "");
            webClient.QueryString.Add("longitud", cliente.longitud + "");
            webClient.QueryString.Add("usuario", cliente.usuario);
            webClient.QueryString.Add("correo", cliente.correo);
            webClient.QueryString.Add("clave", cliente.clave);
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("Existe"))
            {
                await App.Current.MainPage.DisplayAlert("Error", "La identificación especificada ya pertenece a un cliente registrado", "OK");
            }
            else if (responseString.Equals("No Existe"))
            {
                //SOLO PREGUNTA SI EXISTE EL USUARIO EN LA BD
                URL = EndPointsAPI.verificarUsuarioCliente;
                data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                responseString = UnicodeEncoding.UTF8.GetString(data);

                if (responseString.Equals("Existe"))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Este nombre de usuario ya está en uso", "OK");
                }
                else if (responseString.Equals("No Existe"))
                {
                    //INSERTA LA INFORMACION
                    URL = EndPointsAPI.insertCliente;
                    data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                    responseString = UnicodeEncoding.UTF8.GetString(data);

                    if (responseString.Equals("1"))
                    {
                        await App.Current.MainPage.DisplayAlert("Éxito", "Registro completado satisfactoriamente", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se ha guardado", "OK");
                    }
                }
            }
        }
        
        public async void UpdateCliente(Clientes cliente, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.verificarIdeCliente;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idCliente", cliente.idCliente + "");
            webClient.QueryString.Add("identificacion", cliente.identificacion);
            webClient.QueryString.Add("nombres", cliente.nombres);
            webClient.QueryString.Add("apellidos", cliente.apellidos);
            webClient.QueryString.Add("telefono", cliente.telefono);
            webClient.QueryString.Add("nacimiento", cliente.fechaNacimiento.Date.Year + "-" + cliente.fechaNacimiento.Date.Month + "-" + cliente.fechaNacimiento.Date.Day);
            webClient.QueryString.Add("direccion", cliente.direccion);
            webClient.QueryString.Add("latitud", cliente.latitud + "");
            webClient.QueryString.Add("longitud", cliente.longitud + "");
            webClient.QueryString.Add("usuario", cliente.usuario);
            webClient.QueryString.Add("correo", cliente.correo);
            webClient.QueryString.Add("clave", cliente.clave);
            webClient.QueryString.Add("estado",cliente.estado + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("Existe"))
            {
                await App.Current.MainPage.DisplayAlert("Error", "La identificación especificada ya pertenece a un cliente registrado", "OK");
            }
            else if (responseString.Equals("No Existe"))
            {
                //SOLO PREGUNTA SI EXISTE EL USUARIO EN LA BD
                URL = EndPointsAPI.verificarUsuarioCliente;
                data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                responseString = UnicodeEncoding.UTF8.GetString(data);

                if (responseString.Equals("Existe"))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Este nombre de usuario ya está en uso", "OK");
                }
                else if (responseString.Equals("No Existe"))
                {
                    //INSERTA LA INFORMACION
                    URL = EndPointsAPI.updateCliente;
                    data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                    responseString = UnicodeEncoding.UTF8.GetString(data);

                    if (responseString.Equals("1"))
                    {
                        await App.Current.MainPage.DisplayAlert("Éxito", "Información actualizada satisfactoriamente", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
                    }
                }
            }
        }
        
        public async void DeactivateCliente(Clientes cliente, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.deactivateCliente;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idCliente", cliente.idCliente + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Cliente desactivado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha desactivado", "OK");
            }

        }
        
        public List<Clientes> ListClientes()
        {
            string URL = EndPointsAPI.listarClientes;
            WebClient webClient = new WebClient();

            List<Clientes> listarClientes = new List<Clientes>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarClientes = JsonConvert.DeserializeObject<List<Clientes>>(responseString);
            }

            return listarClientes;
        }
        
        public Clientes BuscarCliente(int idCliente)
        {
            string URL = EndPointsAPI.buscarClientes;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idCliente", idCliente + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Clientes cliente = new Clientes();
            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                cliente = JsonConvert.DeserializeObject<Clientes>(responseString);
            }

            return cliente;
        }
        
        public Clientes TraerClienteActual()
        {
            string URL = EndPointsAPI.buscarClienteActual;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idUsuario", Preferences.Get("idUsuario", "0") + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Clientes cliente = new Clientes();
            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                cliente = JsonConvert.DeserializeObject<Clientes>(responseString);
            }

            return cliente;
        }
    }
}