using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.ResolvedorDependencia
{
    internal static class EntidadeResolver
    {
        public const string ASSEMBLY = "GerenciadorEmprestimo.Entidades";
        public static void Register(ConfigurationExpression registry)
        {
            registry.Scan(x =>
            {
                x.Assembly(ASSEMBLY);
                x.WithDefaultConventions();
            });
        }

    }
}
