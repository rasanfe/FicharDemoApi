using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using FicharApi.Services;
using System.Data.Common;

namespace FicharApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SistemaController : ControllerBase
    {
        private readonly ISistemaService _isistemaservice;
        private readonly IConfiguration _configuration;
        
        public SistemaController(ISistemaService isistemaservice, IConfiguration configuration)
        {
            _isistemaservice = isistemaservice;
            _configuration = configuration;
        }
        
        [HttpGet("{empresa}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> NombreEmpresaAsync(string empresa)
        {

            try
            {
                var nombreEmpresa = await _isistemaservice.NombreEmpresaAsync(empresa, default);

                return Ok(new { nombreEmpresa });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }


}
