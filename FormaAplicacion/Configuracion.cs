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
        public Configuracion()
        {
            InitializeComponent();
        }

        private void Configuracion_Load(object sender, EventArgs e)
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
