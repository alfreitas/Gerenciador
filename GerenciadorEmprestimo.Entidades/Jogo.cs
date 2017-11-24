using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Entidades
{
    public class Jogo : EntidadeBase
    {
        public virtual string Titulo { get; set; }
        public virtual string Genero { get; set; }
        public virtual Pessoa Locatario { get; set; }
        public virtual DateTime? Data { get; set; }
    }
}
