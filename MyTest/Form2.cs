using FastReport;
using MyTest.Common;
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
    public partial class Form2 : Form
    {
        private LableTag[] lableTags = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //最大化窗体
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FastReport.Report report1 = new FastReport.Report();
            try
            {

                // load the existing report
               // report1.Load("E:\\FsFile\\Untitled.frx");
               report1.Load("E:\\FsFile\\Test.frx");
                report1.SetParameterValue("aaa", "adbajdbjadbjadbabdadadad");
                report1.SetParameterValue("bbb", "uiyiyuiyy");
                report1.SetParameterValue("ccc", "1313131313");
              
                /*
                var lableTag = new LableTag();
                lableTag.ProdNo = "800-sdashd-a0-0";
                lableTag.Qty = "300";
                lableTags = new LableTag[] { lableTag }; 



                report1.RegisterData(lableTags, "lableTag");
               // report1.RegisterData("456", "two");
                //report.RegisterData(cdsPrints, "frCds_Print");

                //找到 DataBind  邦定数据  一定要先注册数据才可以邦定

                //DataBand data = report1.FindObject("Data1") as DataBand;
               // data.DataSource = report1.GetDataSource("one");
                // register the dataset

                // report1.RegisterData(FDataSet);
                // report1.GetDataSource("Items").Enabled = true;
                // run the report
                // report1.Show();
                //进行打印预览
                */
                report1.Prepare();
                //report1.Variables.Add.Name:=' Yuan';
                report1.ShowPrepared();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // free resources used by report
                report1.Dispose();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FastReport.Report report1 = new FastReport.Report();
            try
            {
                // load the existing report
             // report1.Load("E:\\FsFile\\Untitled.frx");
              report1.Load("E:\\FsFile\\Test.frx");
                /*
                //填充参数及对应的值
                Type typePrint = typeof(LableTag);
                System.Reflection.PropertyInfo[] pros = typePrint.GetProperties();
                foreach (System.Reflection.PropertyInfo pro in pros)
                {
                    object val = pro == null ? ""
                        : pro.GetValue(lableTags[0], null) == null ? ""
                        : pro.GetValue(lableTags[0], null).ToString();

                    if (val == null)
                    {
                        val = "";
                    }
                    string paraName = "lableTag." + pro.Name;
                    report1.SetParameterValue(paraName, val);

                }
                var lableTag = new LableTag();
                lableTag.ProdNo = "800-sdashd-a0-0";
                lableTag.Qty = "300";
                lableTags = new LableTag[] { lableTag };

                report1.RegisterData(lableTags, "lableTag");
                */
                report1.SetParameterValue("aaa", "111");
                report1.SetParameterValue("bbb", "222");
                report1.SetParameterValue("ccc", "333");
                report1.Design();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // free resources used by report
                report1.Dispose();
            }

             }

        private void setPar()
        {
           





            }








    }
}
