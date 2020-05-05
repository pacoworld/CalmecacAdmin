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
    public partial class Modificar : Form
    {
        public Modificar()
        {

            InitializeComponent();

        }

          private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand crop;
            SqlDataReader reader;
            string token;
            string[] campos = { };
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            cs.Open();


                    token = "select nombre from empleados where id = '" + comboBox2.SelectedItem + "' ";
                    crop = new SqlCommand(token, cs);
                    reader = crop.ExecuteReader();
                    if (reader.Read()) {
                   
                    if (reader["Nombre"].ToString() == textBox1.Text)
                    {
               
                    } else {                                   
                    SqlCommand crop1 = cs.CreateCommand(); 
                    SqlDataAdapter da = new SqlDataAdapter();
                    crop1.CommandType = CommandType.Text;
                    crop1.CommandText = "update Empleados set Nombre =  '"+textBox1.Text + " ' where id = '" + comboBox2.SelectedItem + " '";
                    reader.Close(); 
                    crop1.ExecuteNonQuery();
                    MessageBox.Show("Nombre Actualizado", "Correcto", MessageBoxButtons.OK);
                    }

                token = "select Apellido from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["Apellido"].ToString() == textBox2.Text)
                    {
                        
                    }
                    else
                    {                       
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set Apellido =  '" + textBox2.Text + " ' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close(); //this one
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Apellido Actualizado" , "Correcto", MessageBoxButtons.OK);
                    }
                }



                token = "select EMail from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["Email"].ToString() == textBox3.Text)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set EMail =  '" + textBox3.Text + " ' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close(); 
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Correo Electrónico Actualizado", "Correcto", MessageBoxButtons.OK);
                    }
                }


                token = "select Sexo from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["Sexo"].ToString() == comboBox4.Text)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set Sexo =  '" + comboBox4.Text + " ' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Sexo Actualizado" , "Correcto", MessageBoxButtons.OK);
                    }
                }


                token = "select telefono from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["telefono"].ToString() == textBox5.Text)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set Telefono =  '" + textBox5.Text + " ' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Telefono Actualizado" , "Correcto", MessageBoxButtons.OK);
                    }
                }



                token = "select Estatus from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["Estatus"].ToString() == comboBox1.Text)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set Estatus =  '"+ comboBox1.Text +"' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Estatus Actualizado", "Correcto", MessageBoxButtons.OK);
                    }
                }


                token = "select Membresia from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                reader.Close();
                reader = crop.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["Membresia"].ToString() == comboBox3.Text)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set Membresia =  '" + comboBox3.Text + "' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Membresia Actualizado", "Correcto", MessageBoxButtons.OK);
                    }
                }

                token = "select FechaNacimiento from empleados where id = '" + comboBox2.SelectedItem + "' ";
                crop = new SqlCommand(token, cs);
                string ddt = dateTimePicker1.Value.ToString();
                reader.Close();
                reader = crop.ExecuteReader();               
                if (reader.Read())
                {
                    
                    if (reader["FechaNacimiento"].ToString() == ddt)
                    {

                    }
                    else
                    {
                        SqlCommand crop1 = cs.CreateCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        crop1.CommandType = CommandType.Text;
                        crop1.CommandText = "update Empleados set FechaNacimiento =  '" + ddt + " ' where id = '" + comboBox2.SelectedItem + " '";
                        reader.Close();
                        crop1.ExecuteNonQuery();
                        MessageBox.Show("Fecha de Nacimiento Actualizada", "Correcto", MessageBoxButtons.OK);
                    }
                }
            }            
          cs.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            SqlCommand com;
            SqlDataReader reader; 
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");

            cs.Open();
            str = "select nombre from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();         

            if (reader.Read()) {
                textBox1.Text = reader["Nombre"].ToString();
            }
            cs.Close();


            cs.Open();
            str = "select apellido from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                textBox2.Text = reader["Apellido"].ToString();
            }
            cs.Close();


            cs.Open();
            str = "select email from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                textBox3.Text = reader["EMail"].ToString();
            }
            cs.Close();


            cs.Open();
            str = "select Sexo from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                comboBox4.Text = reader["Sexo"].ToString();
            }
            cs.Close();


            cs.Open();
            str = "select telefono from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                textBox5.Text = reader["Telefono"].ToString();
            }
            cs.Close();


            cs.Open();
            str = "select Estatus from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();
            
            if (reader.Read())
            {
                comboBox1.Text = reader["Estatus"].ToString();
            }
            cs.Close();


            cs.Open();
            str = "select Membresia from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
             comboBox3.Text = reader["Membresia"].ToString();
            }
            cs.Close();
           


            cs.Open();
            str = "select MiembroDesde from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                label9.Text = reader["MiembroDesde"].ToString();          
            }
            cs.Close();



            cs.Open();
            str = "select FechaNacimiento from empleados where id = '" + comboBox2.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                string adf = reader ["FechaNacimiento"].ToString();
                DateTime dt = Convert.ToDateTime(adf);
                dateTimePicker1.CustomFormat = "d, MMM, yyyy";
                dateTimePicker1.Value = dt;
            }
            cs.Close();
        }

        private void Modificar_Load(object sender, EventArgs e)
        {
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EMPLEADOS", cs);
            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt.Rows[i]["ID"]);
            }
        }

        
    }
}
