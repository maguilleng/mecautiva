using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace TallerWPF.VentasModule
{
    [ModuleExport(typeof(ModuloVentas))]
    public class ModuloVentas : IModule
    {
        public void Initialize() { }

    }
}
