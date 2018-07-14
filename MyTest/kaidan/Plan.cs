using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest.kaidan
{
    public partial class Plan : Form
    {
        public Plan()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {





        }

        private void button1_Click(object sender, EventArgs e)
        {

            int i = SqlDesigner.ExecuteNoQuery("insert into ls_mes_plan(gongdan,lot,qty,pro,gongyi) values('" + textBox1.Text + "',20180716," + textBox3.Text + ",'" + textBox2.Text + "',"+textBox4.Text+")");
            if (i > 0)
            {
                MessageBox.Show("数据添加成功");
                //更新数据源
                string sql = "select * from ls_mes_plan";
                DataSet ds = SqlDesigner.ExecuteDataSet(sql);
                DataTable dt = ds.Tables[0];
                gridControl1.DataSource = dt;
              

            }
            else
            {
                MessageBox.Show("数据添加不成功");
            }


        }

        private void Plan_Load(object sender, EventArgs e)
        {
            //最大化窗体
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            string sql = "select * from ls_mes_plan";
            DataSet ds = SqlDesigner.ExecuteDataSet(sql);
            DataTable dt = ds.Tables[0];
            gridControl1.DataSource = dt;



        }
    }
}
