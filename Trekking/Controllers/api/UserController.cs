using System;
using System.Collections.Generic;
using System.Web.Http;
using Trekking.Repository.Models.DB;
using Trekking.Services;


namespace Trekking.Controllers.api
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login([FromBody] UserModel userData)
        {
            UserModel result = UserService.Login(userData);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("api/user/register")]
        public IHttpActionResult Register([FromBody] UserModel userData)
        {
            bool? result = UserService.Register(userData);
            return Ok(result);
        }
    }
}