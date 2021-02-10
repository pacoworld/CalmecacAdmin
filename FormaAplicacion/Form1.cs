using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        SqlCommand com;
        SqlDataReader reader;

        private void Form1_Load(object sender, EventArgs e)
        {
            string querty;
            imprime();
            CalculaNumeroDeActivos();

            // Obteniendo el login y password del servidor de correo de la base de datos

            cs.Open();
            querty = "select login from correo";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                Usuario.CorreoLogin = reader["login"].ToString();
            }
            cs.Close();

            cs.Open();
            querty = "select password from correo";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                Usuario.ClaveLogin = reader["password"].ToString();
            }
            cs.Close();

            cs.Open();
            querty = "select host from correo";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                Usuario.MailHost = reader["host"].ToString();
            }
            cs.Close();

            cs.Open();
            querty = "select port from correo";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                Usuario.PortHost = reader["port"].ToString();
            }
            cs.Close();

            cs.Open();
            querty = "select SSL from correo";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                Usuario.SSLHost = reader["SSL"].ToString();
            }
            cs.Close();
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
            da.SelectCommand = new SqlCommand("select ID, Nombre, Apellido, EMail, Sexo, Estatus, MiembroDesde, Membresia, FechaNacimiento from empleados where Estatus = 'Activo'", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[6].DefaultCellStyle.Format = "dd/MMM/yyyy";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "dd/MMM/yyyy";
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
                Reporte rep = new Reporte();
                rep.Show();           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Configuracion cf = new Configuracion();
            cf.Show();

        }
    }
}
