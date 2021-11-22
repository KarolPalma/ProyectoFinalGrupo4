using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void IniciarSesion(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync();
            /*if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Campos Vacíos", "Por favor, rellene todos los campos", "Ok");
            }
            else if (repositoryUsuarios.UsuarioLogin(txtUsuario.Text, txtPassword.Text))
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Inicio de Sesión", "Usuario o contraseña incorrectos o inválidos", "Ok");
            }*/

        }

        async void Registrarse(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPage());
        }
    }
}