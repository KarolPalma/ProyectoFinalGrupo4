using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalGrupo4.Respositories;
using ProyectoFinalGrupo4.Models;

namespace ProyectoFinalGrupo4.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BitacoraPage : ContentPage
    {
        RepositoryBitacora repository = new RepositoryBitacora();
        List<Bitacora> bitacora = new List<Bitacora>();

        public BitacoraPage()
        {
            InitializeComponent();
            //ESTO ES PARA LLAMAR A TODA LA LISTA
            bitacora = repository.ListarBitacora();
            lstBitacora.ItemsSource = bitacora;
        }

        public void Filtrar(object sender, TextChangedEventArgs e)
        {
            var buscador = srcBuscar.Text;
            
            List<Bitacora> listaBitacora;
            listaBitacora = repository.ListarBitacora();
            lstBitacora.ItemsSource = listaBitacora;

            lstBitacora.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                var sCadena = buscador;
                string[] s = sCadena.Split('/');
                string dia = s[0];
                string mes = s[1];
                string anio = s[2];

                //lstBitacora.ItemsSource = listaBitacora.Where(bitacora => string.Equals(bitacora.fecha, buscador, StringComparison.OrdinalIgnoreCase)).Where(bitacora => bitacora.fecha.ToString().ToUpper().Contains(buscador.ToUpper()));

                lstBitacora.ItemsSource = listaBitacora.Where(bitacora => string.Equals(bitacora.fecha.ToString(), mes+"/"+dia+"/"+anio + " 12:00:00 a. m.", StringComparison.OrdinalIgnoreCase)).Where(bitacora => bitacora.fecha.ToString().ToUpper().Contains(buscador.ToUpper()));
                lstBitacora.EndRefresh();
            }
            else
            {
                listaBitacora = repository.ListarBitacora();
                lstBitacora.ItemsSource = listaBitacora;
                lstBitacora.EndRefresh();
            }
        }

        private void srcBuscar_SearchButtonPressed(object sender, EventArgs e)
        {
            var buscador = srcBuscar.Text;

            List<Bitacora> listaBitacora;
            listaBitacora = repository.ListarBitacora();
            lstBitacora.ItemsSource = listaBitacora;

            lstBitacora.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(buscador))
            {
                var sCadena = buscador;
                string[] s = sCadena.Split('/');
                string dia = s[0];
                string mes = s[1];
                string anio = s[2];
                string fechaBusca = mes+"/"+dia+"/"+anio;

                //lstBitacora.ItemsSource = listaBitacora.Where(bitacora => string.Equals(bitacora.fecha, buscador, StringComparison.OrdinalIgnoreCase)).Where(bitacora => bitacora.fecha.ToString().ToUpper().Contains(buscador.ToUpper()));

                lstBitacora.ItemsSource = listaBitacora.Where(bitacora => string.Equals(bitacora.fecha.ToString(), fechaBusca + " 12:00:00 a. m.", StringComparison.OrdinalIgnoreCase)).Where(bitacora => bitacora.fecha.ToString().ToUpper().Contains((fechaBusca + " 12:00:00 a. m.").ToUpper()));
                lstBitacora.EndRefresh();
            }
            else
            {
                listaBitacora = repository.ListarBitacora();
                lstBitacora.ItemsSource = listaBitacora;
                lstBitacora.EndRefresh();
            }
        }
    }
}