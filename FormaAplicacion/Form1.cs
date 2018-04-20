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

namespace FormaAplicacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Control Gym CALMECAC ver 1.0";
        }

        DataSet ds = new DataSet();
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlDataAdapter da = new SqlDataAdapter();

        private void Form1_Load(object sender, EventArgs e)
        {
            imprime();
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            da.SelectCommand = new SqlCommand("SELECT * FROM Empleados where Estatus = 'Activo'", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void ImprimeTodos()
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Empleados", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reporte rep = new Reporte();
            rep.Show();
        }
    }
}
