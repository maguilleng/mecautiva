using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace TallerWPF.ClientesModule
{
    [ModuleExport(typeof(ModuloClientes))]
    public class ModuloClientes : IModule
    {
        public void Initialize() { }
    }
}
