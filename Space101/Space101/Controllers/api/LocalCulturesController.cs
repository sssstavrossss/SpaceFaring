using System.Web.Http;

namespace Space101.Controllers.api
{
    public class LocalCulturesController : ApiController
    {
        //Do Not remove or delete the controler 
        //The custom validations use it
        public IHttpActionResult GetLocalCultureName()
        {
            var serverCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

            return Ok(serverCultureName);
        }
    }
}
