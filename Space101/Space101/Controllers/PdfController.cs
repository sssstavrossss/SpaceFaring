using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPdf;

namespace Space101.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdf
        public ActionResult PdfPrint()
        {
            return View();
        }
    }
}