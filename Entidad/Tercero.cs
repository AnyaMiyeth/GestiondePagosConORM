using System.Collections.Generic;

namespace Entidad
{
    public class Tercero
    {
        public string TerceroId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }   
        
        public List<Pago> Pagos { get; set; }
        
        
        
    }
}