using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormaAplicacion
{
    class Usuario
    {
        private static string UsuarioAdmin = "", CorreoLoginAdmin = "", ClaveLoginAdmin = "", MailHostAdmin = "", MailPortAdmin = "", MailSSLAdmin = "", NombreComputadora = "", NombreBasedeDatos = "";
        


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

        public static string MailHost
        {
            get { return MailHostAdmin; }
            set { MailHostAdmin = value; }
        }

        public static string PortHost
        {
            get { return MailPortAdmin; }
            set { MailPortAdmin = value; }
        }

        public static string SSLHost
        {
            get { return MailSSLAdmin; }
            set { MailSSLAdmin = value; }
        }

        public static string Computadora
        {
            get { return NombreComputadora; }
            set { NombreComputadora = value; }
        }

        public static string BasedeDatos
        {
            get { return NombreBasedeDatos; }
            set { NombreBasedeDatos = value; }
        }


    }
}
