using FicharApi.Services;
using FicharApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FicharApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService authService)
        {
            _service = authService;
        }
        
        // api/Auth/Login
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Login([FromBody] AuthUser authUserRequest)
        {
            ActionResult<String> response = Unauthorized();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    Usuarios LoginResponse = _service.Login(authUserRequest);
                    String token = "";
                    
                    if (LoginResponse.IsValid)
                    {
                        bool retorno = true;
                        response = Ok(retorno);
                    }
                    //else
                    //{
                    //    token = "Invalid Credentials";
                    //}

                    //response = Ok(token);
                }
                else
                {
                    response = BadRequest(ModelState.ValidationState);
                }

            }
            catch (Exception ex)
            {
                //response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                response = Unauthorized();
            }
            return response;
        }
    }
}
