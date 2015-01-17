using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerWPF.Entidades;

namespace Servicios
{
    [Export]
    public class UsuarioService
    {
        public UsuarioService()
        {
        }

        public Usuario LoginUsuario(string usuario, string contraseña)
        {
            var us = new Usuario()
            {
                Nombre = "Haziel Isaí Guillén López",
                CuentaUsuario = "hazi",
                Contraseña = "hazi",
                Rol = "Root"
            };
            return us;
        }
    }
}
