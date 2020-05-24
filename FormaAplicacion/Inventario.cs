using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            string clave = "";
            string[] columna = { "Nombre", "Fabricante", "CantidadTotal", "Descripcion", "Precio" };

        private void button1_Click(object sender, EventArgs e)
        {            
           
            da.InsertCommand = new SqlCommand("INSERT INTO Inventario VALUES (@Nombre, @Fabricante, @CantidadTotal, @Descripcion, @Area, @Precio, @Fecha)", cs);
            da.InsertCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add("@Fabricante", SqlDbType.VarChar).Value = textBox3.Text;
            da.InsertCommand.Parameters.Add("@CantidadTotal", SqlDbType.VarChar).Value = numericUpDown1.Value;
            da.InsertCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = textBox2.Text;
            da.InsertCommand.Parameters.Add("@Area", SqlDbType.VarChar).Value = comboBox2.Text;
            da.InsertCommand.Parameters.Add("@Precio", SqlDbType.VarChar).Value = textBox4.Text;
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
            numericUpDown1.Value = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Inventario where Area = '" + comboBox1.SelectedItem + "'", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[6].DefaultCellStyle.Format = "C";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] tb;
            tb = new TextBox[12];
            tb[5] = textBox5;
            tb[6] = textBox6;
            tb[7] = textBox7;
            tb[8] = textBox8;
            tb[9] = textBox9;
          //  tb[10] = textBox10;
            clave = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string str;
           

            for (int i = 5; i <= 9 ; i++)
            {
                cs.Open();
                str = "select " + columna[i - 5] + " from Inventario where clave = '" + clave + "'";
                com = new SqlCommand(str, cs);
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    tb[i].Text = reader[columna[i - 5]].ToString();
                }
                cs.Close();
            }

                        
                cs.Open();
                str = "select area from Inventario where clave = '" + clave + "'";
                com = new SqlCommand(str, cs);
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    comboBox3.Text = reader["area"].ToString();
                }
                cs.Close();
            

            cs.Open();
            str = "select fecha from Inventario where clave = '" + clave + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                String TheDate = reader["Fecha"].ToString();
                DateTime dt = Convert.ToDateTime(TheDate);
                TheDate = dt.ToString("dd/MMM/yyyy");                
                dateTimePicker1.CustomFormat = "d, MMM, yyyy";
                dateTimePicker1.Value = dt;
            }
            cs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string token;
            SqlCommand crop;
            cs.Open();
            TextBox[] tb;
            tb = new TextBox[12];
            tb[5] = textBox5;
            tb[6] = textBox6;
            tb[7] = textBox7;
            tb[8] = textBox8;
            tb[9] = textBox9;
            //    tb[10] = textBox10;

            for (int i = 5; i <= 9; i++)
            {

                token = "select " + columna[i - 5] + " from Inventario where clave = " + clave + " ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())

                    if (reader[columna[i - 5]].ToString() == tb[i].Text)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Inventario set " + columna[i - 5] + " =  '" + tb[i].Text + " ' where clave = '" + clave + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show(" " + columna[i - 5] + " Actualizado", "Correcto", MessageBoxButtons.OK);
                    }
            }

            token = "select area from Inventario where clave = " + clave + " ";
            crop = new SqlCommand(token, cs);
            reader.Close();
            reader = crop.ExecuteReader();
            if (reader.Read())

                if (reader["area"].ToString() == comboBox3.Text)
                {

                }
                else
                {
                    SqlCommand crop1 = cs.CreateCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    crop1.CommandType = CommandType.Text;
                    crop1.CommandText = "update Inventario set area = '" + comboBox3.Text + " ' where clave = '" + clave + " '";
                    reader.Close();
                    crop1.ExecuteNonQuery();
                    MessageBox.Show(" Area Actualizado", "Correcto", MessageBoxButtons.OK);
                }




            token = "select fecha from Inventario where clave = " + clave + " ";
                crop = new SqlCommand(token, cs);
                string ddt = dateTimePicker1.Value.ToString();
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())

                    if (reader["Fecha"].ToString() == ddt)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Inventario set fecha = '" + ddt + "' where clave = '" + clave + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Fecha de compra actualizada", "Correcto", MessageBoxButtons.OK);
                    }
   //         }
            cs.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
    }
}
