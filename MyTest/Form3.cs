using MyTest.kaidan;
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
    public partial class Form3 : Form
    {
        private Form1 f1;
        private Form2 f2;
        private Plan plan;
        private ImportExcel ie;

        public Form3()
        {
            InitializeComponent();

            //最大化窗体
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;




        }

        private void Form3_Load(object sender, EventArgs e)
        {
            f1 = new Form1();

            //下面的代码，说明：frm2是frm1的子窗体.注意，this的妙用  
            f1.MdiParent = this;
            f1.Show();



        }

        private void 人员信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2 = new Form2();
            f2.MdiParent = this;
            f1.Hide();
            f2.Show();
        }

        private void 机台信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            f1 = new Form1();
            //下面的代码，说明：frm2是frm1的子窗体.注意，this的妙用  
            f1.MdiParent = this;
            f2.Show();
        }

        private void 标签打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2 = new Form2();
            f2.MdiParent = this;
            f1.Hide();
            f2.Show();


        }

        private void 排产计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plan = new Plan();
            plan.MdiParent = this;
            
            plan.Show();
        }


        private void 转出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 收货ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            




        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
