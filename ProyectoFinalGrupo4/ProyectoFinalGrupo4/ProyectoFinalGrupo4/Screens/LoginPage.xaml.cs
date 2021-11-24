using ProyectoFinalGrupo4.Respositories;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            if (repositorySesiones.RevisarToken())
            {
                rol.Text = Preferences.Get("idRol", "");
                Navigation.PushAsync(new MasterPage(int.Parse(rol.Text)));
            }
        }

        async void IniciarSesion(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Campos Vacíos", "Por favor, rellene todos los campos", "Ok");
            }
            else if (repositorySesiones.UsuarioLogin(txtUsuario.Text, txtPassword.Text))
            {
                rol.Text = Preferences.Get("idRol", "");
                Navigation.InsertPageBefore(new MasterPage(int.Parse(rol.Text)), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
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