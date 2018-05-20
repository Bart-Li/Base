using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newegg.EC.Core.Configuration;
using Newegg.EC.Core.Configuration.Impl;
using Newegg.EC.Core.DataAccess;
using Newegg.EC.Core.DataAccess.Config;
using Newegg.EC.Core.Host;
using Newegg.EC.Core.Log;
using Newegg.EC.Core.RestClient;
using Newegg.EC.Core.RestClient.Config;
using Newegg.EC.Core.RestClient.Impl;
using Newegg.EC.Core.Web.Context;

namespace Newegg.EC.Core.Test.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static string test = string.Empty;
        public ValuesController()
        {
        }

        // GET api/values
        [HttpGet]
        public object Get()
        {
            //var serviceHost = ECLibraryContainer.Get<IServiceHostRepository>();
            //var data = serviceHost.GetAllService("SoBigdataService");
            //return data;
            var logger = ECLibraryContainer.Get<ILogManager>();
            var httpContext = ECLibraryContainer.Get<IHttpContextRepository>();
            //logger.AddVariable("content", "hello world");
            logger.Trace("sfsssddsfslog", "CallServiceError");
            logger.Info(httpContext.CurrentUrl);
            logger.Info(httpContext.CurrentIP);
            logger.Info(httpContext.CurrentUrlReferrer);
            logger.Info(httpContext.CurrentUserAgent);
            logger.Info(httpContext.QueryStringOrHeader("CountryCode"));


            //var dBManager = ECLibraryContainer.Get<IDBManager>();
            //IDataCommand command = dBManager.CreateDBCommand("QueryNonExemptTax");
            //command.AddParameter("@UUID", "44B68E28-90EF-E711-80D6-0050569340A7");
            //var response = command.ExecuteDataTable();
            //return response;

            //return response;

            //return data;

            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
