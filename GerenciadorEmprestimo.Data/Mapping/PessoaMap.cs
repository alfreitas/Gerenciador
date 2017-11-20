
using FluentNHibernate.Mapping;
using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Data.Mapping
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Table("Pessoa");
            Id(x => x.Codigo)
                .GeneratedBy.Identity().Column("COD_PESSOA");

            Map(x => x.Nome).Column("DC_NOME").Length(50).Not.Nullable();
            Map(x => x.Endereco).Column("DC_ENDERECO").Length(70);
            Map(x => x.Cidade).Column("COD_CIDADE").Length(40);
        }
    }
}
