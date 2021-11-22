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
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
        }
        async void Registro(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtClave.Text) || string.IsNullOrEmpty(txtConfirmacion.Text))
            {
                await DisplayAlert("Llenado de Datos", "Debe llenar todos los campos", "Ok");
            }
            else if (!txtConfirmacion.Text.Equals(txtClave.Text))
            {
                await DisplayAlert("Llenado de Datos", "La confirmación de la clave es incorrecta", "Ok");
            }
            else if (repositoryUsuarios.BuscarUsuario(txtUsuario.Text) is object)
            {
                await DisplayAlert("Usuario Existente", "No se guardó el usuario pues este ya existe", "Ok");
            }
            else if (repositoryUsuarios.BuscarUsuario(txtUsuario.Text) is null)
            {
                //Crear Nuevo
                repositoryUsuarios.InsertUsuario(txtUsuario.Text, txtClave.Text, true);
                await DisplayAlert("Éxito", "El usuario se ha guardado correctamente, será redireccionado a la pantalla de inicio de sesión", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }*/

            await Navigation.PushAsync(new LoginPage());
        }
    }
}