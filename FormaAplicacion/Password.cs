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
        string clave = "123";
        bool SioNo = false;

        public Password()
        {
            InitializeComponent();
        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == clave)
            {
                Form1 inicio = new Form1();
                this.Hide();
                inicio.Show();
                SioNo = true;
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
            //comboBox1.SelectedIndex = 0;

            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["LOGIN"]);
            }
        }
    }
}
