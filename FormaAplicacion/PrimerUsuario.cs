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
    public partial class PrimerUsuario : Form
    {
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlDataAdapter da = new SqlDataAdapter();

        public PrimerUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("La clave no coincide con la confirmación");
            }
            else
            {
                da.InsertCommand = new SqlCommand("INSERT INTO USUARIOS VALUES (@LOGIN, @NOMBRE, @APELLIDO, @CLAVE)", cs);
                da.InsertCommand.Parameters.Add("@LOGIN", SqlDbType.VarChar).Value = textBox1.Text;
                da.InsertCommand.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = textBox2.Text;
                da.InsertCommand.Parameters.Add("@APELLIDO", SqlDbType.VarChar).Value = textBox3.Text;
                da.InsertCommand.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = textBox4.Text;
                cs.Open();
                da.InsertCommand.ExecuteNonQuery();
                cs.Close();
                MessageBox.Show("El Usuario " + textBox1.Text + " ha sido agregado");
                this.Hide();
                Password pas = new Password();
                pas.Show();
            }
        }
    }
}
