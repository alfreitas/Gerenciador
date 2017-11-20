using GerenciadorEmprestimo.Business;
using GerenciadorEmprestimo.Data;
using GerenciadorEmprestimo.Data.Interface;
using GerenciadorEmprestimo.Entidades;
using StructureMap;

namespace GerenciadorEmprestimo.ResolvedorDependencia
{
    class GerenciadorRegistry : Registry
    {
        public GerenciadorRegistry()
        {
            Scan(scan =>
            {
                scan.Assembly("GerenciadorEmprestimo.Business");
                scan.Assembly("GerenciadorEmprestimo.Data.Interface");
                scan.Assembly("GerenciadorEmprestimo.Data");
                scan.Assembly("GerenciadorEmprestimo.Entidades");
                scan.WithDefaultConventions();
            });
            For(typeof(IBaseData<>)).Use(typeof(BaseData<>));
        }
    }
}
