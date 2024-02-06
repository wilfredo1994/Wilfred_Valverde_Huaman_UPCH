using Azure;
using Microsoft.AspNetCore.Mvc;
using Wilfred_Valverde_Huaman_UPCH.Entities;
using Wilfred_Valverde_Huaman_UPCH.Logic;
using Wilfred_Valverde_Huaman_UPCH.ViewModels;

namespace Wilfred_Valverde_Huaman_UPCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        TipoDocumentoLogic tipoDocumentoLogic = new TipoDocumentoLogic();
        
        [HttpGet]
        [Route("ListarTipoDocumentos")]
        public async Task<ActionResult<GlobalResponse<VistaTipoDocumento>>> ListarTipoDocumentos()
        {
            VistaTipoDocumento tpdoc = new VistaTipoDocumento();
            List<ListaTipoDocumento> tipoDocumento = new List<ListaTipoDocumento>();
            tipoDocumento = tipoDocumentoLogic.GetTipoDocumentos();
            
            if (tipoDocumento.Count > 0)
            {
                tpdoc.listaTipoDocumento = tipoDocumento;
                var response = new GlobalResponse<VistaTipoDocumento>
                {
                    Codigo = 200,
                    Mensaje = "Datos obtenidos correctamente.",
                    Data = tpdoc
                };

                return Ok(response);
            }
            else
            {
                var response = new GlobalResponse<VistaTipoDocumento>
                {
                    Codigo = 204,
                    Mensaje = "No se encontraron documentos",
                    Data = tpdoc
                };

                return BadRequest(response);
            }

        }

        [HttpGet]
        [Route("ListarTipoDocumento")]
        public async Task<ActionResult<GlobalResponse<VistaTipoDocumento>>> ListarTipoDocumento(int IdTipoDocumento)
        {
            VistaTipoDocumento tpdoc = new VistaTipoDocumento();
            List<ListaTipoDocumento> tipoDocumento = new List<ListaTipoDocumento>();
            tipoDocumento = tipoDocumentoLogic.GetTipoDocumento(IdTipoDocumento);

            if (tipoDocumento.Count > 0)
            {
                tpdoc.listaTipoDocumento = tipoDocumento;
                var response = new GlobalResponse<VistaTipoDocumento>
                {
                    Codigo = 200,
                    Mensaje = "Datos obtenidos correctamente.",
                    Data = tpdoc
                };

                return Ok(response);
            }
            else
            {
                var response = new GlobalResponse<VistaTipoDocumento>
                {
                    Codigo = 204,
                    Mensaje = "No se encontraron documentos asociados al IdTipoDocumento",
                    Data = tpdoc
                };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("RegistrarTipoDocumento")]
        public async Task<ActionResult<GlobalResponse<TipoDocumento>>> RegistrarTipoDocumento([FromBody] TipoDocumento tipoDocumento)
        {
            int newUserId = 0;

            if (tipoDocumento.Descripcion != null)
            {
                newUserId = tipoDocumentoLogic.RegistrarTipoDocumento(tipoDocumento);

                if (newUserId == 0)
                {
                    var responses = new GlobalResponse<TipoDocumento>
                    {
                        Codigo = 400,
                        Mensaje = "El documento ya se encuentra registrado"
                    };

                    return BadRequest(responses);

                }

                var response = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 201,
                    Mensaje = "Documento registrado correctamente"
                };

                return Ok(response);
            }
            else
            {
                var response = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 4002,
                    Mensaje = "Se debe ingresar la Descripción"
                };

                return BadRequest(response);
            }

        }


        [HttpPut]
        [Route("ActualizarTipoDocumento")]
        public async Task<ActionResult<GlobalResponse<TipoDocumento>>> ActualizarTipoDocumento([FromBody] TipoDocumento tipoDocumento)
        {
            int result = 0;
            if (tipoDocumento.IdTipoDocumento == 0)
            {
                var responses = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 4001,
                    Mensaje = "El IdTipoDocumento no puede ser cero (0)"
                };

                return BadRequest(responses);
            }else if (tipoDocumento.Descripcion == null)
            {
                var responses = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 4002,
                    Mensaje = "Se debe ingresar la Descripción"
                };

                return BadRequest(responses);
            }

            result = tipoDocumentoLogic.ActualizarTipoDocumento(tipoDocumento);
            if (result == 1)
            {
                var responses = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 201,
                    Mensaje = "Tipo de Documento actualizado correctamente"
                };
                return Ok(responses);
            }
            else
            {
                var response = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 400,
                    Mensaje = "El IdTipoDocumento no se encuentra registrado"
                };

                return BadRequest(response);
            }

        }


        [HttpDelete]
        [Route("EliminarTipoDocumento")]
        public async Task<ActionResult<GlobalResponse<VistaTipoDocumento>>> EliminarTipoDocumento(int IdTipoDocumento)
        {
            int result = 0;
            if (IdTipoDocumento == 0)
            {
                var responses = new GlobalResponse<TipoDocumento>
                {
                    Codigo = 4001,
                    Mensaje = "El IdTipoDocumento no puede ser cero (0)"
                };

                return BadRequest(responses);
            }
            else
            {
                result = tipoDocumentoLogic.EliminarTipoDocumento(IdTipoDocumento);
                if (result == 1)
                {
                    var responses = new GlobalResponse<TipoDocumento>
                    {
                        Codigo = 201,
                        Mensaje = "Tipo de Documento eliminado correctamente"
                    };
                    return Ok(responses);
                }
                else
                {
                    var response = new GlobalResponse<TipoDocumento>
                    {
                        Codigo = 400,
                        Mensaje = "El IdTipoDocumento no se encuentra registrado"
                    };

                    return BadRequest(response);
                }
            }

        }
    }
}
