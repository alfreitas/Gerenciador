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
    public class JogoBusiness : IJogoBusiness
    {
        private IBaseData<Jogo> _data;
        private IBaseData<Pessoa> _dataPessoa;
        private IBaseData<HistoricoEmprestimo> _dataHistorico;

        public JogoBusiness(IBaseData<Jogo> data, IBaseData<Pessoa> dataPessoa, IBaseData<HistoricoEmprestimo> dataHistorico)
        {
            _data = data;
            _dataPessoa = dataPessoa;
            _dataHistorico = dataHistorico;
        }

        public List<Jogo> Consultar()
        {
            return _data.Todos().ToList();
        }

        public void Excluir(int id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    _data.Deletar(_data.Obter(d => d.Codigo == id)); ;
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool SalvarJogo(Jogo jogo)
        {
            try
            {
                if (jogo.Codigo > 0)
                {

                    var jogoAlterado = _data.Obter(d => d.Codigo == jogo.Codigo);
                    if (jogo.Locatario != null && jogo.Locatario.Codigo > 0)
                    {
                        jogoAlterado.Locatario = _dataPessoa.Obter(d => d.Codigo == jogo.Locatario.Codigo);
                    }
                    jogoAlterado.Data = DateTime.Now;
                    using (TransactionScope ts = new TransactionScope())
                    {
                        _data.IncluirOuAlterar(jogoAlterado);
                        ts.Complete();
                    }
                }else
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        _data.IncluirOuAlterar(jogo);
                        ts.Complete();
                    }
                }

                return true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void DevolverJogo(int id)
        {
            try
            {

                var jogoAlterado = _data.Obter(d => d.Codigo == id);
                HistoricoEmprestimo historico = new HistoricoEmprestimo();
                historico.DataFim = DateTime.Now;
                historico.DataInicio = jogoAlterado.Data.Value;
                historico.Pessoa = jogoAlterado.Locatario;
                historico.Jogo = jogoAlterado;

                using (TransactionScope ts = new TransactionScope())
                {
                    _dataHistorico.IncluirOuAlterar(historico);
                    jogoAlterado.Locatario = null;
                    jogoAlterado.Data = null;
                    _data.IncluirOuAlterar(jogoAlterado);
                    ts.Complete();
                }



            }
            catch (Exception e)
            {

                throw;
            }
        }
    }

}
