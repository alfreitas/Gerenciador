using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorEmprestimo.Controllers
{
    [Authorize]
    public class PessoaController : Controller
    {
        // GET: Pessoa
        public ActionResult Index()
        {
            return View();
        }


    }
}
