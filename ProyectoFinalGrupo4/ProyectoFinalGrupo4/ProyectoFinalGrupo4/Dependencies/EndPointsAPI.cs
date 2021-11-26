using System;

namespace ProyectoFinalGrupo4.Dependencies
{
    class EndPointsAPI
    {
        //public static String URL = "http://192.168.0.10:81/Sales_App/";
        public static String URL = "http://192.168.0.15:81/Sales_App/";

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
        public static String buscarAdminActual = URL + "administradores/buscarAdminActual.php";

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
        public static String insertCategoria = URL + "categorias/insertCategoria.php";
        public static String updateCategoria = URL + "categorias/actualizarCategoria.php";
        public static String deactivateCategoria = URL + "categorias/desactivarCategoria.php";
        public static String listarCategorias = URL + "categorias/listarCategorias.php";
        public static String buscarCategoria = URL + "categorias/buscarCategoria.php";
        public static String llenarCategorias = URL + "categorias/llenarCategorias.php";

        //EndPoints de Proveedores
        public static String insertProveedor = URL + "proveedores/insertProveedor.php";
        public static String updateProveedor = URL + "proveedores/actualizarProveedor.php";
        public static String deactivateProveedor = URL + "proveedores/desactivarProveedor.php";
        public static String listarProveedores = URL + "proveedores/listarProveedores.php";
        public static String buscarProveedor = URL + "proveedores/buscarProveedor.php";
        public static String llenarProveedores = URL + "proveedores/listarProveedores.php";

        //EndPoints de Impuestos
        public static String insertImpuesto = URL + "impuestos/insertImpuesto.php";
        public static String updateImpuesto = URL + "impuestos/actualizarImpuesto.php";
        public static String deactivateImpuesto = URL + "impuestos/desactivarImpuesto.php";
        public static String listarImpuestos = URL + "impuestos/listarImpuestos.php";
        public static String buscarImpuesto = URL + "impuestos/buscarImpuesto.php";
        public static String llenarImpuestos = URL + "impuestos/llenarImpuestos.php";
        public static String traerImpuesto = URL + "impuestos/traerImpuesto.php";

        //EndPoints de Descuentos
        public static String insertDescuento = URL + "descuentos/insertDescuento.php";
        public static String updateDescuento = URL + "descuentos/actualizarDescuento.php";
        public static String deactivateDescuento = URL + "descuentos/desactivarDescuento.php";
        public static String listarDescuentos = URL + "descuentos/listarDescuentos.php";
        public static String buscarDescuento = URL + "descuentos/buscarDescuento.php";
        public static String llenarDescuentos = URL + "descuentos/llenarDescuentos.php";
        public static String traerDescuento = URL + "descuentos/traerDescuento.php";

        //EndPoints de Bitacora
        public static String listarBitacora = URL + "bitacora/listarBitacora.php";
        public static String buscarBitacora = URL + "bitacora/buscarBitacora.php";

        //EndPoints de Ventas y Compras (porque mientras lo hacia olvide que eran lo mismo...)
        public static String verProductos = URL + "ventas/verProductos.php";
        public static String verificarInCarrito = URL + "ventas/verificarInCarrito.php";
        public static String insertCarrito = URL + "ventas/addCarrito.php";
        public static String traerCarrito = URL + "ventas/traerCarrito.php";
        public static String actualizarCarrito = URL + "ventas/actualizarCarrito.php";
        public static String eliminarDelCarrito = URL + "ventas/eliminarDelCarrito.php";
        public static String traerMetodosPago = URL + "ventas/traerMetodosPago.php";

        //EndPoints de Facturas Ventas
        public static String facturarVenta = URL + "facturasVentas/insertarFacturaVentas.php";
        public static String listarFacturasVentas = URL + "facturasVentas/listarFacturasVentas.php";
        public static String listarFacturasVentasUsuario = URL + "facturasVentas/listarFacturasVentasUsuario.php";
        public static String recuperarFacturaGet = URL + "reporteria/facturaPDF.php";
        public static String recuperarReporteVentasGet = URL + "reporteria/reporteVentasPDF.php";

        //EndPoints de Facturas Compras
        public static String facturarCompra = URL + "facturasCompras/insertarFacturaCompras.php";
        public static String listarFacturasCompras = URL + "facturasCompras/listarFacturasCompras.php";
        public static String listarFacturasComprasUsuario = URL + "facturasCompras/listarFacturasComprasUsuario.php";
        public static String recuperarFacturaCompraGet = URL + "reporteria/facturaCompraPDF.php";
        public static String recuperarReporteComprasGet = URL + "reporteria/reporteComprasPDF.php";

    }
}