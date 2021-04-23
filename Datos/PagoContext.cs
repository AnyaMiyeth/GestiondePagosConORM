using System;
using Entidad;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class PagoContext:DbContext
    {
        public PagoContext(DbContextOptions options ):base(options)
        {
            
        }

        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Tercero> Terceros { get; set; }
        
    }
}
