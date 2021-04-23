using System.Linq;
using System.Reflection;
using System.Resources;

using Microsoft.AspNetCore.Mvc;
using Logica;
using Datos;
using Entidad;
using PagoModel.Model;
using TerceroModel.Model;
using System.Collections.Generic;

namespace PagoTercero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private PagoService pagoservice;
        public PagoController(PagoContext context)
        {
            pagoservice = new PagoService(context);
        }

        [HttpPost]
        public ActionResult<PagoViewModel> PostPago(PagoInputModel pagoInput)
        {

            var pago = MapearPago(pagoInput);
            var response = pagoservice.GuardarPago(pago);
            if (!response.Error)
            {
                var PagoViewModel = new PagoViewModel(pago);
                return Ok(PagoViewModel);
            }
            return BadRequest(response.Mensaje);
        }


        [HttpGet]
        public ActionResult<IEnumerable<PagoViewModel>> GetPagos()
        {
            var response = pagoservice.Consultar();
            if (!response.Error)
            {
                var PagoViewModels = response.Pagos.Select(p => new PagoViewModel(p));
                return Ok(PagoViewModels);
            }
            return BadRequest(response.Mensaje);
        }

        private Pago MapearPago(PagoInputModel pagoInput)
        {
            var pago = new Pago()
            {
                PagoId = pagoInput.PagoId,
                TerceroId = pagoInput.Tercero.TerceroId,
                Valor = pagoInput.Valor,
                Fecha = pagoInput.Fecha,
                Tercero = new Tercero() { TerceroId = pagoInput.Tercero.TerceroId, Nombre = pagoInput.Tercero.Nombre, Telefono = pagoInput.Tercero.Telefono },
                PorcentajeIva = pagoInput.PorcentajeIva,
            };
            return pago;
        }

        private Tercero MapearTercero(TerceroInputModel terceroInput)
        {
            var tercero = new Tercero()
            {
                TerceroId = terceroInput.TerceroId,
                Nombre = terceroInput.Nombre,
                Telefono = terceroInput.Telefono,
            };
            return tercero;
        }

    }
}