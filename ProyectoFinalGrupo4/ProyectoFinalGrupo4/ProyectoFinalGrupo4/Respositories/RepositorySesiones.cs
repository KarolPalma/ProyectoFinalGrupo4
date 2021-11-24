using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using ProyectoFinalGrupo4.Screens;
using System.Net;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Respositories
{
    public class RepositorySesiones
    {

        public bool UsuarioLogin(string usuario, string clave)
        {
            bool sesionIniciada = false;
            string URL = EndPointsAPI.login;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("usuario", usuario);
            webClient.QueryString.Add("clave", clave);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (!responseString.Equals("SESION NO INICIADA"))
            {
                Preferences.Set("token", responseString);
                if (RevisarToken())
                {
                    sesionIniciada = true;
                }
            }
            return sesionIniciada;
        }

        public bool RevisarToken()
        {
            bool tokenExiste = false;
            var tokenLocal = Preferences.Get("token", "");
            Administradores administrador = new Administradores();
            Clientes cliente = new Clientes();

            string URL = EndPointsAPI.revisarToken;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("token", tokenLocal);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("SESION NO INICIADA"))
            {
                Preferences.Set("token", "");
                Preferences.Set("idUsuario", "");
                Preferences.Set("usuario", "");
                Preferences.Set("idRol", "");
                Preferences.Set("idSesionUsuario", "");

                //PASAR AL LOGIN
                //App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                tokenExiste = true;
                if (responseString.Contains("idAdmin"))
                {
                    administrador = JsonConvert.DeserializeObject<Administradores>(responseString);
                    Preferences.Set("idUsuario", administrador.idUsuario + "");
                    Preferences.Set("usuario", administrador.usuario);
                    Preferences.Set("idRol", "1");
                    Preferences.Set("idSesionUsuario", administrador.idAdmin + "");
                }
                else
                {
                    cliente = JsonConvert.DeserializeObject<Clientes>(responseString);
                    Preferences.Set("idUsuario", cliente.idUsuario + "");
                    Preferences.Set("usuario", cliente.usuario);
                    Preferences.Set("idRol", "2");
                    Preferences.Set("idSesionUsuario", cliente.idCliente + "");
                }
            }
            return tokenExiste;
        }
    }
}