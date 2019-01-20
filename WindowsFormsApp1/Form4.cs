using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        static public DataTable dt = new DataTable("FilterInfo");
        static public DataGridView dgv1 = new DataGridView();
        BindingSource myBindingSource = new BindingSource();
        static DataSet ds = new DataSet();


        public Form4()
        {
            InitializeComponent();
            DataTableInit();
        }

        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;

            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);

                width += dgViewFiles.Columns[i].Width;
            }
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

        public void DataTableInit()
        {
            DataColumn dc1 = new DataColumn("Filtername", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("Filtervalue", Type.GetType("System.String"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = (Form2)this.Owner;


            f2.Form4TransmitData(dt);

            dt = dt.Clone();

            dgv1.DataSource=dt;

            tabPage1.Controls.Add(dgv1);

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr["Filtername"] = this.comboBox1.Text + " ";
            dr["Filtervalue"] = this.comboBox2.Text +" "+ "\'"+this.textBox1.Text+"\'";
            dt.Rows.Add(dr);
            myBindingSource.DataSource = dt;
            dgv1.DataSource = myBindingSource;
            dgv1.Dock = DockStyle.Fill;
            AutoSizeColumn(dgv1);
            tabPage1.Controls.Add(dgv1);
            tabPage1.Text = "Filter";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myBindingSource.MovePrevious();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            myBindingSource.MoveNext();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myBindingSource.RemoveCurrent();
        }

        public void Form2TransmitData(string PageText,DataSet f2ds)
        {
            ds = f2ds.Copy();
            dt = ds.Tables[PageText].Copy();
            dgv1.DataSource = dt;
            dgv1.Dock = DockStyle.Fill;
            AutoSizeColumn(dgv1);
            tabPage1.Controls.Add(dgv1);
            tabPage1.Text = "Filter";
        }

        public void Cleardt()
        {
            dt.Clear();
        }
    }
}
