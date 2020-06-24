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
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace FormaAplicacion
{
    public partial class Control : Form
    {
        DateTime hoy = DateTime.Today;
        DateTime FechaRecordatorio = DateTime.MinValue;
        string currentYear = DateTime.Now.Year.ToString();
        string LastYear = (DateTime.Now.Year - 1).ToString();
        string Nombrex, Apellidox;
        string clave = "", NombreCorreo = "", ApellidoCorreo = "";

        DataSet ds = new DataSet();
//        SqlConnection cs1 = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand com;
        SqlDataReader reader;


        public Control()
        {
            InitializeComponent();
            this.Text = "Control de los Pagos";
        }

        private void Control_Load(object sender, EventArgs e)
        {
            /*SqlDataAdapter*/
            da = new SqlDataAdapter("SELECT * FROM EMPLEADOS WHERE estatus = 'activo'", cs);
            DataTable dt = new DataTable();
            comboBox4.SelectedIndex = 3;

            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["ID"]);
            }

            da.SelectCommand = new SqlCommand("select Pagos.ID, Empleados.Nombre, Empleados.Apellido, MAX(pagos.Fecha) AS Ultimo_Pago from Empleados Inner join Pagos on pagos.ID = Empleados.ID where Empleados.Estatus = 'Activo' group by Pagos.id, Empleados.Nombre, Empleados.Apellido Order by Pagos.id", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
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
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un usuario");
            }

            else
            {
                bool SioNo = VerificaSiYaPago(comboBox1.SelectedItem.ToString(), comboBox2.Text, currentYear);
                bool LoEnvioSioNO = true;

                if (SioNo == true)
                {
                    DialogResult dr = MessageBox.Show("Este usuario ya cuenta con un pago en este mes.  ¿Deseas sumarlo al saldo actual?", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        SqlCommand crop;
                        SqlDataReader reader;
                        string token, saldo;
                        float saldoInt, SaldoSumado, SaldoAnterior;
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
                            token = "Delete from Pagos where id = '" + comboBox1.SelectedItem + "' and mes = '" + comboBox2.Text + "' ";
                            crop = new SqlCommand(token, cs);
                            reader = crop.ExecuteReader();
                            cs.Close();

                            cs.Open();
                            token = "Insert Into Pagos (ID, Fecha, Abono, Concepto, Mes, Año) VALUES ('" + comboBox1.SelectedItem + "' , '" + hoy.ToString() + "' , '" + SaldoSumado.ToString() + "' , '" + ' ' + "', '" + comboBox2.Text + "', '" + currentYear + "' )";
                            crop = new SqlCommand(token, cs);
                            reader = crop.ExecuteReader();
                            cs.Close();
                        }
                        ImprimePagosMensuales(comboBox1.SelectedItem.ToString(), currentYear);
                    }
                }
                else
                {

                    SqlDataAdapter da = new SqlDataAdapter();
                    DateTime hoy = DateTime.Today;
                    string tempMonto;
                    da.InsertCommand = new SqlCommand("INSERT INTO Pagos VALUES (@ID, @Fecha, @Abono, @Concepto, @Mes, @Año)", cs);
                    da.InsertCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                    da.InsertCommand.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = hoy.ToString();
                    da.InsertCommand.Parameters.Add("@Abono", SqlDbType.VarChar).Value = textBox1.Text;
                    da.InsertCommand.Parameters.Add("@Mes", SqlDbType.VarChar).Value = comboBox2.Text;
                    da.InsertCommand.Parameters.Add("@Año", SqlDbType.VarChar).Value = comboBox4.Text;
                    da.InsertCommand.Parameters.Add("@Concepto", SqlDbType.VarChar).Value = textBox2.Text;
                    tempMonto = textBox1.Text;
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

                    //if (LoEnvioSioNO == true)
                    //{
                    //       EnviaEMailTicket(comboBox1.SelectedItem.ToString(), comboBox2.Text, currentYear, tempMonto);

                    //}
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

        private void ImprimePagosMensuales(string identif, string Año)
        {

            label8.Text = Año;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SELECT folio, mes, abono, fecha FROM pagos Empleados where ID = '" + identif + "' and Año = '" + Año + "'", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[2].DefaultCellStyle.Format = "C";
            dataGridView2.Columns[3].DefaultCellStyle.Format = "dd/MMM/yyyy";
        }

        private void EnviaEMailTicket(string IDPago, string MesPago, string AñoPago, string MontoPago)
        {
            string str, strfolio, EMailPago;

            cs.Open();
            strfolio = "select folio from pagos where id = '" + IDPago + "' and fecha = '" + hoy.ToString() + "'";
            com = new SqlCommand(strfolio, cs);
            reader = com.ExecuteReader();
            if (reader.Read())
            {
                strfolio = reader["Folio"].ToString();
            }
            cs.Close();

            cs.Open();
            str = "select email from empleados where id = '" + IDPago + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    EMailPago = reader["EMail"].ToString();

                    if (EMailPago == "" || EMailPago == null)
                    {
                        MessageBox.Show("El usuario no tiene un correo electronico válido");
                    }
                    else
                    {

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("calmecacfitness@gmail.com");
                        message.Subject = "Calmecac Gym - Recibo de Pago de " + MesPago + " del " + AñoPago + " ";
                        message.Body = "Comprobante de pago: \n\nFolio: " + strfolio + " \nNombre: " + Nombrex + " " + Apellidox + "\nMes: " + MesPago + "\nAño: " + AñoPago + "\nMonto: $" + MontoPago + "\nCorreo: " + EMailPago + "\n\nCalmecac Gym agradece tu preferencia\n Este Pago no exime adeudos anteriores";
                        message.To.Add(EMailPago);
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new NetworkCredential("calmecacfitness@gmail.com", "calmecacfitness1");
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.Send(message);
                        MessageBox.Show("Correo Electronico Enviado");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error al mandar comprobante de pago \nNo hay direccion de correo electrónico de este usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona a un usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Selecciona el año a consultar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                ImprimePagosMensuales(comboBox1.SelectedItem.ToString(), comboBox3.SelectedItem.ToString());
        }

        public void HighlightMorosos()
        {
            DateTime fecha = new DateTime();
            double dias = 0;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                fecha = Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value.ToString());
                dias = (hoy - fecha).TotalDays;

                if (dias > 30)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str, EMailPago;

            cs.Open();
            str = "select FechaRecordat from empleados where id = '" + clave + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                try
                {
                    FechaRecordatorio = Convert.ToDateTime(reader["FechaRecordat"].ToString());
                }
                catch
                {

                }
            }
            cs.Close();

            cs.Open();
            str = "select email from empleados where id = '" + clave + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    EMailPago = reader["EMail"].ToString();
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("calmecacfitness@gmail.com");
                    message.Subject = "Calmecac Gym - Recordatorio de Pago";
                    message.Body = "Estimado " + NombreCorreo + " " + ApellidoCorreo + " \n \n Le recordamos que tiene un adeudo de su mensualidad, agradecemos se ponga al corriente. \n \n \n  Camecac Gym agradece tu preferencia\n ";
                    message.To.Add(EMailPago);
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("calmecacfitness@gmail.com", "calmecacfitness1");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Send(message);
                    MessageBox.Show("Correo Electronico Enviado");
                }

                cs.Close();
            }
            catch
            {
                MessageBox.Show("Error al mandar comprobante de pago \nNo hay direccion de correo electrónico de este usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cs.Open();

            str = "update Empleados set FechaRecordat = '" + hoy.ToString() + "' where ID = '" + clave + "'";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            cs.Close();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HighlightMorosos();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string dato;
            DateTime FechaDeRecordatorioFormatoDT = DateTime.MinValue; ;
            clave = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string FechaDelUltimoPago = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            DateTime FechaDelUltimoPagoDate = Convert.ToDateTime(FechaDelUltimoPago);
            double DiasDesdeUltimoPago = (hoy - FechaDelUltimoPagoDate).TotalDays;
            string FechaDeRecordatorioString, FechaDeRecordatorioStringEspanol;                       

            button3.Enabled = true;

            cs.Open();
            dato = "select nombre from Empleados where ID = '" + clave + "'";
            com = new SqlCommand(dato, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {

                NombreCorreo = textBox3.Text = reader["nombre"].ToString();
                textBox3.Text = NombreCorreo;
            }
            cs.Close();

            cs.Open();
            dato = "select Apellido from Empleados where ID = '" + clave + "'";
            com = new SqlCommand(dato, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                ApellidoCorreo = reader["Apellido"].ToString();
                textBox4.Text = ApellidoCorreo;
            }
            cs.Close();

            cs.Open();
            dato = "select FechaRecordat from Empleados where ID = '" + clave + "'";
            com = new SqlCommand(dato, cs);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                FechaDeRecordatorioString = reader["FechaRecordat"].ToString();
                FechaDeRecordatorioFormatoDT = Convert.ToDateTime(FechaDeRecordatorioString);
                FechaDeRecordatorioStringEspanol = FechaDeRecordatorioFormatoDT.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));

                if (FechaDeRecordatorioStringEspanol == "01 enero 2017")
                {
                    label14.Text = "";
                }
                else
                {
                    label14.Text = FechaDeRecordatorioStringEspanol;
                }
            }
            cs.Close();

            double DiasDesdeUltimoCorreoEnviado = (hoy - FechaDeRecordatorioFormatoDT).TotalDays;

            label15.Text = DiasDesdeUltimoCorreoEnviado.ToString();

            if (DiasDesdeUltimoCorreoEnviado < 31 || DiasDesdeUltimoPago < 31)
            {
                button3.Enabled = false;
            }
        }

        //private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{

        //}

        private bool VerificaSiYaPago(string ElID, string ElMes, string Elaño)
        {
            bool tiene;
            string str;

            cs.Open();
            str = "Select Abono from Pagos where id = '" + ElID + "' and Mes = '" + ElMes + "' and Año = '" + Elaño + "' ";
            com = new SqlCommand(str, cs);
            reader = com.ExecuteReader();

            if (string.IsNullOrEmpty(ElMes))
            {
                tiene = false;
            }
            else
            {

                if (reader.Read())
                {
                    tiene = true;
                }
                else
                {
                    tiene = false;
                }
            }
            cs.Close();
            return tiene;
        }
    }
}

