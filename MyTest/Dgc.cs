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
    public partial class Dgc : Form
    {
        public Dgc()
        {
            InitializeComponent();
        }

        private void Dgc_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
                 dt.Columns.Add(new DataColumn("Name"));
                    dt.Columns.Add(new DataColumn("Age"));
             dt.Columns.Add(new DataColumn("Sex"));
      
            DataRow row = dt.NewRow();
                row["Name"] = "陈蒙";
                 row["Age"] = "22";
               row["Sex"] = "男";
              dt.Rows.Add(row);
           gridControl1.DataSource = dt;
        }
    }
}
