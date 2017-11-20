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
    [RoutePrefix("api/Jogo")]
    public class JogoController : ApiController
    {
        private IJogoBusiness _jogoBusiness;

        public IJogoBusiness JogoBusiness
        {
            get
            {
                if (_jogoBusiness == null)
                {
                    _jogoBusiness = StructureMapConfig.Resolve<IJogoBusiness>();
                }
                return _jogoBusiness;
            }
        }
        // GET: api/Jogo
        [Route("Consultar")]
        [HttpGet]
        public object Consultar(int start = 0, int length = 0)
        {
            DataTable<JogoModel> datatable = new DataTable<JogoModel>();
            var lista = JogoBusiness.Consultar();
            List<JogoModel> listaModel = new List<JogoModel>();
            foreach (var item in lista)
            {
                JogoModel jogo = new JogoModel();
                jogo.Codigo = item.Codigo;
                jogo.Genero = item.Genero;
                jogo.Titulo = item.Titulo;
                jogo.NomeAmigo = item.Locatario != null ? item.Locatario.Nome : string.Empty;
                listaModel.Add(jogo);
            }
            datatable.data = listaModel.ToArray();
            datatable.recordsTotal = lista.Count();
            return Json(datatable);
        }

        // POST: api/Jogo
        [Route("Salvar")]
        [HttpPost]
        public void Salvar([FromBody]Jogo jogo)
        {
            JogoBusiness.SalvarJogo(jogo);
        }

        // POST: api/Jogo
        [Route("Emprestar")]
        [HttpPost]
        public void Emprestar([FromBody]Jogo jogo)
        {
            jogo.Data = DateTime.Now;
            JogoBusiness.SalvarJogo(jogo);
        }

        [Route("Devolver")]
        [HttpGet]
        public void Devolver(int id)
        {
            JogoBusiness.DevolverJogo(id);
        }

        [Route("Delete")]
        [HttpGet]
        public void Delete(int id)
        {
            JogoBusiness.Excluir(id);
        }
    }

}
