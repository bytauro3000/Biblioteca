using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Controllers {
    public class InicioController : Controller {
        [HttpGet]
        public ActionResult Index() {
            return View();
        }
    }
}