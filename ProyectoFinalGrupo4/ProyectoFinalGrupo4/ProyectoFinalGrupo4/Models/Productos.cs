﻿using System;
using System.IO;

using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Models
{
    public class Productos
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idCategoria { get; set; }
        public string categoria { get; set; }
        public int idProveedor { get; set; }
        public string proveedor { get; set; }
        public string cantidadUnidad { get; set; } //DESCRIPCION DE LA UNIDAD (3 lb, 18 onzas, 3 paquetes, etc.)
        public int unidadesAlmacen { get; set; }
        public int cantidadMinima { get; set; }
        public int cantidadMaxima { get; set; }
        public string foto { get; set; }
        public bool estado { get; set; }
        public double precio { get; set; }
        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public double porcentajeImpuesto { get; set; }
        public Xamarin.Forms.ImageSource fotoSource => Base64StringToImageSource(this.foto);
        public double precioConImpuesto => (precio * (porcentajeImpuesto / 100)) + precio;
        public string precioImpuestoVisualizacion { get; set; }
        public double precioConDescuento { get; set; }
        public int unidadesCarrito { get; set; }
        public int idDescuento { get; set; }


        public void SetPrecioConDescuento(double porcentajeDescuento) //Porcentual (Precio final después de imptos y desctos)
        {
            this.precioConDescuento = precioConImpuesto - (precio * (porcentajeDescuento / 100));
        }

        public ImageSource Base64StringToImageSource(string source)
        {
            if (source is null)
            {
                string notFound = "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0QHRAdEB0QHRAdFBdEB0QHRAdEB0QHRAdEB0EB0QHRAdEB0QHRAdEB0QHRAdRAdEB0QHRAdEB0QHRAdEB9EB0QHRAdEB0QHRAdEB0UF0QHRAdEB0QHRAdEB0QHRAdBAdEB0QHRAdEB0QHRAdEB1EB0QHRAdEB0QHRAdEB0QH0QHRAdEB0QHRAdEB0QHRAdFBdEB0QHRAdEB0QHRAdEB0EB0QHRAdEB0QHRAdEB0QHUQHRAdEB0QHRAdEB0QHRAdEB9EB0QHRAdEB0QHRAdEB0UF0QHRAdEB0QHRAdEB0QHQQHRAdEB0QHRAdEB0QHRAdEB1EB0QHRAdEB0QHRAdEB0QH0QHRAdEB0QHRAdEB0QHRQXRAdEB0QHRAdEB0QHRAdEB0EB0QHRAdEB0QHRAdEB0QHUQHRAdEB0QHRAdEB0QHRAfRAdEB0QHRAdEB0QHRAdEB0UF0QHRAdEB0QHRAdEB0QHQQHRAdEB0QHRAdEB0QHRAdRAdEB0QHRAdEB0QHRAdEB1JAdLhDdEB0QHRAdEB0QHRAdEB0QHQQHRAdEB0QHRAdEB0QHRAdRAdEB0QHRAdEB0QHRAdEB9EB0QHRAdEB0QHRAdEB0QHRQXRAdEB0QHRAdEB0QHRAdBAdEB0QHRAdEB0QHRAdEB1EB0QHRAdEB0QHRAdEB0QHBuX9Tll+BjMhAAAAAElFTkSuQmCC";
                var byteArray = Convert.FromBase64String(notFound);
                Stream stream = new MemoryStream(byteArray);
                return ImageSource.FromStream(() => stream);
            }
            else
            {
                var byteArray = Convert.FromBase64String(source);
                Stream stream = new MemoryStream(byteArray);
                return ImageSource.FromStream(() => stream);
            }

        }

        public Productos()
        {
        }

        public Productos(string nombre, string descripcion, int idCategoria, int idProveedor, string cantidadUnidad, int unidadesAlmacen, int cantidadMinima, int cantidadMaxima, string foto, double precio, int idImpuesto)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.idCategoria = idCategoria;
            this.idProveedor = idProveedor;
            this.cantidadUnidad = cantidadUnidad;
            this.unidadesAlmacen = unidadesAlmacen;
            this.cantidadMinima = cantidadMinima;
            this.cantidadMaxima = cantidadMaxima;
            this.foto = foto;
            this.precio = precio;
            this.idImpuesto = idImpuesto;
        }

        public Productos(int idProducto, string nombre, string descripcion, int idCategoria, string categoria, int idProveedor, string proveedor, string cantidadUnidad, int unidadesAlmacen, int cantidadMinima, int cantidadMaxima, string foto, bool estado, double precio, int idImpuesto)
        {
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.idCategoria = idCategoria;
            this.categoria = categoria;
            this.idProveedor = idProveedor;
            this.proveedor = proveedor;
            this.cantidadUnidad = cantidadUnidad;
            this.unidadesAlmacen = unidadesAlmacen;
            this.cantidadMinima = cantidadMinima;
            this.cantidadMaxima = cantidadMaxima;
            this.foto = foto;
            this.estado = estado;
            this.precio = precio;
            this.idImpuesto = idImpuesto;
        }
    }
}