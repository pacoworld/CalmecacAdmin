using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;


namespace FormaAplicacion
{
    public partial class Registro : Form
    {
        DateTime hoy = DateTime.Today;
        public Registro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection cs = new SqlConnection("Data Source = .\\sqlexpress; Initial Catalog = DatabasePaco; Integrated Security = TRUE");
            SqlDataAdapter da = new SqlDataAdapter();            

            da.InsertCommand = new SqlCommand("INSERT INTO Empleados VALUES (@Nombre, @Apellido, @EMail, @Sexo, @Estatus, @Telefono, @MiembroDesde, @Membresia, @FechaNacimiento, @FechaRecordat, @NombreEmergencia, @TelefonoEmergencia)", cs);               
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
            da.InsertCommand.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = textBox4.Text;            
            da.InsertCommand.Parameters.Add("@MiembroDesde", SqlDbType.VarChar).Value = hoy.ToString();            
            da.InsertCommand.Parameters.Add("@Membresia", SqlDbType.VarChar).Value = comboBox2.Text;
            
            string LaFecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            da.InsertCommand.Parameters.Add("@FechaNacimiento", SqlDbType.VarChar).Value = LaFecha;
            da.InsertCommand.Parameters.Add("@FechaRecordat", SqlDbType.VarChar).Value = "2017-01-01";
            da.InsertCommand.Parameters.Add("@NombreEmergencia", SqlDbType.VarChar).Value = textBox5.Text;
            da.InsertCommand.Parameters.Add("@TelefonoEmergencia", SqlDbType.VarChar).Value = textBox6.Text;
            da.InsertCommand.ExecuteNonQuery();

            EnviaEmail(textBox1.Text, textBox2.Text);
            MessageBox.Show("Nuevo usuario dado de alta");
            cs.Close();
        }

        private void EnviaEmail(String nombre, String apellido) {

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(Usuario.CorreoLogin);
                message.Subject = "Bienvenido a Calmecac Gym";
                message.Body = "Felicidades " + nombre + " " + apellido + " por tu inscripción a Calmecac \n Te damos la Bienvenida y juntos te ayudaremos a mejorar tu salud";
                message.To.Add(textBox3.Text);
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(Usuario.CorreoLogin, Usuario.ClaveLogin);
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(message);
                MessageBox.Show("Correo Electronico Enviado");
            }
            catch {
                MessageBox.Show("Error al enviar correo de bienvenida");
            }

        }
        
    }
}
