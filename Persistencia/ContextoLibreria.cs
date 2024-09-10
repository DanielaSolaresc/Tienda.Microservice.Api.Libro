using Microsoft.EntityFrameworkCore;
using Tienda.Microservice.Api.Libro.Modelo;

namespace Tienda.Microservice.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) 
        {
            
        }

        public DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
