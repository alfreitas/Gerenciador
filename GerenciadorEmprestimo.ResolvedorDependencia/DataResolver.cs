using FluentNHibernate.Cfg;
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
    internal static class DataResolver
    {
        public const string ASSEMBLY = "GerenciadorEmprestimo.Data";
        public static void Register(ConfigurationExpression registry)
        {
            registry.Scan(x =>
            {
                x.Assembly(ASSEMBLY);
                x.WithDefaultConventions();
            });

            var nhConfig = Fluently.Configure()
                .Mappings(map => map.FluentMappings
                    .AddFromAssembly(Assembly.Load(ASSEMBLY)))
                .BuildConfiguration();

            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(Assembly.Load(ASSEMBLY));
            new SchemaExport(cfg).Execute(true, true, false);

            registry.For<Configuration>().Use(nhConfig).Singleton();
            registry.For<ISessionFactory>().Use(nhConfig.BuildSessionFactory()).Singleton();
            registry.For<ISession>().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
            registry.For<IStatelessSession>().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenStatelessSession());
        }
    }
}
