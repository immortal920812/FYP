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
        public DataTable dt1 = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataSet ds1 = new DataSet();
        public DataSet ds2 = new DataSet();


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
                CertificateFile = @"C:\certificate\client.pfx",
                CACertificateFile = @"C:\certificate\server-ca.pem",
                CertificatePassword = "pass",
                SslMode = MySqlSslMode.VerifyCA,
            };

            using (MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate.ConnectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM" + " " + this.comboBox1.SelectedItem.ToString() + ";", databaseConnection))
                {
                    adapter.Fill(dt1);
                }

            }
            if (this.comboBox2.SelectedText.Contains("("))
            {

            }
            else
            {
                using (MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate.ConnectionString))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM" + " " + this.comboBox2.SelectedItem.ToString() + ";", databaseConnection))
                    {
                        adapter.Fill(dt2);
                    }

                }
            }
            dt1.Merge(dt2, false, MissingSchemaAction.Add);
        
            Form2.dgv1.DataSource = dt1;

            string PageText = this.comboBox1.SelectedItem.ToString() + "+" + this.comboBox2.SelectedItem.ToString();

            f2.Form3TransmitData(PageText,dt1);

            this.Close();
        }
    }
}
