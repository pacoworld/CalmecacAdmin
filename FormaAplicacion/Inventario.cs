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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlDataAdapter da = new SqlDataAdapter();

           
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
            cs.Close();
        }
    }
}
