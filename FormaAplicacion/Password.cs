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
    public partial class Password : Form
    {
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlCommand com;
        SqlDataReader reader;
        string clavefromDB = "";
        public string ElUsuario;
        public Password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cs.Open();
            string querty = "select clave from usuarios where Login = '" + comboBox1.SelectedItem + "'";
            com = new SqlCommand(querty, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                clavefromDB = reader["clave"].ToString();
            }
            cs.Close();


            if (textBox1.Text == clavefromDB)
            {

                Usuario.ElUsuario = comboBox1.Text;
                Form1 inicio = new Form1();                
                this.Hide();
                inicio.Show();
            }
            else
            {
                MessageBox.Show("Clave incorrecta");
            }
        }

        private void Password_Load(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("SELECT * FROM USUARIOS", cs);
            DataTable dt = new DataTable();

            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["LOGIN"]);
            }
        }

       
    }
}
