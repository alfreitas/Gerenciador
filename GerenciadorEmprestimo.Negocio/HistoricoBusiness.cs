using GerenciadorEmprestimo.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorEmprestimo.Entidades;
using GerenciadorEmprestimo.Data.Interface;

namespace GerenciadorEmprestimo.Business
{
    public class HistoricoBusiness : IHistoricoBusiness
    {
        private IBaseData<HistoricoEmprestimo> _data;
        public HistoricoBusiness(IBaseData<HistoricoEmprestimo> data)
        {
            _data = data;
        }

        public List<HistoricoEmprestimo> Consultar()
        {
            return _data.Todos().ToList();
        }
    }
}
