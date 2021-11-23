using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Respositories;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        RepositorySesiones repositorySesiones = new RepositorySesiones();

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void IniciarSesion(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Campos Vacíos", "Por favor, rellene todos los campos", "Ok");
            }
            else if (repositorySesiones.UsuarioLogin(txtUsuario.Text, txtPassword.Text))
            {
                if (repositorySesiones.RevisarToken())
                {
                    txtUsuario.Text = Preferences.Get("usuario", "");
                    rol.Text = Preferences.Get("idRol", "");
                    idSesionUsuario.Text = Preferences.Get("idSesionUsuario", "");
                    int unu = int.Parse(Preferences.Get("idSesionUsuario", ""));
                    //Navigation.InsertPageBefore(new MainPage(txtUsuario.Text, rol.Text), Navigation.NavigationStack[0]);
                    Navigation.InsertPageBefore(new MasterPage(int.Parse(rol.Text)), Navigation.NavigationStack[0]);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert("Error al Iniciar de Sesión", "No se pudo iniciar sesión", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Inicio de Sesión", "Usuario o contraseña incorrectos o inválidos", "Ok");
            }

        }

        async void Registrarse(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPage());
        }
    }
}