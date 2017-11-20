using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorEmprestimo.Models
{
    public class HistoricoModel : EntidadeBase
    {
        public string Jogo { get; set; }
        public string Amigo { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
    }
}