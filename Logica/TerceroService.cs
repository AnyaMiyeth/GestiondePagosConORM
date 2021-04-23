using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Entidad;
using Microsoft.EntityFrameworkCore;

namespace Logica
{
    public class TerceroService
    {
        private readonly PagoContext _context;

        public TerceroService(PagoContext context)
        {
            _context = context;
        }

        public GuardarTerceroResponse GuardarTercero(Tercero tercero)
        {
            try
            {
                var _tercero = _context.Terceros.Find(tercero.TerceroId);
                if (_tercero == null)
                {
                    _context.Terceros.Add(tercero);
                    _context.SaveChanges();
                    return new GuardarTerceroResponse(tercero);
                }

                return new GuardarTerceroResponse("El tercero ya se encuentra Registrado");
            }
            catch (Exception e)
            {
                return new GuardarTerceroResponse("Ocurrieron algunos Errores:" + e.Message);
            }
        }

        public GuardarTerceroResponse Modificar(Tercero terceroNew, Tercero terceroOld)
        {
            try
            {
                var _terceroOld = _context.Terceros.Find(terceroOld.TerceroId);
                if (_terceroOld != null)
                {

                    var _terceroNew = _context.Terceros.Find(terceroOld.TerceroId);
                    if (_terceroNew == null)
                    {
                        _terceroOld.TerceroId = terceroNew.TerceroId;
                        _terceroOld.Nombre = terceroNew.Nombre;
                        _terceroOld.Telefono = terceroNew.Telefono;
                        _context.Terceros.Add(terceroNew);
                        _context.SaveChanges();
                        return new GuardarTerceroResponse(terceroNew);
                    }
                    return new GuardarTerceroResponse($"No es posible actualizar al tercero porque ya existe una persona con la identificación: {_terceroNew.TerceroId}");
                }
                return new GuardarTerceroResponse("El tercero que intenta modificar no se encuentra registrado");
            }
            catch (Exception e)
            {
                return new GuardarTerceroResponse("Ocurrieron algunos Errores:" + e.Message);
            }
        }


        public ConsultarTerceroResponse Consultar()
        {
            try
            {
                var terceros = _context.Terceros.ToList();
                return new ConsultarTerceroResponse(terceros);

            }
            catch (Exception e)
            {
                return new ConsultarTerceroResponse("Ocurriern algunos Errores:" + e.Message);
            }
        }

        public BuscarTerceroResponse BuscarTerceroConPagoPorId(string id)
        {
            try
            {
                var tercero = _context.Terceros.Where(t => t.TerceroId == id).Include(t => t.Pagos).FirstOrDefault();
                if (tercero != null)
                {
                    return new BuscarTerceroResponse(tercero);
                }
                return new BuscarTerceroResponse("El tercero consultado no existe");
            }
            catch (Exception e)
            {
                return new BuscarTerceroResponse("Ocurriern algunos Errores:" + e.Message);
            }

        }

         public BuscarTerceroResponse BuscarPorId(string id)
        {
            try
            {
                var tercero = _context.Terceros.Find(id);
                if (tercero != null)
                {
                    return new BuscarTerceroResponse(tercero);
                }
                return new BuscarTerceroResponse("El tercero consultado no existe");
            }
            catch (Exception e)
            {
                return new BuscarTerceroResponse("Ocurriern algunos Errores:" + e.Message);
            }

        }
    }
    public class GuardarTerceroResponse
    {
        public Tercero Tercero { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }


        public GuardarTerceroResponse(Tercero tercero)
        {
            Tercero = tercero;
            Error = false;
        }

        public GuardarTerceroResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }

    }

    public class BuscarTerceroResponse
    {
        public Tercero Tercero { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }


        public BuscarTerceroResponse(Tercero tercero)
        {
            Tercero = tercero;
            Error = false;
        }

        public BuscarTerceroResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }

    }


    public class ConsultarTerceroResponse
    {
        public List<Tercero> Terceros { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }


        public ConsultarTerceroResponse(List<Tercero> terceros)
        {
            Terceros = terceros;
            Error = false;
        }

        public ConsultarTerceroResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }

    }
}