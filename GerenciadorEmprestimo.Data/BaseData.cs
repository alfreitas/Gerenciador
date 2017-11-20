

using GerenciadorEmprestimo.Data.Interface;
using GerenciadorEmprestimo.Entidades;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Data
{
    public class BaseData<T> : IBaseData<T> where T : EntidadeBase
    {
        private readonly ISession _session;

        public BaseData(ISession session)
        {
            _session = session;
            _session.FlushMode = FlushMode.Commit;
        }


        public T IncluirOuAlterar(T entidade)
        {
            if (entidade.Codigo > 0)
            {
                _session.Evict(_session.Load<T>(entidade.Codigo));
                _session.SaveOrUpdate(entidade);

            }
            else
            {
                entidade.Codigo = (int)_session.Save(entidade);
            }
            return entidade;
        }

        public void Deletar(T entidade)
        {
            if (entidade.Codigo > 0)
            {
                _session.Evict(_session.Load<T>(entidade.Codigo));
            }
            _session.Delete(entidade);
            _session.Flush();

        }

        public T Obter(Expression<Func<T, bool>> predicado)
        {
            return _session.Query<T>().Where(predicado).FirstOrDefault(); ;
        }

        public IEnumerable<T> Todos(int qtd = 0)
        {
            var query = _session.Query<T>();
            if (qtd > 0)
            {
                return query.Take(qtd).ToList();
            }
            return query.ToList();
        }

        public IEnumerable<T> Consultar(Expression<Func<T, bool>> predicado)
        {
            var retorno = _session.Query<T>().CacheMode(CacheMode.Refresh).Where(predicado);
            return retorno.ToList();

        }

        public virtual Func<T, object> ObterCriterioDeOrdenacaoPorCampo(string campoOrdenacao)
        {
            return null;
        }


    }
}
