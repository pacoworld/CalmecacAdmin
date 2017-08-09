using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPaco1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hola mundo");
            TestPacoDataDataContext tmp = new TestPacoDataDataContext();
            Empleado[] MisEmpleados = tmp.Empleados.ToArray();

            for (int i = 0; i < MisEmpleados.Length; i++) {

                Console.WriteLine(MisEmpleados[i].Nombre.Trim());

            }

            Console.ReadLine();
        }
    }
}
