namespace Exame_POO.DataBase.Entity
{
    public class BaseEntity : ProductoEntity
    {
        public Guid IdCliente { get; set; }

        public string ListaProductos { get; set; }
    }
}
