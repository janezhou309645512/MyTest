using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            DataGridViewRow row = new DataGridViewRow();

            int index = dataGridView1.Rows.Add(row);
            dataGridView1.Rows[index].Cells[0].Value = textBox1.Text;
            dataGridView1.Rows[index].Cells[1].Value = textBox2.Text;
            dataGridView1.Rows[index].Cells[2].Value = textBox3.Text;
            dataGridView1.Rows[index].Cells[3].Value = textBox4.Text;
            dataGridView1.Rows[index].Cells[4].Value = dateTimePicker1.Value.Date.ToLongDateString();
            //dataGridView1.Rows[index].Cells[5].Value = Image.FromFile(textBox5.Text);
            dataGridView1.Rows[index].Cells[5].Value = textBox5.Text;
            */

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //最大化窗体
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            /*
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            //DataGridViewImageColumn col5 = new DataGridViewImageColumn();

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxCell celltext = new DataGridViewTextBoxCell();
            DataGridViewImageCell cellimage = new DataGridViewImageCell();


            col.CellTemplate = col1.CellTemplate = col2.CellTemplate = col3.CellTemplate = col4.CellTemplate = celltext;
            col5.CellTemplate = celltext;
            col.HeaderText = "名称";
            col1.HeaderText = "分类";
            col2.HeaderText = "数量";
            col3.HeaderText = "价格";
            col4.HeaderText = "销售时间";
            col5.HeaderText = "图片";

            dataGridView1.Columns.Add(col);
            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);
            dataGridView1.Columns.Add(col4);
            dataGridView1.Columns.Add(col5);
*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            openFileDialog1.Filter = "png图像文件|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog1.FileName;
                pictureBox1.Load(openFileDialog1.FileName);
    }
        }

        private void button3_Click(object sender, EventArgs e)
       {

            DataSet ds = new DataSet();
            this.openFileDialog1.Filter = "*.xls;*.xlsx|*.xls;*.xlsx";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileNames.Length == 0)
            {
                MessageBox.Show("请选择文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable table = ExcelNpoi.ExcelSheetImportToDataTable(openFileDialog1.FileName,"Sheet1");
            ds.Tables.Add(table);
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn c = new DataGridViewColumn();





        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExcelNpoi.ExportToExcel(dataGridView1, "Sheet1");


           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
