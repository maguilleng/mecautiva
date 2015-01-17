using System.Windows;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Telerik.Windows.Controls;

namespace TallerWPF
{
    /// <summary>
    /// Lógica de interacción para Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : RadRibbonWindow
    {
        static Shell()
        {
            RadRibbonWindow.IsWindowsThemeEnabled = false;
        }
        
        public Shell()
        {
            InitializeComponent();
        }

        [Import]
        [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        ShellViewModel ViewModel
        {
            set 
            {
                this.DataContext = value;
            }
        }
    }
}
