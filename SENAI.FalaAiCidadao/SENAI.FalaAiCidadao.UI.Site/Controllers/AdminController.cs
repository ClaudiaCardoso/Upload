using SENAI.FalaAiCidadao.UI.Site.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SENAI.FalaAiCidadao.UI.Site.Controllers
{
    [CustomAuthorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}