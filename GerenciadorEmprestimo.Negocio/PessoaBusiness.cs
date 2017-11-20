
using GerenciadorEmprestimo.Business.Interface;
using GerenciadorEmprestimo.Data.Interface;
using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GerenciadorEmprestimo.Business
{
    public class PessoaBusiness : IPessoaBusiness
    {
        private IBaseData<Pessoa> _data;
        public PessoaBusiness(IBaseData<Pessoa> data)
        {
            _data = data;
        }

        public List<Pessoa> Consultar()
        {
            return _data.Todos().ToList();
        }

        public void Excluir(int id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    _data.Deletar(_data.Obter(d => d.Codigo == id));
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool SalvarPessoa(Pessoa pessoa)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _data.IncluirOuAlterar(pessoa);
                    ts.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
