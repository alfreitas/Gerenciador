using System.Web.Mvc;

namespace GerenciadorEmprestimo.Controllers
{
    [Authorize]
    public class HistoricoController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

    }
}
