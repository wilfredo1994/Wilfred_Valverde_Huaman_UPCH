using Microsoft.AspNetCore.Mvc;
using Wilfred_Valverde_Huaman_UPCH.Entities;
using Wilfred_Valverde_Huaman_UPCH.Logic;
using Wilfred_Valverde_Huaman_UPCH.ViewModels;

namespace Wilfred_Valverde_Huaman_UPCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        PersonaLogic personaLogic = new PersonaLogic();
        TipoDocumentoLogic tipoDocumentoLogic = new TipoDocumentoLogic();

        [HttpGet]
        [Route("ListarPersonas")]
        public async Task<ActionResult<GlobalResponse<VistaPersona>>> ListarPersonas()
        {
            VistaPersona vPersona = new VistaPersona();
            List<Persona> listaPersona = new List<Persona>();
            listaPersona = personaLogic.GetPersonas();

            if (listaPersona.Count > 0)
            {
                vPersona.listaPersona = listaPersona;
                var response = new GlobalResponse<VistaPersona>
                {
                    Codigo = 200,
                    Mensaje = "Datos obtenidos correctamente.",
                    Data = vPersona
                };

                return Ok(response);
            }
            else
            {
                var response = new GlobalResponse<VistaPersona>
                {
                    Codigo = 204,
                    Mensaje = "No se encontraron documentos",
                    Data = vPersona
                };

                return BadRequest(response);
            }

        }

        [HttpGet]
        [Route("ListarPersona")]
        public async Task<ActionResult<GlobalResponse<VistaPersona>>> ListarPersona(int IdPersona)
        {
            VistaPersona vPersona = new VistaPersona();
            List<Persona> listaPersona = new List<Persona>();
            listaPersona = personaLogic.GetPersona(IdPersona);

            if (listaPersona.Count > 0)
            {
                vPersona.listaPersona = listaPersona;
                var response = new GlobalResponse<VistaPersona>
                {
                    Codigo = 200,
                    Mensaje = "Datos obtenidos correctamente.",
                    Data = vPersona
                };

                return Ok(response);
            }
            else
            {
                var response = new GlobalResponse<VistaPersona>
                {
                    Codigo = 204,
                    Mensaje = "No se encontraron personas asociadas al IdPersona",
                    Data = vPersona
                };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("RegistrarPersona")]
        public async Task<ActionResult<GlobalResponse<Persona>>> RegistrarTipoDocumento([FromBody] Persona persona)
        {
            int newUserId = 0;

            List<ListaTipoDocumento> tipoDocumento = new List<ListaTipoDocumento>();
            tipoDocumento = tipoDocumentoLogic.GetTipoDocumento(persona.TipoDocumento.IdTipoDocumento);

            if (!(tipoDocumento.Count > 0))
            {
                var responses = new GlobalResponse<Persona>
                {
                    Codigo = 400,
                    Mensaje = "El IdTipoDocumento no se encuentra registrado"
                };

                return BadRequest(responses);
            }

            if (!(persona.TipoDocumento.IdTipoDocumento == null || persona.NumeroDocumento == null ||
            persona.Nombres == null || persona.ApellidoPaterno == null || persona.ApellidoMaterno == null ||
            persona.Telefono == null || persona.Correo == null || persona.Direccion == null))
            {
                newUserId = personaLogic.RegistrarPersona(persona);

                if (newUserId == 0)
                {
                    var responses = new GlobalResponse<Persona>
                    {
                        Codigo = 400,
                        Mensaje = "El numero de documento ya se encuentra registrado"
                    };

                    return BadRequest(responses);

                }

                var response = new GlobalResponse<Persona>
                {
                    Codigo = 201,
                    Mensaje = "Persona registrada correctamente"
                };
                return Ok(response);

            }
            else
            {
                var response = new GlobalResponse<Persona>
                {
                    Codigo = 4002,                
                    Mensaje = "El IdTipoDocumento, NumeroDocumento, Nombres, ApellidoPaterno, ApellidoMaterno, Telefono, Correo y Direccion son obligatorios"
                };

                return BadRequest(response);
            }

        }

        [HttpPut]
        [Route("ActualizarPersona")]
        public async Task<ActionResult<GlobalResponse<Persona>>> ActualizarPersona([FromBody] Persona persona)
        {
            int result = 0;
            if (persona.IdPersona == 0)
            {
                var responses = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 4001,
                    Mensaje = "El IdPersona no puede ser cero (0)"
                };

                return BadRequest(responses);
            }
            else if (persona.TipoDocumento.IdTipoDocumento == null || persona.NumeroDocumento == null ||
                    persona.Nombres == null || persona.ApellidoPaterno == null || persona.ApellidoMaterno == null ||
                    persona.Telefono == null || persona.Correo == null || persona.Direccion == null)
            {
                var response = new GlobalResponse<Persona>
                {
                    Codigo = 4002,
                    Mensaje = "El IdTipoDocumento, NumeroDocumento, Nombres, ApellidoPaterno, ApellidoMaterno, Telefono, Correo y Direccion son obligatorios"
                };

                return BadRequest(response);
            }

            result = personaLogic.ActualizarPersona(persona);
            
            if (result == 0)
            {
                var response = new GlobalResponse<Persona>
                {
                    Codigo = 400,
                    Mensaje = "El IdPersona no se encuentra registrado"
                };

                return BadRequest(response);
            }
            else if (result == -1)
            {
                var response = new GlobalResponse<Persona>
                {
                    Codigo = 400,
                    Mensaje = "El IdTipoDocumento no se encuentra registrado"
                };

                return BadRequest(response);
            }
            else if (result == -2)
            {
                var response = new GlobalResponse<Persona>
                {
                    Codigo = 400,
                    Mensaje = "El Numero de documento se encuentra registrado en otra Persona"
                };

                return BadRequest(response);
            }else                
            {
                var responses = new GlobalResponse<Persona>
                {
                    Codigo = 201,
                    Mensaje = "Persona actualizada correctamente"
                };
                return Ok(responses);
            }


        }

        [HttpDelete]
        [Route("EliminarPersona")]
        public async Task<ActionResult<GlobalResponse<Persona>>> EliminarPersona(int IdPersona)
        {
            int result = 0;
            if (IdPersona == 0)
            {
                var responses = new GlobalResponse<Persona>
                {
                    Codigo = 4001,
                    Mensaje = "El IdPersona no puede ser cero (0)"
                };

                return BadRequest(responses);
            }
            else
            {
                result = personaLogic.EliminarPersona(IdPersona);
                if (result == 1)
                {
                    var responses = new GlobalResponse<Persona>
                    {
                        Codigo = 201,
                        Mensaje = "Persona eliminada correctamente"
                    };
                    return Ok(responses);
                }
                else
                {
                    var response = new GlobalResponse<Persona>
                    {
                        Codigo = 400,
                        Mensaje = "El IdPersona no se encuentra registrado"
                    };

                    return BadRequest(response);
                }
            }

        }
    }
}
