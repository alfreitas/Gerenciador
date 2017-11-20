using FluentNHibernate.Mapping;
using GerenciadorEmprestimo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.Data.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuario");
            Id(x => x.Codigo)
               .GeneratedBy.Identity().Column("COD_JOGO");

            Map(x => x.Login).Column("DC_LOGIN").Length(50).Not.Nullable();
            Map(x => x.Senha).Column("DC_SENHA").Length(30).Not.Nullable();
            Map(x => x.IsAdmin).Column("IS_ADM");
            References(d => d.Pessoa).Column("COD_PESSOA");
        }
    }
}
