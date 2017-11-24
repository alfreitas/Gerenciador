using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Entidades
{
    public class Usuario : EntidadeBase
    {
        public virtual string Login { get; set; }
        public virtual string Senha { get; set; }
        public virtual Boolean IsAdmin { get; set; }
        public virtual Pessoa Pessoa { get; set; }

    }
}
