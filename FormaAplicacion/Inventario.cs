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
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
        }

        DateTime hoy = DateTime.Today;

            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataReader reader;
            SqlCommand com;

        private void button1_Click(object sender, EventArgs e)
        {            

           
            da.InsertCommand = new SqlCommand("INSERT INTO Inventario VALUES (@Nombre, @Fabricante, @CantidadTotal, @Descripcion, @Area, @Precio, @Fecha)", cs);
            da.InsertCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add("@Fabricante", SqlDbType.VarChar).Value = textBox3.Text;
            da.InsertCommand.Parameters.Add("@CantidadTotal", SqlDbType.VarChar).Value = numericUpDown1.Value;
            da.InsertCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = textBox2.Text;
            da.InsertCommand.Parameters.Add("@Area", SqlDbType.VarChar).Value = textBox4.Text;
            da.InsertCommand.Parameters.Add("@Precio", SqlDbType.VarChar).Value = textBox5.Text;
            da.InsertCommand.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = hoy.ToString();
            cs.Open();
            da.InsertCommand.ExecuteNonQuery();
            MessageBox.Show("Articulo dado de alta");
            ClearForm();            
            cs.Close();
        }

        private void ClearForm() 
        { 
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            numericUpDown1.Value = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Inventario where Area = '" + comboBox1.SelectedItem + "'", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] tb;
            tb = new TextBox[10];
            tb[6] = textBox6;
            tb[7] = textBox7;
            tb[8] = textBox8;
            string clave = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string[] columna = { "Nombre", "Fabricante", "CantidadTotal" };

            for (int i = 6; i <= 8; i++)
            {
                cs.Open();
                string str = "select " + columna[i - 6] + " from Inventario where clave = '" + clave + "'";
                com = new SqlCommand(str, cs);
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    tb[i].Text = reader[columna[i - 6]].ToString();
                }
                cs.Close();
            }
        }
    }
}
