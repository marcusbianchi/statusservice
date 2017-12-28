using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using statusservice.Data;
using statusservice.Model;
using statusservice.Services.Interfaces;


namespace statusservice.Controllers
{
    [Route("api/[controller]")]
    public class ContextStatusController : Controller
    {
        private readonly IContextStatusService _contextStatusService;
        public ContextStatusController(IContextStatusService contextStatusService)
        {
            _contextStatusService = contextStatusService;
        }


        [HttpGet("{thingId}")]
        public async Task<IActionResult> Get(int thingId)
        {
            var status = await _contextStatusService.getAllCurrentContextStatus(thingId);
            return Ok(status);
        }
        [HttpGet("{thingId}/{context}")]
        public async Task<IActionResult> Get(int thingId, string context)
        {
            var status = await _contextStatusService.getCurrentContextStatus(thingId, context);
            return Ok(status);
        }

        [HttpGet("history/{thingId}/{context}")]
        public async Task<IActionResult> Put(int thingId, string context)
        {

            if (ModelState.IsValid)
            {
                var curStatus = await _contextStatusService.getHistoryContextStatus(thingId, context);
                if (curStatus == null)
                    return NotFound();
                return Ok(curStatus);

            }
            return BadRequest(ModelState);
        }


        [HttpPut("{thingId}/{context}")]
        public async Task<IActionResult> Put(int thingId, string context, [FromBody]ContextStatus contextStatus, [FromQuery]bool recurrent = false)
        {
            if (context != contextStatus.context)
                ModelState.AddModelError("context", "The Contexts didn match.");
            if (ModelState.IsValid)
            {
                var curStatus = await _contextStatusService.updateCurrentContextStatus(thingId, context, contextStatus, recurrent);
                if (curStatus == null)
                    return NotFound();
                return NoContent();

            }
            return BadRequest(ModelState);
        }


    }
}