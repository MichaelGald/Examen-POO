using Exame_POO.DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace Exame_POO.DataBase
{
    public class GestionPedidosContext : DbContext
    {
        public GestionPedidosContext(DbContextOptions options) : base(options) 
        {
           
        }
        public DbSet<ClienteEntity> ClienteEntities { get; set; }
        public DbSet<ProductoEntity> ProductoEntities { get; set; }
        public DbSet<PedidoEntity> PedidoEntities { get; set; }

    }
}
    
