
using FluentNHibernate.Mapping;
using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Data.Mapping
{
    public class JogoMap : ClassMap<Jogo>
    {
        public JogoMap()
        {

            Table("Jogo");
            Id(x => x.Codigo)
               .GeneratedBy.Identity().Column("COD_JOGO");

            Map(x => x.Titulo).Column("DC_TITULO").Length(50).Not.Nullable();
            Map(x => x.Genero).Column("TP_GENERO").Length(30);
            Map(x => x.Data).Column("DT_EMPRESTIMO");
            References(d => d.Locatario).Column("COD_LOCATARIO");
        }
    }
}
