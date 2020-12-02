using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMSApi.Common.Models;
using IMSApi.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }


        [HttpPost]
        public IActionResult Login([FromBody]AuthenticateRequest request)
        {
            var response = _authenticationManager.Authenticate(request);
            if (!response.IsSuccess)
            {
                return Problem(title:response.Message,statusCode: response.StatusCode);

            }
            return Ok(response);
        }
    }
}
