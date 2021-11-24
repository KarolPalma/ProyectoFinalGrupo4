using Newtonsoft.Json;
using ProyectoFinalGrupo4.Dependencies;
using ProyectoFinalGrupo4.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Respositories
{
    public class RepositoryProductos : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        //METODOS
        public async Task<bool> InsertProducto(Productos producto, int idUsuarioActual)
        {
            //INSERTA LOS DATOS
            string URL = EndPointsAPI.insertProducto;
            bool insertado = false;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("nombre", producto.nombre);
            webClient.QueryString.Add("descripcion", producto.descripcion);
            webClient.QueryString.Add("idCategoria", producto.idCategoria + "");
            webClient.QueryString.Add("idProveedor", producto.idProveedor + "");
            webClient.QueryString.Add("cantidadUnidad", producto.cantidadUnidad);
            webClient.QueryString.Add("unidadesAlmacen", producto.unidadesAlmacen + "");
            webClient.QueryString.Add("cantidadMinima", producto.cantidadMinima + "");
            webClient.QueryString.Add("cantidadMaxima", producto.cantidadMaxima + "");
            webClient.QueryString.Add("foto", "null"); //SI SE VA A ENVIAR NULL DEBE SER "null" como string
            webClient.QueryString.Add("precio", producto.precio + "");
            webClient.QueryString.Add("idImpuesto", producto.idImpuesto + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                var fotoJson = new
                {
                    id = producto.idProducto,
                    foto = producto.foto
                };

                // Serialize our concrete class into a JSON String
                var stringPayload = JsonConvert.SerializeObject(fotoJson);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                responseString = "0";
                HttpResponseMessage response = await client.PostAsync(EndPointsAPI.subirImagen, content).ConfigureAwait(false);
                responseString = response.Content.ReadAsStringAsync().Result;
                System.Threading.Thread.Sleep(1000);
                if (responseString == "1")
                {
                    insertado = true;
                }
            }
            return insertado;
        }

        public async Task<bool> UpdateProducto(Productos producto, int idUsuarioActual)
        {
            //ACTUALIZA LOS DATOS
            string URL = EndPointsAPI.updateProducto;
            bool insertado = false;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idProducto", producto.idProducto + "");
            webClient.QueryString.Add("nombre", producto.nombre);
            webClient.QueryString.Add("descripcion", producto.descripcion);
            webClient.QueryString.Add("idCategoria", producto.idCategoria + "");
            webClient.QueryString.Add("idProveedor", producto.idProveedor + "");
            webClient.QueryString.Add("cantidadUnidad", producto.cantidadUnidad);
            webClient.QueryString.Add("unidadesAlmacen", producto.unidadesAlmacen + "");
            webClient.QueryString.Add("cantidadMinima", producto.cantidadMinima + "");
            webClient.QueryString.Add("cantidadMaxima", producto.cantidadMaxima + "");
            webClient.QueryString.Add("foto", "null"); //SI SE VA A ENVIAR NULL DEBE SER "null" como string
            webClient.QueryString.Add("estado", producto.estado + "");
            webClient.QueryString.Add("precio", producto.precio + "");
            webClient.QueryString.Add("idImpuesto", producto.idImpuesto + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                var fotoJson = new
                {
                    id = producto.idProducto,
                    foto = producto.foto
                };

                // Serialize our concrete class into a JSON String
                var stringPayload = JsonConvert.SerializeObject(fotoJson);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                responseString = "0";
                HttpResponseMessage response = await client.PostAsync(EndPointsAPI.subirImagen, content).ConfigureAwait(false);
                responseString = response.Content.ReadAsStringAsync().Result;
                System.Threading.Thread.Sleep(1000);
                if (responseString == "1")
                {
                    insertado = true;
                }
            }
            return insertado;

        }

        public async void DeactivateProducto(Productos producto, int idUsuarioActual)
        {
            //SOLO REQUIERE EL ID DEL PRODUCTO
            string URL = EndPointsAPI.deactivateProducto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idProducto", producto.idProducto + "");
            webClient.QueryString.Add("usuarioActual", idUsuarioActual + "");
            webClient.QueryString.Add("tipoDispositivo", DeviceInfo.Manufacturer + " " + DeviceInfo.Model);
            webClient.QueryString.Add("sistemaOperativo", DeviceInfo.Platform + " " + DeviceInfo.VersionString);

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals("1"))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Producto desactivado satisfactoriamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se ha desactivado", "OK");
            }

        }

        public List<Productos> ListProductos()
        {
            string URL = EndPointsAPI.listarProductos;
            WebClient webClient = new WebClient();

            List<Productos> listarProductos = new List<Productos>();

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                listarProductos = JsonConvert.DeserializeObject<List<Productos>>(responseString);
            }

            return listarProductos;
        }

        public Productos BuscarProductos(int idProducto)
        {
            string URL = EndPointsAPI.buscarProducto;
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("idProducto", idProducto + "");

            var data = webClient.UploadValues(URL, "POST", webClient.QueryString);
            string responseString = UnicodeEncoding.UTF8.GetString(data);

            Productos producto = new Productos();
            if (responseString.Equals(""))
            {
                App.Current.MainPage.DisplayAlert("Error", "No se pueden consultar los datos", "OK");
            }
            else
            {
                producto = JsonConvert.DeserializeObject<Productos>(responseString);
            }

            return producto;
        }

    }
}