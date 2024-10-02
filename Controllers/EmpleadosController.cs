using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using FicharApi.Services;
using System.Threading.Tasks;
using System.Threading;

namespace FicharApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosService _iempleadosservice;
        
        public EmpleadosController(IEmpleadosService iempleadosservice)
        {
            _iempleadosservice = iempleadosservice;
        }
        
        //GET api/Usuarios/EmpresaUsuario/{usuario}
        [HttpGet("{empresa}/{empleado}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CompuestoEmpleadoAsync(string empresa, string empleado)
        {
            try
            {
                string compuestoEmpleado = await _iempleadosservice.CompuestoEmpleadoAsync(empresa, empleado, default);
                return Ok(new { compuestoEmpleado });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        //GET api/Usuarios/ConductoresEmpresa/{empresa}
        [HttpGet("{empresa}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConductoresEmpresaAsync(string empresa)
        {
            try
            {
                var result = await _iempleadosservice.ConductoresEmpresaAsync(empresa, default);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        //GET api/Usuarios/EmpleadosEmpresas/
        [HttpGet()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmpleadosEmpresasAsync()
        {
            try
            {
                var result = await _iempleadosservice.EmpleadosEmpresasAsync(default);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        


    }
}