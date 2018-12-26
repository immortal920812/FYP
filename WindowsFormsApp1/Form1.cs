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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            runQuery();
        }

        private void runQuery()
        {
            /*string query = textBox1.Text;

            if (query == "")
            {
                MessageBox.Show("Please insert some sql query!");
                return;
            }*/
            //string query = "SELECT * FROM admin.empolyeeinfo WHERE Name='" + this.username_txt + "'and Password='" + this.password_txt + "';";
            var goolglecloudcertificate = new MySqlConnectionStringBuilder
            {
                Server = "35.198.216.109",
                UserID = "long",
                Password = "root",
                Database = "Xspace",
                CertificateFile = @"C:\certificate\client.pfx",
                CACertificateFile = @"C:\certificate\server-ca.pem",
                CertificatePassword= "pass",
                SslMode = MySqlSslMode.VerifyCA,
            };

            //For Local 
            //string MySQLConnectionString = "datasource=35.186.148.3;;port=3306;username=longyin;password=123456;database=admin";

            //MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);

            MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate.ConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM Xspace.admin WHERE username='"+ this.username_txt.Text + "'And password='" + this.password_txt.Text +"';", databaseConnection);

            commandDatabase.CommandTimeout = 60;

            int count = 0;

            try
            {
                databaseConnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();


                while (myReader.Read())
                {
                        count++;
                        //Console.WriteLine(myReader[0].ToString());
                        //comboBox1.Items.Add(myReader[0].ToString());
                        //Console.WriteLine(myReader.GetString(0) + "-" + myReader.GetString(1) + "-" + myReader.GetString(2) + "-" + myReader.GetString(3));
                }
                if (count == 1)
                {
                    MessageBox.Show("Username and Password is correct!");
                    this.Hide();
                    Form2 f2 = new Form2();
                    databaseConnection.Close();
                    f2.ShowDialog();
                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate Username and Password, Please try again.");
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password, Please try again.");
                }



            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }





        }

      
    }
}
