using System.Collections.Generic;
using System.Linq;
using Datos;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Mvc;
using TerceroModel.Model;

namespace PagoTercero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TerceroController : ControllerBase
    {
        private readonly TerceroService terceroService;
        public TerceroController(PagoContext context)
        {
            terceroService = new TerceroService(context);
        }

        [HttpPost]
        public ActionResult<TerceroViewModel> PostTercero(TerceroInputModel TerceroInput)
        {

            var tercero = MapearTercero(TerceroInput);
            var response = terceroService.GuardarTercero(tercero);
            if (!response.Error)
            {
                var TerceroViewModel = new TerceroViewModel(tercero);
                return Ok(TerceroViewModel);
            }
            return BadRequest(response.Mensaje);
        }

        [HttpGet("{id}")]
        public ActionResult<TerceroViewModel> GetTerceroId(string id)
        {
            var response = terceroService.BuscarPorId(id);
            if (!response.Error)
            {
                var terceroViewModel = new TerceroViewModel(response.Tercero);
                return Ok(terceroViewModel);
            }
            return BadRequest(response.Mensaje);
        }

        [HttpGet("{id}/Pagos")]
        public ActionResult<TerceroConPagosViewModel> GetTerceroConPagos(string id)
        {
            var response = terceroService.BuscarTerceroConPagoPorId(id);
            if (!response.Error)
            {
                var terceroConPagoViewModel = new TerceroConPagosViewModel(response.Tercero);
                return Ok(terceroConPagoViewModel);
            }
            return BadRequest(response.Mensaje);
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<TerceroViewModel>> GetTercero()
        {
            var response = terceroService.Consultar();
            if (!response.Error)
            {
                var terceroViewModels = response.Terceros.Select(p => new TerceroViewModel(p));
                return Ok(terceroViewModels);
            }
            return BadRequest(response.Mensaje);
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