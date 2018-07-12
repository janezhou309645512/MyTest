using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace Datum
{
    public class DataIn
    {
        public static System.Data.DataTable GetExcelData(string excelFilePath)
        {

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Sheets sheets;
            object oMissiong = System.Reflection.Missing.Value;
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                if (app == null)
                {
                    return null;
                }
                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);


                //将数据读入到DataTable中——Start  

                sheets = workbook.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);//读取第一张表
                if (worksheet == null)
                    return null;

                string cellContent;
                int iRowCount = worksheet.UsedRange.Rows.Count;
                int iColCount = worksheet.UsedRange.Columns.Count;
                Microsoft.Office.Interop.Excel.Range range;

                //负责列头Start
                DataColumn dc;
                int ColumnID = 1;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1];
                while (range.Text.ToString().Trim() != "")
                {
                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = range.Text.ToString().Trim();
                    dt.Columns.Add(dc);

                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, ++ColumnID];
                }
                //End

                for (int iRow = 2; iRow <= iRowCount; iRow++)
                {
                    DataRow dr = dt.NewRow();

                    for (int iCol = 1; iCol <= iColCount; iCol++)
                    {
                        range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[iRow, iCol];

                        cellContent = (range.Value2 == null) ? "" : range.Text.ToString();
                        dr[iCol - 1] = cellContent;
                    }

                    dt.Rows.Add(dr);
                }

                //将数据读入到DataTable中——End
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                workbook.Close(false, oMissiong, oMissiong);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
                app.Workbooks.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        static public void toexcel(System.Windows.Forms.DataGridView grid)
        {
            if (Type.GetTypeFromProgID("Excel.Application") == null)
            {
                MessageBox.Show("对不起,没有检测到EXCEL程序,请安装后再试");
                return;
            }
            if (grid.RowCount > 10000)
            {
                if (MessageBox.Show("导出数据大于10000项,将会需要较长时间,请确认是否导出！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    return;
                }
            }
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = false;
            xlapp.Application.Workbooks.Add(true);
            object[,] obj = new object[grid.RowCount + 1, grid.ColumnCount];
            int count = 0;
            foreach (DataGridViewColumn c in grid.Columns)//导出表头
            {
                if (c.Visible)
                {
                    obj[0, count] = c.HeaderText;
                    count++;
                }
            }
            foreach (DataGridViewRow row in grid.Rows)//导出表体
            {
                count = 0;
                foreach (DataGridViewCell c in row.Cells)
                {
                    if (c.Visible)
                    {
                        obj[row.Index + 1, count] = c.Value;
                        count++;
                    }
                }
            }
            Microsoft.Office.Interop.Excel.Range xlrange = xlapp.Range[xlapp.Cells[1, 1], xlapp.Cells[grid.RowCount + 1, grid.ColumnCount]];
            xlrange.Value2 = obj;
            xlapp.Visible = true;
        }
    }
}
