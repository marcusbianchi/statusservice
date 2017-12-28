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
    public class ThingStatusController : Controller
    {
        private readonly IThingStatusService _thingStatusService;
        public ThingStatusController(IThingStatusService thingStatusService)
        {
            _thingStatusService = thingStatusService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var status = await _thingStatusService.getAllCurrentStatus();
            return Ok(status);
        }

        [HttpGet("history/{thingId}")]
        public async Task<IActionResult> GetHistoryList(int thingId)
        {
            var thingStatus = await _thingStatusService.getHistoryStatus(thingId, null, null);
            return Ok(thingStatus);
        }

        [HttpGet("list/")]
        public async Task<IActionResult> GetList([FromQuery]int[] thingId)
        {
            var thingStatus = await _thingStatusService.getListCurrentStatus(thingId);
            return Ok(thingStatus);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var thingStatus = await _thingStatusService.getCurrentStatus(id);
            if (thingStatus == null)
                return NotFound();

            return Ok(thingStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ThingStatus thingStatus, [FromQuery]bool recurrent = false)
        {
            if (ModelState.IsValid)
            {
                var curStatus = await _thingStatusService.updateCurrentStatus(id, thingStatus, recurrent);
                if (curStatus == null)
                    return NotFound();
                return NoContent();

            }
            return BadRequest(ModelState);
        }



    }
}