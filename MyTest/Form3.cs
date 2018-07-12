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
        private Form1 f2;
        private From3 f3;
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
            f2 = new Form1();

            //下面的代码，说明：frm2是frm1的子窗体.注意，this的妙用  
            f2.MdiParent = this;
            f2.Show();



        }

        private void 人员信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f3 = new From3();
            f3.MdiParent = this;
            f2.Hide();
            f3.Show();
        }

        private void 机台信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            f2 = new Form1();
            //下面的代码，说明：frm2是frm1的子窗体.注意，this的妙用  
            f2.MdiParent = this;
            f2.Show();
        }

        private void 标签打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ie = new ImportExcel();
            ie.MdiParent = this;
           
            ie.Show();




        }
    }
}
