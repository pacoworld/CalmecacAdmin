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
    }
    
}
