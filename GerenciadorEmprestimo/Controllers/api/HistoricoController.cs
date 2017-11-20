using GerenciadorEmprestimo.Business.Interface;
using GerenciadorEmprestimo.Models;
using GerenciadorEmprestimo.ResolvedorDependencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GerenciadorEmprestimo.Controllers.api
{

    [RoutePrefix("api/Historico")]
    public class HistoricoController : ApiController
    {
        private IHistoricoBusiness _historicoBusiness;

        public IHistoricoBusiness HistoricoBusiness
        {
            get
            {
                if (_historicoBusiness == null)
                {
                    _historicoBusiness = StructureMapConfig.Resolve<IHistoricoBusiness>();
                }
                return _historicoBusiness;
            }
        }

        // GET: api/Historico
        [Route("Consultar")]
        [HttpGet]
        public object Consultar(int start = 0, int length = 0)
        {
            DataTable<HistoricoModel> datatable = new DataTable<HistoricoModel>();
            List<HistoricoModel> listaModel = new List<HistoricoModel>();
            var lista = HistoricoBusiness.Consultar();
            foreach (var item in lista)
            {
                HistoricoModel model = new HistoricoModel();
                model.Amigo = item.Pessoa.Nome;
                model.DataFim = item.DataFim.ToShortDateString();
                model.DataInicio = item.DataInicio.ToShortDateString();
                model.Jogo = item.Jogo.Titulo;
                listaModel.Add(model);
            }
            datatable.data = listaModel.ToArray();
            datatable.recordsTotal = lista.Count();
            return Json(datatable);
        }
    }
}
