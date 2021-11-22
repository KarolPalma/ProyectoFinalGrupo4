using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace ProyectoFinalGrupo4
{
    public class RepositoryAdministradores : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        //METODOS
        public async void InsertAdministrador(Administradores administrador, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.verificarIde; 
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("identificacion", administrador.identificacion);
            webClient.QueryString.Add("nombres", administrador.nombres);
            webClient.QueryString.Add("apellidos", administrador.apellidos);
            webClient.QueryString.Add("telefono", administrador.telefono);
            webClient.QueryString.Add("nacimiento", administrador.fechaNacimiento.Date.Year + "-" + administrador.fechaNacimiento.Date.Month + "-" + administrador.fechaNacimiento.Date.Day);
            webClient.QueryString.Add("usuario", administrador.usuario);
            webClient.QueryString.Add("correo", administrador.correo);
            webClient.QueryString.Add("clave", administrador.clave);
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("Existe"))
            {
                await App.Current.MainPage.DisplayAlert("Error", "La identificación especificada ya pertenece a un administrador registrado", "OK");
            }
            else if (responseString.Equals("No Existe"))
            {
                //SOLO PREGUNTA SI EXISTE EL USUARIO EN LA BD
                URL = EndPointsAPI.verificarUsuario; 
                data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                responseString = UnicodeEncoding.UTF8.GetString(data);

                if (responseString.Equals("Existe"))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Este nombre de usuario ya está en uso", "OK");
                }
                else if (responseString.Equals("No Existe"))
                {
                    //INSERTA LA INFORMACION
                    URL = EndPointsAPI.insertAdmin; 
                    data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                    responseString = UnicodeEncoding.UTF8.GetString(data);

                    if (responseString.Equals("1"))
                    {
                        await App.Current.MainPage.DisplayAlert("Éxito", "Administrador guardado satisfactoriamente", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se ha guardado", "OK");
                    }
                }
            }
        }
        
        public async void UpdateAdministrador(Administradores administrador, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.verificarIde;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idAdmin", administrador.idAdmin + "");
            webClient.QueryString.Add("identificacion", administrador.identificacion);
            webClient.QueryString.Add("nombres", administrador.nombres);
            webClient.QueryString.Add("apellidos", administrador.apellidos);
            webClient.QueryString.Add("telefono", administrador.telefono);
            webClient.QueryString.Add("nacimiento", administrador.fechaNacimiento.Date.Year + "-" + administrador.fechaNacimiento.Date.Month + "-" + administrador.fechaNacimiento.Date.Day);
            webClient.QueryString.Add("usuario", administrador.usuario);
            webClient.QueryString.Add("correo", administrador.correo);
            webClient.QueryString.Add("clave", administrador.clave);
            webClient.QueryString.Add("estado", administrador.estado + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("Existe"))
            {
                await App.Current.MainPage.DisplayAlert("Error", "La identificación especificada ya pertenece a un administrador registrado", "OK");
            }
            else if (responseString.Equals("No Existe"))
            {
                //SOLO PREGUNTA SI EXISTE EL USUARIO EN LA BD
                URL = EndPointsAPI.verificarUsuario;
                data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                responseString = UnicodeEncoding.UTF8.GetString(data);

                if (responseString.Equals("Existe"))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Este nombre de usuario ya está en uso", "OK");
                }
                else if (responseString.Equals("No Existe"))
                {
                    //INSERTA LA INFORMACION
                    URL = EndPointsAPI.updateAdmin;
                    data = webClient.UploadValues(URL, "POST", webClient.QueryString);
                    responseString = UnicodeEncoding.UTF8.GetString(data);

                    if (responseString.Equals("1"))
                    {
                        await App.Current.MainPage.DisplayAlert("Éxito", "Administrador actualizado satisfactoriamente", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se ha actualizado", "OK");
                    }
                }
            }
        }

        public async void DeactivateAdministrador(Administradores administrador, int idUsuarioActual)
        {
            //SOLO PREGUNTA SI EXISTE LA IDENTIFICACION EN LA BD
            string URL = EndPointsAPI.deactivateAdmin;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idAdmin", administrador.idAdmin + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Administrador desactivado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha desactivado", "OK");
            }
                
        }

        public List<Administradores> ListAdministradores()
        {
            string URL = EndPointsAPI.listarAdmins;
            WebClient webClient = new WebClient();

            List<Administradores> listarAdministradores = new List<Administradores>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarAdministradores = JsonConvert.DeserializeObject<List<Administradores>>(responseString);
            }
            
            return listarAdministradores;
        }

        public Administradores BuscarAdministrador(int idAdmin)
        {
            string URL = EndPointsAPI.buscarAdmin;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idAdmin", idAdmin + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Administradores administrador = new Administradores();
            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                administrador = JsonConvert.DeserializeObject<Administradores>(responseString);
            }

            return administrador;
        }

    }

}