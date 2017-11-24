using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Entidades
{
    [Serializable]
    public class Pessoa : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Cidade { get; set; }

    }
}
