using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Entidad;
using PagoModel.Model;

namespace TerceroModel.Model
{
    public class TerceroInputModel
    {
        public string TerceroId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class TerceroViewModel
    {
        public string TerceroId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public TerceroViewModel(Tercero tercero)
        {
            TerceroId = tercero.TerceroId;
            Nombre = tercero.Nombre;
            Telefono = tercero.Telefono;
        }
    }

    public class TerceroConPagosViewModel
    {
        public string TerceroId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public List<InformacionPagoViewModel> Pagos { get; set; }
        public TerceroConPagosViewModel(Tercero tercero)
        {
            TerceroId = tercero.TerceroId;
            Nombre = tercero.Nombre;
            Telefono = tercero.Telefono;
            Pagos = tercero.Pagos.Select(p => new InformacionPagoViewModel(p)).ToList();
        }
    }
}