using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace TallerWPF.InventariosModule
{
    [ModuleExport(typeof(ModuloInventarios))]
    public class ModuloInventarios : IModule
    {
        public void Initialize() { }
    }
}
