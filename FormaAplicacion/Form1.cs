using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FormaAplicacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Control Gym CALMECAC ver 1.4";
        }

        DataSet ds = new DataSet();
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlDataAdapter da = new SqlDataAdapter();
        //Password ps = new Password();

        private void Form1_Load(object sender, EventArgs e)
        {
            imprime();
            CalculaNumeroDeActivos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registro reg = new Registro();
            reg.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ImprimeTodos();
            }
            else {
                imprime();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Modificar mod = new Modificar();
            mod.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Control con = new Control();
            con.Show();
        }

        private void imprime() {
            da.SelectCommand = new SqlCommand("select ID, Nombre, Apellido, EMail, Sexo, Estatus, Telefono, MiembroDesde, Membresia, FechaNacimiento from empleados where Estatus = 'Activo'", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MMM/yyyy";
            dataGridView1.Columns[9].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void ImprimeTodos()
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Empleados", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void CalculaNumeroDeActivos() 
        {
            SqlCommand com1;
            SqlDataReader reader1;
            cs.Open();
            string QuertyTotal2;
            QuertyTotal2 = "select COUNT (*) from (select * from empleados where estatus = 'activo') myNewTable";
            com1 = new SqlCommand(QuertyTotal2, cs);
            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                object temp1 = reader1[0];
                label2.Text = temp1.ToString();
            }
            cs.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //ps.Show();           
            //bool acces = ps.authorization;
            //if (acces == true)
            //{
                Reporte rep = new Reporte();
                rep.Show();
            //}
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.Show();
        }
    }
}
