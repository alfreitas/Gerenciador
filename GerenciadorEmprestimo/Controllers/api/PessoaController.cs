using GerenciadorEmprestimo.Business;
using GerenciadorEmprestimo.Business.Interface;
using GerenciadorEmprestimo.Entidades;
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
    [RoutePrefix("api/Pessoa")]
    public class PessoaController : ApiController
    {
        private IPessoaBusiness _pessoaBusiness;

        public IPessoaBusiness PessoaBusiness
        {
            get
            {
                if (_pessoaBusiness == null)
                {
                    _pessoaBusiness = StructureMapConfig.Resolve<IPessoaBusiness>();
                }
                return _pessoaBusiness;
            }
        }
        // GET: api/Pessoa
        [Route("Consultar")]
        [HttpGet]
        public object Consultar(int start = 0, int length = 0)
        {
            DataTable<Pessoa> datatable = new DataTable<Pessoa>();
            var lista = PessoaBusiness.Consultar();
            datatable.data = lista.ToArray();
            datatable.recordsTotal = lista.Count();
            return Json(datatable);
        }

        // POST: api/Pessoa
        [Route("Salvar")]
        [HttpPost]
        public void Salvar([FromBody]Pessoa pessoa)
        {
            PessoaBusiness.SalvarPessoa(pessoa);
        }

        [Route("Delete")]
        [HttpGet]
        public void Delete(int id)
        {
            PessoaBusiness.Excluir(id);
        }
    }
}
