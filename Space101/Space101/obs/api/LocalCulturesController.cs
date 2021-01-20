using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Space101.Controllers.api
{
    public class LocalCulturesController : ApiController
    {
        public IHttpActionResult GetLocalCultureName()
        {
            var serverCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

            return Ok(serverCultureName);
        }
    }
}
