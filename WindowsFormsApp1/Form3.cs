using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlConnector;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private int index = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = (Form2)this.Owner;
            var goolglecloudcertificate = new MySqlConnectionStringBuilder
            {
                Server = "35.198.216.109",
                UserID = "long",
                Password = "root",
                Database = "Xspace",
                CertificateFile = @"C:\certificate\client.pfx",
                CACertificateFile = @"C:\certificate\server-ca.pem",
                CertificatePassword = "pass",
                SslMode = MySqlSslMode.VerifyCA,
            };
            MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate.ConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM" + " "+ f2.comboBox1.SelectedItem.ToString() + "." + f2.comboBox2.SelectedItem.ToString()+ "';", databaseConnection);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
