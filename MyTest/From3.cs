using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest
{
    public partial class From3 : Form
    {
        public From3()
        {
            InitializeComponent();
        }

        private void From3_Load(object sender, EventArgs e)
        {
            //最大化窗体
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            ExecuteStoredProcedure();




        }
        public void ExecuteStoredProcedure()
        {
            SqlConnection con = new SqlConnection("server =192.168.3.52;database =RMMS;user =cncpro;pwd=cncpro");
            con.Open();
            SqlCommand scmd = new SqlCommand("T_Slitting",con);
            scmd.CommandType = CommandType.StoredProcedure;//调用命令改成存储格式，若上个语句中是SQL语句则不用这一句
           //如存储过程带参数，怎需要向存储过程传参，否则不需要下面
            SqlParameter[] sps = new SqlParameter[]{
           new SqlParameter("@startTime",dateTimePicker1.Value.Date.ToLongDateString()),
           new SqlParameter("@endTime",dateTimePicker2.Value.Date.ToLongDateString()),
           new SqlParameter("@manuOrder",textBox1.Text.ToString().Trim()),
           new SqlParameter("@materialNumber",textBox2.Text.ToString().Trim())
           new SqlParameter("@materialNumber",textBox2.Text.ToString().Trim())
      };

            scmd.Parameters.AddRange(sps);
            //执行存储过程
            scmd.ExecuteNonQuery();
            //如果想把结果用DataGridView显示出来，需要以下步骤

            SqlDataAdapter da = new SqlDataAdapter(scmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserTable");
            //将返回的数据和DataGrid绑定显示 
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            //关闭数据库
            con.Close();
           




        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteStoredProcedure();
        }
    }
}
