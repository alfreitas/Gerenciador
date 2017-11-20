using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Business.Interface
{
    public interface IHistoricoBusiness
    {
        List<HistoricoEmprestimo> Consultar();
    }
}
