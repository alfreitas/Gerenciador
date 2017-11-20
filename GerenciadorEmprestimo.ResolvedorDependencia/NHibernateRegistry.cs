using FluentNHibernate.Cfg;
using GerenciadorEmprestimo.Data;
using GerenciadorEmprestimo.Entidades;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.ResolvedorDependencia
{
    public class NHibernateRegistry : Registry
    {
        public const string ASSEMBLY = "GerenciadorEmprestimo.Data";
        public NHibernateRegistry()
        {
            var nhConfig = Fluently.Configure()
                .Mappings(map => map.FluentMappings
                    .AddFromAssembly(Assembly.Load(ASSEMBLY)))//.ExposeConfiguration(d=> new SchemaExport(d).Execute(true,true,false))
                .BuildConfiguration();

            For<Configuration>().Use(nhConfig).Singleton();
            For<ISessionFactory>().Use(ctx => ctx.GetInstance<Configuration>().BuildSessionFactory()).Singleton();
            For<ISession>().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
            For<IStatelessSession>().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenStatelessSession());
        }
    }
}
