using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FormaAplicacion
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main()
        {
            SqlConnection cs = new SqlConnection("Data Source = LAPTOP-G3MFU6OV; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlCommand com1;
            SqlDataReader reader1;
            object temp1;
            int temp = 0;

            cs.Open();
            string QuertyTotal = "select count (*) from usuarios ";
            com1 = new SqlCommand(QuertyTotal, cs);
            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                temp1 = reader1[0];
                temp = Convert.ToInt32(temp1);
            }
            cs.Close();

            if (temp == 0)
            {
                MessageBox.Show("No hay usuarios, tienes que registrar uno");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PrimerUsuario());

            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Password());
            }
            
        }
    }
}
