using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorEmprestimo.ResolvedorDependencia
{
    public static class StructureMapConfig
    {


        private static IContainer Init()
        {
            return new Container(c =>
            {
                c.AddRegistry<GerenciadorRegistry>();
                c.AddRegistry<NHibernateRegistry>();
            });
        }

        public static IContainer InitMvc()
        {
            var container = Init();

            return container;
        }

        public static T Resolve<T>()
        {

            return Init().GetInstance<T>();
        }
    }
}
