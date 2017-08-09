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
using System.Net.Mail;
using System.Net;

namespace FormaAplicacion
{
    public partial class Control : Form
    {
        DateTime hoy = DateTime.Today;
        string currentYear = DateTime.Now.Year.ToString();
        string Nombrex, Apellidox;
        public Control()
        {
            InitializeComponent();
            this.Text = "Control de Pagos";
        }

        private void Control_Load(object sender, EventArgs e)
        {
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EMPLEADOS WHERE estatus = 'activo'", cs);
            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["ID"]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            SqlCommand com;
            SqlDataReader reader;
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            dataGridView1.Rows.Clear();
            string ElAño = DateTime.Now.ToString("yyyy");

            cs.Open();
            str = "select nombre from empleados where id = '" + comboBox1.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                label3.Text = reader["Nombre"].ToString();
            }
            Nombrex = label3.Text;
            cs.Close();


            cs.Open();
            str = "select apellido from empleados where id = '" + comboBox1.SelectedItem + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                label4.Text = reader["Apellido"].ToString();
            }
            Apellidox = label4.Text;
            cs.Close();

            label8.Text = ElAño;
            ImprimePagosMensuales(comboBox1.SelectedItem.ToString(), ElAño);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool SioNo = VerificaSiYaPago(comboBox1.SelectedItem.ToString(), comboBox2.Text);
            bool LoEnvioSioNO = true;

            if (SioNo == true)
            {
               DialogResult dr = MessageBox.Show("Este usuario ya cuenta con un pago en este mes.  ¿Deseas sumarlo al saldo actual?", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    SqlCommand crop;
                    SqlDataReader reader, reader2;
                    string token, token2, token3, saldo;
                    float saldoInt, SaldoSumado, SaldoAnterior;
                    SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
                    cs.Open();
                    token = "select Abono from pagos where id = '" + comboBox1.SelectedItem + "' and mes = '" + comboBox2.Text + "' ";
                    crop = new SqlCommand(token, cs);
                    reader = crop.ExecuteReader();
                    if (reader.Read())
                    {
                        saldo = reader["Abono"].ToString();
                        Single.TryParse(saldo, out saldoInt);
                        Single.TryParse(textBox1.Text, out SaldoAnterior);
                        SaldoSumado = saldoInt + SaldoAnterior;
                        cs.Close();

                        cs.Open();
                        token2 = "Delete from Pagos where id = '" + comboBox1.SelectedItem + "' and mes = '" + comboBox2.Text + "' ";
                        crop = new SqlCommand(token2, cs);
                        reader = crop.ExecuteReader();
                        cs.Close();

                        cs.Open();
                        token3 = "Insert Into Pagos (ID, Fecha, Abono, Concepto, Mes, Año) VALUES ('" + comboBox1.SelectedItem + "' , '" + hoy.ToString() + "' , '" + SaldoSumado.ToString() + "' , '" + ' ' + "', '" + comboBox2.Text + "', '" + currentYear + "' )";
                        crop = new SqlCommand(token3, cs);
                        reader2 = crop.ExecuteReader();
                        cs.Close();        
                        }
                    }
                } else {

                    SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    DateTime hoy = DateTime.Today;
                    string temp;
                    da.InsertCommand = new SqlCommand("INSERT INTO Pagos VALUES (@ID, @Fecha, @Abono, @Concepto, @Mes, @Año)", cs);
                    da.InsertCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                    da.InsertCommand.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = hoy.ToString();
                    da.InsertCommand.Parameters.Add("@Abono", SqlDbType.VarChar).Value = textBox1.Text;
                    da.InsertCommand.Parameters.Add("@Mes", SqlDbType.VarChar).Value = comboBox2.Text;
                    da.InsertCommand.Parameters.Add("@Año", SqlDbType.VarChar).Value = currentYear;
                    da.InsertCommand.Parameters.Add("@Concepto", SqlDbType.VarChar).Value = textBox2.Text;
                    temp = textBox1.Text;                   
                    cs.Open();

                    if (checkBox1.Checked == false)
                    {
                        if (string.IsNullOrWhiteSpace(textBox1.Text))
                        {
                            MessageBox.Show("Introduce el monto a pagar", "Error");
                            LoEnvioSioNO = false;
                            cs.Close();
                        }
                        else
                        {
                            if (comboBox2.SelectedItem == null)
                            {
                                MessageBox.Show("Selecciona el mes a pagar");
                                LoEnvioSioNO = false;
                                cs.Close();
                            }
                            else
                            {
                                try
                                {
                                    da.InsertCommand.ExecuteNonQuery();
                                    MessageBox.Show("Pago registrado.", "Correcto");
                                }
                                catch
                                {
                                    MessageBox.Show("Verifica el monto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    LoEnvioSioNO = false;
                            }

                                textBox1.Clear();
                                textBox2.Clear();
                                cs.Close();
                            }
                        }
                    }
                    else
                    {
                    LoEnvioSioNO = false;
                    if (string.IsNullOrWhiteSpace(textBox2.Text))
                        {
                            MessageBox.Show("Introduce el concepto a pagar");
                            cs.Close();
                        }
                        else
                        {
                            try
                            {
                                da.InsertCommand.ExecuteNonQuery();
                                MessageBox.Show("Pago registrado", "Pago");
                            }
                            catch
                            {
                                MessageBox.Show("Verifica el monto");
                            }
                            textBox1.Clear();
                            textBox2.Clear();
                            cs.Close();
                        }
                    }

                    ImprimePagosMensuales(comboBox1.SelectedItem.ToString(), currentYear);

                if (LoEnvioSioNO == true) {
                 //   EnviaEMailTicket(comboBox1.SelectedItem.ToString(), comboBox2.Text, currentYear, temp);
                    MessageBox.Show("Correo Electronico Enviado");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.Enabled = true;
                comboBox2.Enabled = false;
            }

            else
            {
                textBox2.Enabled = false;
                comboBox2.Enabled = true;
            }
        }

        private void ImprimePagosMensuales(string identif, string Año) {

            string str;
            SqlCommand com;
            SqlDataReader reader;
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            label8.Text = Año;

            String[] QueMes = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            for (int i = 0; i < 12; i++)
            {
                cs.Open();
                str = "Select mes from Pagos where Mes = '" + QueMes[i] + "' and ID = '" + identif + "' and Año = '"+ Año +"'";
                com = new SqlCommand(str, cs);
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    dataGridView1.Rows[0].Cells[i].Value = "Pagado";
                }
                cs.Close();
            }
            label8.Text = Año;
        }

        private void EnviaEMailTicket(string IDPago, string MesPago, string AñoPago, string MontoPago) {
            string str, EMailPago;
            SqlCommand com;
            SqlDataReader reader;
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            cs.Open();
            str = "select email from empleados where id = '" + IDPago + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    EMailPago = reader["EMail"].ToString();

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("pacoworld@gmail.com");
                    message.Subject = "Calmecac Gym - Recibo de Pago de " + MesPago + " del " + AñoPago + " ";
                    message.Body = "Comprobante de pago: \n\nNombre= " + Nombrex + " " + Apellidox + "\nMes: " + MesPago + "\nAño: " + AñoPago + "\nMonto: $" + MontoPago + "\nCorreo: " + EMailPago + "\n\nCalmecac Gym agradece tu preferencia\n Este Pago no exime adeudos anteriores";
                    message.To.Add(EMailPago);
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("pacoworld@gmail.com", "Taladega82");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            catch {
                MessageBox.Show("Error al mandar comprobante de pago \nNo hay direccion de correo electrónico de este usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ImprimePagosMensuales(comboBox1.SelectedItem.ToString(), comboBox3.SelectedItem.ToString());
        }

        private bool VerificaSiYaPago(string ElID, string ElMes) {
            bool tiene;
            string str;
            SqlCommand com;
            SqlDataReader reader;
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            cs.Open();
            str = "Select Abono from Pagos where id = '" + ElID + "' and Mes = '" + ElMes + "' ";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (string.IsNullOrEmpty(ElMes))
            {
                tiene = false;
            }else{ 

                if (reader.Read())
                {
                    tiene = true;
                }
                else {
                    tiene = false;
                }
            }
            cs.Close();
            return tiene;
        }




    }
}
