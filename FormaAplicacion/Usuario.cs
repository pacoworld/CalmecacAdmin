using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormaAplicacion
{
    class Usuario
    {
        private static string UsuarioAdmin = "";

        public static string ElUsuario 
        {
            get { return UsuarioAdmin; }
            set { UsuarioAdmin = value;  }
        }
    }
}
