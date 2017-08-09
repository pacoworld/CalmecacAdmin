﻿using System;
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
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlDataAdapter da = new SqlDataAdapter();

            da.InsertCommand = new SqlCommand("INSERT INTO Empleados VALUES (@Nombre, @Apellido, @EMail, @Sexo, @Estatus, @Telefono)", cs);
            da.InsertCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = textBox2.Text;
            da.InsertCommand.Parameters.Add("@EMail", SqlDbType.VarChar).Value = textBox3.Text;         
            if (comboBox1.Text == "Masculino" ){
                da.InsertCommand.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = "Masculino";
                 }else{
                da.InsertCommand.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = "Femenino";
                 }
            cs.Open();
            da.InsertCommand.Parameters.Add("@Estatus", SqlDbType.VarChar).Value = "Activo";
            da.InsertCommand.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = textBox4.Text; //este
            da.InsertCommand.ExecuteNonQuery();
            MessageBox.Show("Nuevo usuario dado de alta");
            cs.Close();
        }

        
    }
}
