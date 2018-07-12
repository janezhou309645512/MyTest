using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }
        private int id;
        private void button1_Click(object sender, EventArgs e)
        {
          int  i = SqlDesigner.ExecuteNoQuery("insert into Ls_Mms_Info(info,more,name) values('" + textBox1.Text+ "','" + textBox2.Text + "','" + textBox3.Text + "')");
            if (i > 0)
            {
                MessageBox.Show("数据添加成功");
            }
            else {
                MessageBox.Show("数据添加不成功");
            }
        }
        //查询
        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "select * from Ls_Mms_Info where 1 = 1";
            if (textBox1.Text.Trim() != "")
            {
                sql = sql + " and info = '" + textBox1.Text + "' ";
            }
            if (textBox2.Text.Trim() != "")
            {
                sql = sql + " and more = '" + textBox2.Text + "' ";
            }
            if (textBox3.Text.Trim() != "")
            {
                sql = sql + " and name = '" + textBox3.Text + "' ";
            }


            DataSet ds = SqlDesigner.ExecuteDataSet(sql);
            DataTable dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }
        //选中哪个删除
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult RSS = MessageBox.Show(this, "确定要删除选中行数据码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (RSS)
            {
                case DialogResult.Yes:
                    for (int i = this.dataGridView1.SelectedRows.Count; i > 0; i--)
                    {
                        int ID = Convert.ToInt32(dataGridView1.SelectedRows[i - 1].Cells[0].Value);
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                        //使用获得的ID删除数据库的数据
                        string sql = "delete  from Ls_Mms_Info where id="+ID;
                        int s = Convert.ToInt32(SqlDesigner.ExecuteNoQuery(sql));  //cl是操作类的一个对像，Execute()是类中的一个方法
                        if (s != 0)
                        {
                            MessageBox.Show("成功删除选中行数据！");
                        }
                    }
                    break;
                case DialogResult.No:
                    break;
            }
        }
        //修改保存
        private void button4_Click(object sender, EventArgs e)
        {
          int  i = SqlDesigner.ExecuteNoQuery("update Ls_Mms_Info set info='" + textBox1.Text + "',more='" + textBox2.Text + "',name='" + textBox3.Text + "' where id="+id);
            if (i>0)
            {
                MessageBox.Show("修改数据成功");
            }
            else
            {
                MessageBox.Show("修改数据不成功");

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();




        }

        private void Update_Load(object sender, EventArgs e)
        {

            DataSet ds = SqlDesigner.ExecuteDataSet("select * from Ls_Mms_Info");
            DataTable dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();


        }
    }
}
