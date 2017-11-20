using FluentNHibernate.Mapping;
using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Data.Mapping
{
    public class HistoricoEmprestimoMap : ClassMap<HistoricoEmprestimo>
    {
        public HistoricoEmprestimoMap()
        {

            Table("Historico");
            Id(x => x.Codigo)
               .GeneratedBy.Identity().Column("COD_HIST");
            Map(x => x.DataInicio).Column("DT_INI");
            Map(x => x.DataFim).Column("DT_FIM");
            References(d => d.Pessoa).Column("COD_LOCATARIO");
            References(d => d.Jogo).Column("COD_JOGO");
        }
    }
}
