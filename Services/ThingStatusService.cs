using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using statusservice.Data;
using statusservice.Model;
using statusservice.Services.Interfaces;

namespace statusservice.Services
{
    public class ThingStatusService : IThingStatusService
    {

        private readonly ApplicationDbContext _context;
        private readonly IThingService _thingService;
        public ThingStatusService(ApplicationDbContext context, IThingService thingService)
        {
            _context = context;
            _thingService = thingService;
        }
        public async Task<ThingStatus> getCurrentStatus(int thingId)
        {
            var status = await _context.ActiveThingStatus
                .Where(x => x.thingId == thingId)
                .Include(x => x.statusContexts)
                .FirstOrDefaultAsync();
            var (thing, code) = await _thingService.getThing(thingId);
            if (code != HttpStatusCode.OK)
                return null;
            status.thing = thing;
            return status;

        }

        public async Task<List<HistoryThingStatus>> getHistoryStatus(int thingId)
        {
            var status = await _context.HistoryThingStatus
                .Where(x => x.thingId == thingId)
                .Include(x => x.statusContexts)
                .ToListAsync();
            var (thing, code) = await _thingService.getThing(thingId);
            if (code != HttpStatusCode.OK)
                return null;
            foreach (var item in status)
            {
                item.thing = thing;
            }
            return status;
        }

        public async Task<List<ThingStatus>> getListCurrentStatus(int[] thingId)
        {
            List<ThingStatus> returnList = new List<ThingStatus>();
            foreach (var item in thingId)
            {
                var status = await _context.ActiveThingStatus
                    .Where(x => x.thingId == item)
                    .Include(x => x.statusContexts)
                    .FirstOrDefaultAsync();
                status.thing = (await _thingService.getThing(item)).Item1;
                if (status != null)
                    returnList.Add(status);
            }
            return returnList;
        }
        public async Task<List<ThingStatus>> updateCurrentStatus(int thingId, ThingStatus newStatus, bool recurrent = false)
        {
            var thingUpdateList = new List<int>();
            thingUpdateList.Add(thingId);
            if (recurrent)
            {
                for (int i = 0; i < thingUpdateList.Count; i++)
                {
                    var (childrenThings, status) = await _thingService.getChildrenThingList(thingUpdateList[i]);
                    if (status == HttpStatusCode.OK && childrenThings.Count != 0)
                        thingUpdateList.AddRange(childrenThings.Select(x => x.thingId));
                }
            }
            var returnstatusList = new List<ThingStatus>();
            foreach (var item in thingUpdateList)
            {
                var status = await updateThingCurrentStatus(item, JsonConvert.SerializeObject(newStatus));
                returnstatusList.Add(status);
            }
            return returnstatusList;
        }

        public async Task saveHistoryStatus(int thingId)
        {
            var status = await _context.ActiveThingStatus
                 .Where(x => x.thingId == thingId)
                 .Include(x => x.statusContexts)
                 .FirstOrDefaultAsync();
            var historyStatus = new HistoryThingStatus();
            historyStatus.thing = status.thing;
            historyStatus.thingId = status.thingId;
            historyStatus.statusContexts = new List<HistoryContextStatus>();
            foreach (var item in status.statusContexts)
            {
                var historyStatusContext = new HistoryContextStatus();
                historyStatusContext.context = item.context;
                historyStatusContext.contextDescription = item.contextDescription;
                historyStatusContext.startTimestampTicks = item.startTimestampTicks;
                historyStatusContext.statusName = item.statusName;
                historyStatusContext.value = item.value;
                historyStatusContext.endTimestampTicks = DateTime.Now.Ticks;
                historyStatus.statusContexts.Add(historyStatusContext);
            }
            _context.HistoryThingStatus.Add(historyStatus);
            await _context.SaveChangesAsync();
        }

        private async Task<ThingStatus> updateThingCurrentStatus(int thingId, string newStatusString)
        {
            var newStatus = JsonConvert.DeserializeObject<ThingStatus>(newStatusString);
            newStatus.thingStatusId = 0;
            newStatus.thingId = thingId;
            foreach (var item in newStatus.statusContexts)
            {
                item.startTimestampTicks = DateTime.Now.Ticks;
                item.contextStatusId = 0;
                item.context = item.context.ToLower().Replace(" ", string.Empty);
            }

            var thing = _thingService.getThing(thingId);
            if (thing == null)
                return null;
            if (newStatus.statusContexts == null)
                newStatus.statusContexts = new List<ContextStatus>();
            var status = await _context.ActiveThingStatus
                  .Where(x => x.thingId == thingId)
                  .Include(x => x.statusContexts)
                  .FirstOrDefaultAsync();
            if (status == null)
            {
                _context.ActiveThingStatus.Add(newStatus);
                await _context.SaveChangesAsync();
                return newStatus;
            }
            await saveHistoryStatus(thingId);
            _context.Entry(status).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            _context.ActiveThingStatus.Add(newStatus);
            await _context.SaveChangesAsync();
            return newStatus;
        }

        public async Task<List<ThingStatus>> getAllCurrentStatus()
        {
            var status = await _context.ActiveThingStatus
                          .Include(x => x.statusContexts).ToListAsync();
            foreach (var item in status)
            {
                var (thing, code) = await _thingService.getThing(item.thingId.Value);
                if (code != HttpStatusCode.OK)
                    return null;
                item.thing = thing;
            }
            return status;
        }
    }
}
