

using Microsoft.EntityFrameworkCore;
//
namespace MascotasCRUDApi.Models
{
    // esta clase lo que nos va a permitir es crear la base de datos
    //acceder a la bd para hacer el crud
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        { 
        }

        public DbSet<Mascota> Mascotas { get; set; }
    }
}
