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
    public class ContextStatusService : IContextStatusService
    {

        private readonly ApplicationDbContext _context;
        private readonly IThingService _thingService;
        public ContextStatusService(ApplicationDbContext context, IThingService thingService)
        {
            _context = context;
            _thingService = thingService;
        }
        public async Task<List<ContextStatus>> getAllCurrentContextStatus(int thingId)
        {
            var status = await _context.ActiveThingStatus
                .Where(x => x.thingId == thingId)
                .Include(x => x.statusContexts)
                .FirstOrDefaultAsync();
            return status.statusContexts.ToList();
        }

        public async Task<ContextStatus> getCurrentContextStatus(int thingId, string context)
        {
            var status = await _context.ActiveThingStatus
                           .Where(x => x.thingId == thingId)
                           .Include(x => x.statusContexts)
                           .FirstOrDefaultAsync();

            return status.statusContexts.Where(x => x.context == context).FirstOrDefault();
        }

        public async Task<List<HistoryContextStatus>> getHistoryContextStatus(int thingId, string context)
        {
            var status = await _context.HistoryThingStatus
                           .Where(x => x.thingId == thingId)
                           .Include(x => x.statusContexts)
                           .ToListAsync();

            var returnList = new List<HistoryContextStatus>();
            foreach (var item in status)
            {
                var contextStatus = item.statusContexts.Where(x => x.context == context).ToList();
                if (contextStatus.Count != 0)
                    returnList.AddRange(contextStatus);
            }
            return returnList;

        }

        public async Task<List<ContextStatus>> updateCurrentContextStatus(int thingId, string context, ContextStatus newContextStatus, bool recurrent = false)
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
            var returnstatusList = new List<ContextStatus>();
            foreach (var item in thingUpdateList)
            {
                var status = await updateThingCurrentStatus(item, context, JsonConvert.SerializeObject(newContextStatus));
                returnstatusList.Add(status);
            }
            return returnstatusList;
        }

        public async Task saveHistoryContextStatus(int thingId, string context)
        {

            var curStatus = await _context.ActiveThingStatus
                           .Where(x => x.thingId == thingId)
                           .Include(x => x.statusContexts)
                           .FirstOrDefaultAsync();
            var updatedStatus = curStatus.statusContexts.Where(x => x.context == context).FirstOrDefault();
            var histThingStatus = new HistoryThingStatus();
            histThingStatus.thingId = thingId;
            histThingStatus.statusContexts = new List<HistoryContextStatus>();
            if (updatedStatus != null)
            {

                var histContextStatus = new HistoryContextStatus();
                histContextStatus.context = updatedStatus.context;
                histContextStatus.contextDescription = updatedStatus.contextDescription;
                histContextStatus.endTimestampTicks = DateTime.Now.Ticks;
                histContextStatus.startTimestampTicks = updatedStatus.startTimestampTicks;
                histContextStatus.statusName = updatedStatus.statusName;
                histContextStatus.value = updatedStatus.value;
                histThingStatus.statusContexts.Add(histContextStatus);
                _context.HistoryThingStatus.Add(histThingStatus);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<ContextStatus> updateThingCurrentStatus(int thingId, string context, string newContextString)
        {
            var newContextStatus = JsonConvert.DeserializeObject<ContextStatus>(newContextString);
            newContextStatus.context = newContextStatus.context.ToLower().Replace(" ", string.Empty);
            newContextStatus.startTimestampTicks = DateTime.Now.Ticks;
            var status = await _context.ActiveThingStatus
                              .Where(x => x.thingId == thingId)
                              .Include(x => x.statusContexts)
                              .FirstOrDefaultAsync();
            if (status == null)
            {
                var thingStatus = new ThingStatus();
                thingStatus.thingId = thingId;
                thingStatus.statusContexts = new List<ContextStatus>();
                thingStatus.statusContexts.Add(newContextStatus);
                _context.ActiveThingStatus.Add(thingStatus);
                await _context.SaveChangesAsync();
                return newContextStatus;
            }
            if (status.statusContexts == null)
                status.statusContexts = new List<ContextStatus>();
            await saveHistoryContextStatus(thingId, context);
            var curContextStatus = status.statusContexts.Where(x => x.context == context).FirstOrDefault();
            if (curContextStatus == null)
            {
                status.statusContexts.Add(newContextStatus);
            }
            else
            {
                var index = status.statusContexts.IndexOf(curContextStatus);
                status.statusContexts[index] = newContextStatus;
            }
            _context.Entry(status).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return newContextStatus;
        }


    }
}
