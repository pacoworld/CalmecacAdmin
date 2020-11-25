using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormaAplicacion
{
    class Usuario
    {
        private static string UsuarioAdmin = "", CorreoLoginAdmin = "", ClaveLoginAdmin = "";


        public static string ElUsuario 
        {
            get { return UsuarioAdmin; }
            set { UsuarioAdmin = value;  }
        }

        public static string CorreoLogin
        {
            get { return CorreoLoginAdmin; }
            set { CorreoLoginAdmin = value; }
        }

        public static string ClaveLogin
        {
            get { return ClaveLoginAdmin; }
            set { ClaveLoginAdmin = value; }
        }

    }
}
