using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using statusservice.Model;
using statusservice.Services.Interfaces;


namespace statusservice.Services
{
    public class ThingService : IThingService
    {
        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        public ThingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(List<Thing>, HttpStatusCode)> getChildrenThingList(int thingId)
        {
            List<Thing> returnThings = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/things/childrenthings/" + thingId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnThings = JsonConvert.DeserializeObject<List<Thing>>(await client.GetStringAsync(url));
                    return (returnThings, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnThings, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnThings, HttpStatusCode.InternalServerError);
            }
            return (returnThings, HttpStatusCode.NotFound);
        }

        public async Task<(Thing, HttpStatusCode)> getThing(int thingId)
        {
            Thing returnThing = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/things/" + thingId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnThing = JsonConvert.DeserializeObject<Thing>(await client.GetStringAsync(url));
                    return (returnThing, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnThing, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnThing, HttpStatusCode.InternalServerError);
            }
            return (returnThing, HttpStatusCode.NotFound);

        }

        public async Task<(List<Thing>, HttpStatusCode)> getThingList(int[] thingIds)
        {
            List<Thing> listThings = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/things/list?");
            string url = builder.ToString();
            foreach (var item in thingIds)
            {
                url += $"thingid={item}&";
            }
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    listThings = JsonConvert.DeserializeObject<List<Thing>>(await client.GetStringAsync(url));
                    return (listThings, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (listThings, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (listThings, HttpStatusCode.InternalServerError);
            }
            return (listThings, HttpStatusCode.NotFound);
        }
    }
}