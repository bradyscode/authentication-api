using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authentication_dot_net.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("/test")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string Test()
        {
            return "Authorized User";
        }
    }
}
