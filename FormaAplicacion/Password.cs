using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormaAplicacion
{
    public partial class Password : Form
    {
        string clave = "1234";
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

        public bool Permiso() 
        {            
            return SioNo;
        }

        
    }
}
