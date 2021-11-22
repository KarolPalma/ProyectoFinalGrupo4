using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Dependencies
{
    class EndPointsAPI
    {
        public static String URL = "http://192.168.0.10:81/Sales_App/";
        //public static String URL = "http://192.168.0.15:81/Sales_App/";

        //EndPoints de Sesion
        public static String login = URL + "sesiones/login.php";
        public static String revisarToken = URL + "sesiones/revisarToken.php";

        //EndPoints de Administradores
        public static String verificarIde = URL + "administradores/verificarIdentificacion.php";
        public static String verificarUsuario = URL + "administradores/verificarUsuario.php";
        public static String insertAdmin = URL + "administradores/insertarAdmin.php";
        public static String updateAdmin = URL + "administradores/actualizarAdmin.php";
        public static String deactivateAdmin = URL + "administradores/desactivarAdmin.php";
        public static String listarAdmins = URL + "administradores/listarAdmins.php";
        public static String buscarAdmin = URL + "administradores/buscarAdmin.php";

        //EndPoints de Clientes
        public static String verificarIdeCliente = URL + "clientes/verificarIdentificacion.php";
        public static String verificarUsuarioCliente = URL + "clientes/verificarUsuario.php";
        public static String insertCliente = URL + "clientes/insertarCliente.php";
        public static String updateCliente = URL + "clientes/actualizarCliente.php";
        public static String deactivateCliente = URL + "clientes/desactivarCliente.php";
        public static String listarClientes = URL + "clientes/listarClientes.php";
        public static String buscarClientes = URL + "clientes/buscarCliente.php";
        public static String buscarClienteActual = URL + "clientes/buscarClienteActual.php";

        //EndPoints de Productos
        public static String insertProducto = URL + "productos/insertarProducto.php";
        public static String updateProducto = URL + "productos/actualizarProducto.php";
        public static String deactivateProducto = URL + "productos/desactivarProducto.php";
        public static String listarProductos = URL + "productos/listarProductos.php";
        public static String buscarProducto = URL + "productos/buscarProducto.php";
        public static String subirImagen = URL + "productos/guardarImagen.php";

        //EndPoints de Categorias
        public static String llenarCategorias = URL + "categorias/llenarCategorias.php";

        //EndPoints de Proveedores
        public static String llenarProveedores = URL + "proveedores/llenarProveedores.php";

        //EndPoints de Impuestos
        public static String llenarImpuestos = URL + "impuestos/llenarImpuestos.php";
        public static String traerImpuesto = URL + "impuestos/traerImpuesto.php";

        //EndPoints de Descuentos
        public static String llenarDescuentos = URL + "descuentos/llenarDescuentos.php";
        public static String traerDescuento = URL + "descuentos/traerDescuento.php";

        //EndPoints de Bitacora
        public static String listarBitacora = URL + "bitacora/listarBitacora.php";
        public static String buscarBitacora = URL + "bitacora/buscarBitacora.php";

        //EndPoints de Ventas
        public static String verProductos = URL + "ventas/verProductos.php";
        public static String verificarInCarrito = URL + "ventas/verificarInCarrito.php";
        public static String insertCarrito = URL + "ventas/addCarrito.php";
        public static String traerCarrito = URL + "ventas/traerCarrito.php";
        public static String actualizarCarrito = URL + "ventas/actualizarCarrito.php";
        public static String eliminarDelCarrito = URL + "ventas/eliminarDelCarrito.php";

        public static String traerMetodosPago = URL + "ventas/traerMetodosPago.php";

    }
}