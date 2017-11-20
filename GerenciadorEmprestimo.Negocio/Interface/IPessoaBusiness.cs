using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Business.Interface
{
    public interface IPessoaBusiness
    {
        bool SalvarPessoa(Pessoa pessoa);
        List<Pessoa> Consultar();
        void Excluir(int id);
    }
}
