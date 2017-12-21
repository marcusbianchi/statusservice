using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using statusservice.Services.Interfaces;

namespace statusservice.Controllers
{
    [Route("")]
    public class GatewayController : Controller
    {
        private HttpClient client = new HttpClient();
        private IConfiguration _configuration;
        private IThingService _thingService;

        public GatewayController(IConfiguration configuration, IThingService thingService)
        {
            _configuration = configuration;
            _thingService = thingService;
        }


        [HttpGet("gateway/things/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetThing(int id)
        {
            var (thing, resultCode) = await _thingService.getThing(id);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(thing);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}