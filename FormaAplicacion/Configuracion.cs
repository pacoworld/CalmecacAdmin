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
using System.Diagnostics.Eventing.Reader;

namespace FormaAplicacion
{
    public partial class Configuracion : Form
    {
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand com;
        SqlDataReader reader;
        public Configuracion()
        {
            InitializeComponent();
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {
            textBox1.Text = Usuario.ElUsuario;
            textBox1.Enabled = false;
            textBox10.Text = Usuario.CorreoLogin;
            textBox11.Text = Usuario.ClaveLogin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ClaveActual = "";
            string querty;

            cs.Open();
            querty = "select clave from usuarios where Login = '" + Usuario.ElUsuario + "'";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                ClaveActual = reader["clave"].ToString();
            }
            cs.Close();

            if (ClaveActual == textBox2.Text)
            {
                if (textBox3.Text == textBox4.Text)
                {
                    cs.Open();
                    querty = "update Usuarios set Clave = '" + textBox3.Text + "' where Login = '" + Usuario.ElUsuario + "'";
                    com = new SqlCommand(querty, cs);
                    reader = com.ExecuteReader();
                    cs.Close();
                    MessageBox.Show("La clave ha sido actualizada");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else {
                    MessageBox.Show("La Nueva clave no concuerda");
                }
            }
            else
            {
                MessageBox.Show("Clave Actual incorrecta");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string names;
            bool falg = false;
            da = new SqlDataAdapter("SELECT * FROM USUARIOS", cs);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                names = dt.Rows[i]["LOGIN"].ToString();
                if (names != textBox5.Text)
                {
                    falg = true;
                }
                else
                {   MessageBox.Show("El nombre de usuario " + textBox5.Text + " ya se encuetra ocupado");
                    falg = false;
                    break;                    
                }
            }

            if (falg == false)
            {
                // Do nothing
            }
            else
            {                               
                if (textBox8.Text != textBox9.Text)
                {
                    MessageBox.Show("La clave no coincide con la confirmación");
                }
                else
                {
                    da.InsertCommand = new SqlCommand("INSERT INTO USUARIOS VALUES (@LOGIN, @NOMBRE, @APELLIDO, @CLAVE)", cs);
                    da.InsertCommand.Parameters.Add("@LOGIN", SqlDbType.VarChar).Value = textBox5.Text;
                    da.InsertCommand.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = textBox6.Text;
                    da.InsertCommand.Parameters.Add("@APELLIDO", SqlDbType.VarChar).Value = textBox7.Text;
                    da.InsertCommand.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = textBox8.Text;
                    cs.Open();
                    da.InsertCommand.ExecuteNonQuery();
                    cs.Close();
                    MessageBox.Show("El Usuario " + textBox5.Text + "ha sido agregado");
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string querty;
            cs.Open();

            querty = "select login from Correo";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();
            if (reader.Read())

                if (Usuario.CorreoLogin == textBox10.Text)
                {

                }
                else
                {
                    SqlCommand crop1 = cs.CreateCommand();
                    crop1.CommandType = CommandType.Text;
                    crop1.CommandText = "update Correo set login = '" + textBox10.Text + "' where cuenta = 'google'";
                    reader.Close();
                    crop1.ExecuteNonQuery();
                    Usuario.CorreoLogin = textBox10.Text;
                    MessageBox.Show("Login Actualizado", "Correcto", MessageBoxButtons.OK);                    
                }

            querty = "select password from Correo";
            com = new SqlCommand(querty, cs);
            reader.Close();
            reader = com.ExecuteReader();
            if (reader.Read())

                if (Usuario.ClaveLogin == textBox11.Text)
                {

                }
                else
                {
                    SqlCommand crop1 = cs.CreateCommand();
                    crop1.CommandType = CommandType.Text;
                    crop1.CommandText = "update Correo set password = '" + textBox11.Text + "' where cuenta = 'google'";
                    reader.Close();
                    crop1.ExecuteNonQuery();
                    Usuario.ClaveLogin = textBox11.Text;
                    MessageBox.Show("Clave Actualizada", "Correcto", MessageBoxButtons.OK);
                }
            cs.Close();
        }
    }
    
}
