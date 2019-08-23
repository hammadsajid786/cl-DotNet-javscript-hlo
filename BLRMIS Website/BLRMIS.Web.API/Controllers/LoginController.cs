using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfigService _configService;
        private delegate void LoginSuccessEventHandler(object data);
        private event LoginSuccessEventHandler LoginSuccessEvent; 
        public LoginController(ILoginService loginService, IConfigService configService)
        {
            _loginService = loginService;
            _configService = configService;
            //LoginSuccessEvent += GetUserSettings; 
        }

        [HttpPost]
        public IActionResult Login(LoginModel request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password)) {
                return BadRequest(new APIResponse("NULL_PARAMETERS", "User name or Password is null or Empty")); 
            }
            var Response = _loginService.Login(request.UserName, request.Password);
            if (Response.Authenticated)
            {
                OnLoginSuccess(Response); 
                return Ok(Response);
            }
            else
                return Unauthorized();
        }


        private void GetUserSettings(object loginResponseModel)
        {
            var _loginResponseModel = (LoginResponseModel)loginResponseModel; 
            if (_loginResponseModel != null)
            {
              //  _loginResponseModel.Settings = new ConfigModel();
              //  _loginResponseModel.Settings.Functions = _configService.GetUserFunctions(1);
            }
        }

        private void OnLoginSuccess(object data) {
            if (LoginSuccessEvent != null) {
                LoginSuccessEvent(data);
            }
        }
    }
}