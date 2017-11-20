using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Data.Interface
{
    public interface IBaseData<T>
    {
       
        T IncluirOuAlterar(T entidade);

       
        void Deletar(T entidade);

       
        T Obter(Expression<Func<T, bool>> predicado);

     
        IEnumerable<T> Todos(int qtd = 0);

      
        IEnumerable<T> Consultar(Expression<Func<T, bool>> predicado);

        
    }
}
