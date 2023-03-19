using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers.Abstract
{
    [Route("[controller]s")]
    [ApiController]
    [Authorize("user")]
    public class BaseController : ControllerBase { }
}
