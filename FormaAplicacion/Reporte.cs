using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace FormaAplicacion
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlDataAdapter da = new SqlDataAdapter();
        int NumPgad = 0;

        private void Reporte_Load(object sender, EventArgs e)
        {
            var a = DateTime.Now.ToString("MMMM");
            var f = "MMMM";
            var dt = DateTime.ParseExact(a, f, new CultureInfo("en-US"));
            var result = dt.ToString(f, new CultureInfo("es-ES"));
            string ElMes = result.ToString();
            string ElAño = DateTime.Now.ToString("yyyy");
            

            label2.Text = ElMes;
            label4.Text = ElAño;            
            imprime(ElMes, ElAño);
            CalculaTotalMes(ElMes, ElAño);
            
        }

        private void imprime(string ElMesImprime, string ElAñoImprime)
        {           
            da.SelectCommand = new SqlCommand("select Empleados.ID, Empleados.Nombre, Empleados.Apellido, Pagos.Abono, Pagos.Fecha, Pagos.Folio from Empleados Inner join Pagos on Empleados.ID = pagos.ID where Pagos.Mes='" + ElMesImprime + "' and Pagos.Año='" + ElAñoImprime + "';", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[3].DefaultCellStyle.Format = "C";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void CalculaTotalMes(string Mes, string año) {
            SqlCommand com, com1;
            SqlDataReader reader, reader1;
            cs.Open();
            string QuertyTotal;
            QuertyTotal = "select sum (Abono) from pagos where Mes='" + Mes + "' and Año='" + año + "'";
            com = new SqlCommand(QuertyTotal, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                    object temp1 = reader[0];
                
                try
                {
                    int temp2 = Convert.ToInt32(temp1);
                    label5.Text = temp2.ToString("C");
                }
                catch {
                    label5.Text = "0";
                      }
            }
            cs.Close();


            cs.Open();   
            string QuertyTotal2;
            QuertyTotal2 = "select COUNT (*) from (select Empleados.ID, Empleados.Nombre, Empleados.Apellido, Pagos.Abono, Pagos.Fecha from Empleados Inner join Pagos on Empleados.ID = pagos.ID where Pagos.Mes= '" + Mes + "' and Pagos.Año='" + año + "' ) myNewTable";
            com1 = new SqlCommand(QuertyTotal2, cs);
            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                object temp1 = reader1[0];                
                label8.Text = temp1.ToString();
                Int32.TryParse(temp1.ToString(), out NumPgad);               


            }
            cs.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imprime(comboBox1.Text, comboBox2.Text);
            CalculaTotalMes(comboBox1.Text, comboBox2.Text);
            label2.Text = comboBox1.Text;
            label4.Text = comboBox2.Text;
        }
        
    }
}
