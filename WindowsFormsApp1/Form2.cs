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
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        DataGridView dgv1 = new DataGridView();
        public static int index = 0;

        public Form2()
        {
            InitializeComponent();
            Datalist();
        }

        private void LoadChartVariables()
        {
            var goolglecloudcertificate = dataconnect(this.comboBox1.SelectedItem.ToString());

            MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate);

            comboBox3.Items.Clear();

            comboBox4.Items.Clear();

            TabPage Page = new TabPage();

            Page = this.tabControl1.SelectedTab;

            using (MySqlCommand commandDatabase = new MySqlCommand("SHOW COLUMNS FROM" + " " + Page.Text + ";", databaseConnection))
                {
                databaseConnection.Open(); 
                    using (MySqlDataReader myReader = commandDatabase.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            comboBox3.Items.Add(myReader[0].ToString());
                            comboBox4.Items.Add(myReader[0].ToString());
                        }
                    }

            }

        }


        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            //dgViewFiles.Columns[1].Frozen = true;
        }

        //connection setting
        public string dataconnect(string Database)
        {
            var goolglecloudcertificate = new MySqlConnectionStringBuilder
            {
                Server = "35.198.216.109",
                UserID = "long",
                Password = "root",
                //Database = Database,
                CertificateFile = @"C:\certificate\client.pfx",
                CACertificateFile = @"C:\certificate\server-ca.pem",
                CertificatePassword = "pass",
                SslMode = MySqlSslMode.VerifyCA,
            };
            return goolglecloudcertificate.ToString();
        }

        //Generate table and schema when form2 launch.
        private void Datalist()
        {
            var goolglecloudcertificate= dataconnect("Xspace");

            MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate);

     
            try
            {
                DataGridView dsv1 = new DataGridView();

                databaseConnection.Open();

                string query = "show databases";

                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

                using (MySqlDataReader myReader = commandDatabase.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        if (myReader[0].ToString()!= "information_schema" && myReader[0].ToString() != "mysql" && myReader[0].ToString() != "performance_schema" && myReader[0].ToString() != "sys")
                        {
                            comboBox1.Items.Add(myReader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            /*finally
            {
                if (databaseConnection != null && databaseConnection.State == ConnectionState.Open)
                {
                    databaseConnection.Clone();
                }
            }*/
        }

        /*private void Schemalist_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Tablelist.Items.Clear();

                string MySQLConnectionString = "datasource=127.0.0.1;;port=3306;username=root;password=root;database='" + this.Schemalist.SelectedItem.ToString() + "';";

                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);

                MySqlCommand commandDatabase = new MySqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA ='"+ this.Schemalist.SelectedItem.ToString() + "';", databaseConnection);

                databaseConnection.Open();

                using (MySqlDataReader myReader = commandDatabase.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        Tablelist.Items.Add(myReader[0].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

        
           
        }*/

        //Generate schema and table
        public void button1_Click(object sender, EventArgs e)
        {

            //string MySQLConnectionString = "datasource=127.0.0.1;;port=3306;username=root;password=root;database='" + this.comboBox1.SelectedItem.ToString() + "';";
            var goolglecloudcertificate = dataconnect(this.comboBox1.SelectedItem.ToString());
            using (MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate))
             {
                 using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM" + " " + this.comboBox1.SelectedItem.ToString() + "." + this.comboBox2.SelectedItem.ToString() + ";", databaseConnection))
                 {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv1.DataSource = dt;
                    int colcount = dt.Columns.Count;
                    Console.WriteLine("列数" + colcount);
                    //auto size for datagrid//

                    /*int height = 0;
                    foreach (DataGridViewRow row in dgv1.Rows)
                    {
                        height += row.Height;
                    }
                    height += dgv1.ColumnHeadersHeight;

                    int width = 0;
                    foreach (DataGridViewColumn col in dgv1.Columns)
                    {
                        width += col.Width;
                    }
                    width += dgv1.RowHeadersWidth;

                    dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;*/
                }

             }



            TabPage Page = new TabPage();

            Page.Name = "Page" + index.ToString();

            Page.Text = this.comboBox1.SelectedItem.ToString()+"."+ this.comboBox2.SelectedItem.ToString();

            Page.TabIndex = index;
            
            this.tabControl1.Controls.Add(Page);

            /*this.dgv1.Width = Page.Width;

            this.dgv1.Height = Page.Height;*/ //fix length fill cannot autosize control

            this.dgv1.Dock = DockStyle.Fill;

            AutoSizeColumn(dgv1);

            Page.Controls.Add(dgv1);

            this.tabControl1.SelectedTab = Page;

            LoadChartVariables();

            index++;
            //size output
            /*int x = this.dgv1.Location.X;//当前控件在窗体的X轴位置
            int y = this.dgv1.Location.Y;//当前控件在窗体的Y轴位置

            int width = this.dgv1.Width;//当前控件的宽度
            int height = this.dgv1.Height;//当前控件的高度

            int Right = this.dgv1.Right;//当前窗体边界与控件边界之间的宽度差
            int Bottom = this.dgv1.Bottom;//当前窗体边界与控件边界之间的高度差

            int pagex = this.tabControl1.Location.X;//当前控件在窗体的X轴位置
            int pagey = this.tabControl1.Location.Y;//当前控件在窗体的Y轴位置

            int pagewidth = this.tabControl1.Width;//当前控件的宽度
            int pageheight = this.tabControl1.Height;//当前控件的高度

            int pageRight = this.tabControl1.Right;//当前窗体边界与控件边界之间的宽度差
            int pageBottom = this.tabControl1.Bottom;//当前窗体边界与控件边界之间的高度差
           




            Console.WriteLine("x = " + x);//X轴
            Console.WriteLine("y = " + y);//Y轴

            Console.WriteLine("width = " + width);//宽度
            Console.WriteLine("Height = " + height);//高度

            Console.WriteLine("Right = " + Right);//Y+宽
            Console.WriteLine("Bottom = " + Bottom);//X+高*/
 


        }

        //Open specific Excel file;

        static void Openexcel(string text)
        {
            string path = @"C:\Users\LongYin\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\"+ text;

            var excelapp = new Excel.Application();

            excelapp.Visible = true;

            Excel.Workbooks books = excelapp.Workbooks;

            Excel.Workbook sheet = books.Open(path);
        }

        //Change table when user choose different schema
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();

                //string MySQLConnectionString = "datasource=127.0.0.1;;port=3306;username=root;password=root;database='" + this.comboBox1.SelectedItem.ToString() + "';";
                var goolglecloudcertificate = dataconnect(this.comboBox1.SelectedItem.ToString());
                MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate);

                MySqlCommand commandDatabase = new MySqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA ='" + this.comboBox1.SelectedItem.ToString() + "';", databaseConnection);

                databaseConnection.Open();

                using (MySqlDataReader myReader = commandDatabase.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        comboBox2.Items.Add(myReader[0].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        //Select tabpage eventhandler
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string MySQLConnectionString = "datasource=127.0.0.1;;port=3306;username=root;password=root;database='" + this.comboBox1.SelectedItem.ToString() + "';";
            var goolglecloudcertificate = dataconnect(this.comboBox1.SelectedItem.ToString());
            TabPage Page = new TabPage();

            Page = this.tabControl1.SelectedTab;

            using (MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM" + " " + Page.Text + ";", databaseConnection))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dgv1.DataSource = ds.Tables[0];
                }

            }

            Page.Controls.Add(dgv1);

            this.tabControl1.SelectedTab = Page;

            LoadChartVariables();
        }

        //Cancel current table
        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.tabControl1.Controls.Count >1)
            {
                TabPage Page = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(Page);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var goolglecloudcertificate = dataconnect(this.comboBox1.SelectedItem.ToString());

            using (MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM" + " " + this.comboBox1.SelectedItem.ToString() + "." + this.comboBox2.SelectedItem.ToString() + ";", databaseConnection))
                {
                    DataTable dt = new DataTable("New_DataTable");

                    DataSet ds = new DataSet("New_DataSet");

                    ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                    dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                    adapter.Fill(dt);

                    ds.Tables.Add(dt);

                    TabPage Page = tabControl1.SelectedTab;

                    ExcelLibrary.DataSetHelper.CreateWorkbook(Page.Text+".xls", ds);

                    //Openexcel(Page.Text + ".xls");


                }

            }
        }

        //Save as PDF
        private void button3_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            TabPage Page = tabControl1.SelectedTab;

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Page.Text+".pdf", FileMode.Create));

            doc.Open();

            DataGridView view = this.dgv1;

            PdfPTable table = new PdfPTable(view.Columns.Count);

            for (int j = 0; j < view.Columns.Count; j++)
            {
                table.AddCell(new Phrase(view.Columns[j].HeaderText));
            }
            //Flag the first row as a header
            table.HeaderRows = 1;

            //Add the actual rows from dgv to table
            for (int i = 0; i < view.Rows.Count; i++)
            {
                for (int k = 0; k < view.Columns.Count; k++)
                {
                    if (view[k, i].Value != null)
                    {
                        table.AddCell(new Phrase(view[k, i].Value.ToString()));
                    }
                }
            }

            doc.Add(table);
            doc.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            var goolglecloudcertificate = dataconnect(this.comboBox1.SelectedItem.ToString());
            MySqlConnection databaseConnection = new MySqlConnection(goolglecloudcertificate);
            MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM" + " " + this.comboBox1.SelectedItem.ToString() + "." + this.comboBox2.SelectedItem.ToString() + ";", databaseConnection);
            MySqlDataReader myReader;
            try
            {
                databaseConnection.Open();
                myReader = commandDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    this.chart1.Series["Series1"].Points.AddXY(myReader.GetString(this.comboBox3.SelectedItem.ToString()), myReader.GetInt32(this.comboBox4.SelectedItem.ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            foreach (TabPage Page in tabControl1.TabPages)
            {
                Console.WriteLine("hi");
                f3.comboBox1.Items.Add(Page.Text);
                f3.comboBox2.Items.Add(Page.Text);
            }
            f3.ShowDialog(this);
        }
    }

}
