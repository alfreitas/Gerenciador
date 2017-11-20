using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorEmprestimo.Models
{
    [Serializable]
    public class JogoModel : EntidadeBase
    {
        public string Genero { get; set; }
        public string Titulo { get; set; }
        public string NomeAmigo { get; set; }
        public string CodigoAmigo { get; set; }
    }
}