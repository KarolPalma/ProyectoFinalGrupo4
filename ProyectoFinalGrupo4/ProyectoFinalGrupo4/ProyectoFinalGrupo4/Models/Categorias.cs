namespace ProyectoFinalGrupo4.Models
{
    public class Categorias
    {
        public int idCategoria { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }

        public Categorias()
        {
        }

        public Categorias(string categoria, string descripcion)
        {
            this.categoria = categoria;
            this.descripcion = descripcion;
        }

        public Categorias(int idCategoria, string categoria, string descripcion, bool activo)
        {
            this.idCategoria = idCategoria;
            this.categoria = categoria;
            this.descripcion = descripcion;
            this.activo = activo;
        }
    }
}