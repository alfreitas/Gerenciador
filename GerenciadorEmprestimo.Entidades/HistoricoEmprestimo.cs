using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Entidades
{
    public class HistoricoEmprestimo : EntidadeBase
    {
        public virtual Jogo Jogo { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
    }
}
