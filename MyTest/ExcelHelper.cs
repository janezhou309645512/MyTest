using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest
{
    class ExcelHelper
    {
        /// <summary>  
        /// 判断是否为兼容模式  
        /// </summary>  
        /// <param name="filePath"></param>  
        /// <returns></returns>  
        private static bool GetIsCompatible(string filePath)
        {
            return filePath.EndsWith(".xls", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>  
        /// 创建工作薄  
        /// </summary>  
        /// <param name="isCompatible"></param>  
        /// <returns></returns>  
        private static IWorkbook CreateWorkbook(bool isCompatible)
        {
            if (isCompatible)
            {
                return new HSSFWorkbook();
            }
            else
            {
                return new XSSFWorkbook();
            }
        }
        // <summary>  
        /// 创建工作薄(依据文件流)  
        /// </summary>  
        /// <param name="isCompatible"></param>  
        /// <param name="stream"></param>  
        /// <returns></returns>  
        private static IWorkbook CreateWorkbook(bool isCompatible, Stream stream)
        {
            if (isCompatible)
            {
                return new HSSFWorkbook(stream);
            }
            else
            {
                return new XSSFWorkbook(stream);
            }
        }
        /// <summary>  
        /// 创建表格头单元格  
        /// </summary>  
        /// <param name="sheet"></param>  
        /// <returns></returns>  
        private static ICellStyle GetCellStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.FillPattern = FillPattern.SolidForeground;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;

            return style;
        }

        /// <summary>  
        /// 从工作表中生成DataTable  
        /// </summary>  
        /// <param name="sheet"></param>  
        /// <param name="headerRowIndex"></param>  
        /// <returns></returns>  
        private static DataTable GetDataTableFromSheet(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(headerRowIndex);
            try
            {
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    //调用GetCellValue()函数  
                    ICell cells = headerRow.GetCell(i); //一个单元格  
                    string GCV = GetCellValue(cells);

                    if (GCV == null || GCV.Trim() == "")
                    {
                        // 如果遇到第一个空列，则不再继续向后读取  
                        cellCount = i + 1;
                        break;

                    }
                    DataColumn column = new DataColumn(GCV);
                    table.Columns.Add(column);

                }

                for (int i = (headerRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);

                    //调用GetCellValue()函数  
                    ICell cell = row.Cells[0]; //一个单元格  
                    string GC = GetCellValue(cell);

                    // if (row != null && !string.IsNullOrEmpty(row.Cells[0].StringCellValue))  
                    if (row != null && !string.IsNullOrEmpty(GC))
                    {
                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {

                                //调用GetCellValue()函数  
                                ICell cells = row.GetCell(j); //一个单元格  
                                string GCV = GetCellValue(cells);
                                dataRow[j] = GCV.ToString();

                                // dataRow[j] = row.GetCell(j).ToString();  
                            }
                        }

                        table.Rows.Add(dataRow);
                    }
                }
            }
            catch
            {
                MessageBox.Show("文件内容格式不对!\n\n存在空内容或者列名相同\n\n请重新选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return table;
        }




        /// <summary>  
        /// 由DataSet导出Excel  
        /// </summary>  
        /// <param name="sourceTable">要导出数据的DataTable</param>  
        /// <returns>Excel工作表</returns>  
        public static string ExportToExcel(DataSet sourceDs, string filePath)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            if (string.IsNullOrEmpty(filePath)) return null;

            bool isCompatible = GetIsCompatible(filePath);

            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);

            for (int i = 0; i < sourceDs.Tables.Count; i++)
            {
                DataTable table = sourceDs.Tables[i];
                string sheetName = "result" + i.ToString();
                ISheet sheet = workbook.CreateSheet(sheetName);
                IRow headerRow = sheet.CreateRow(0);
                // handling header.  
                foreach (DataColumn column in table.Columns)
                {
                    ICell cell = headerRow.CreateCell(column.Ordinal);
                    cell.SetCellValue(column.ColumnName);
                    cell.CellStyle = cellStyle;
                }

                // handling value.  
                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue((row[column] ?? "").ToString());
                    }

                    rowIndex++;
                }
            }

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();
            workbook = null;

            return filePath;

        }

        /// <summary>  
        /// 由DataTable导出Excel  
        /// </summary>  
        /// <param name="sourceTable">要导出数据的DataTable</param>  
        /// <returns>Excel工作表</returns>  
        public static string ExportToExcel(DataTable sourceTable, string sheetName, string filePath)
        {
            if (sourceTable.Rows.Count <= 0) return null;

            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            if (string.IsNullOrEmpty(filePath)) return null;

            bool isCompatible = GetIsCompatible(filePath);

            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);

            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);
            // handling header.  
            foreach (DataColumn column in sourceTable.Columns)
            {
                ICell headerCell = headerRow.CreateCell(column.Ordinal);
                headerCell.SetCellValue(column.ColumnName);
                headerCell.CellStyle = cellStyle;
            }

            // handling value.  
            int rowIndex = 1;

            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue((row[column] ?? "").ToString());
                }

                rowIndex++;
            }
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();

            sheet = null;
            headerRow = null;
            workbook = null;

            return filePath;
        }
        /// <summary>  
        /// 由List导出Excel  
        /// </summary>  
        /// <typeparam name="T">类型</typeparam>  
        /// <param name="data">在导出的List</param>  
        /// <param name="sheetName">sheet名称</param>  
        /// <returns></returns>  
        public static string ExportToExcel<T>(List<T> data, IList<KeyValuePair<string, string>> headerNameList, string sheetName, string filePath) where T : class
        {
            if (data.Count <= 0) return null;

            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            if (string.IsNullOrEmpty(filePath)) return null;

            bool isCompatible = GetIsCompatible(filePath);

            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);

            for (int i = 0; i < headerNameList.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headerNameList[i].Value);
                cell.CellStyle = cellStyle;
            }

            Type t = typeof(T);
            int rowIndex = 1;
            foreach (T item in data)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int n = 0; n < headerNameList.Count; n++)
                {
                    object pValue = t.GetProperty(headerNameList[n].Key).GetValue(item, null);
                    dataRow.CreateCell(n).SetCellValue((pValue ?? "").ToString());
                }
                rowIndex++;
            }
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();

            sheet = null;
            headerRow = null;
            workbook = null;

            return filePath;
        }
        /// <summary>  
        /// 由DataGridView导出  
        /// </summary>  
        /// <param name="grid"></param>  
        /// <param name="sheetName"></param>  
        /// <param name="filePath"></param>  
        /// <returns></returns>  
        public static string ExportToExcel(DataGridView grid, string sheetName, string filePath)
        {
            if (grid.Rows.Count <= 0) return null;

            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            if (string.IsNullOrEmpty(filePath)) return null;

            bool isCompatible = GetIsCompatible(filePath);

            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow headerRow = sheet.CreateRow(0);

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(grid.Columns[i].Name);
                cell.CellStyle = cellStyle;
            }

            int rowIndex = 1;
            foreach (DataGridViewRow row in grid.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int n = 0; n < grid.Columns.Count; n++)
                {
                    dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                }
                rowIndex++;
            }

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();

            sheet = null;
            headerRow = null;
            workbook = null;

            return filePath;
        }


        #region 公共导入方法  

        /// <summary>  
        /// 由Excel导入DataTable  
        /// </summary>  
        /// <param name="excelFileStream">Excel文件流</param>  
        /// <param name="sheetName">Excel工作表名称</param>  
        /// <param name="headerRowIndex">Excel表头行索引</param>  
        /// <param name="isCompatible">是否为兼容模式</param>  
        /// <returns>DataTable</returns>  
        public static DataTable ImportFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex, bool isCompatible)
        {
            IWorkbook workbook = CreateWorkbook(isCompatible, excelFileStream);
            ISheet sheet = null;
            int sheetIndex = -1;
            if (int.TryParse(sheetName, out sheetIndex))
            {
                sheet = workbook.GetSheetAt(sheetIndex);
            }
            else
            {
                sheet = workbook.GetSheet(sheetName);
            }

            DataTable table = GetDataTableFromSheet(sheet, headerRowIndex);

            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>  
        /// 由Excel导入DataTable  
        /// </summary>  
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>  
        /// <param name="sheetName">Excel工作表名称</param>  
        /// <param name="headerRowIndex">Excel表头行索引</param>  
        /// <returns>DataTable</returns>  
        public static DataTable ImportFromExcel(string excelFilePath, string sheetName, int headerRowIndex)
        {

            try
            {
                using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
                {
                    bool isCompatible = GetIsCompatible(excelFilePath);
                    return ImportFromExcel(stream, sheetName, headerRowIndex, isCompatible);
                }
            }
            catch
            {
                MessageBox.Show("请关闭文件,再导入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DataTable DT = new DataTable();
                return DT;

            }

        }
        /// <summary>  
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable  
        /// </summary>  
        /// <param name="excelFileStream">Excel文件流</param>  
        /// <param name="headerRowIndex">Excel表头行索引</param>  
        /// <param name="isCompatible">是否为兼容模式</param>  
        /// <returns>DataSet</returns>  
        public static DataSet ImportFromExcel(Stream excelFileStream, int headerRowIndex, bool isCompatible)
        {
            DataSet ds = new DataSet();
            IWorkbook workbook = CreateWorkbook(isCompatible, excelFileStream);
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                DataTable table = GetDataTableFromSheet(sheet, headerRowIndex);
                ds.Tables.Add(table);
            }

            excelFileStream.Close();
            workbook = null;

            return ds;
        }

        /// <summary>  
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable  
        /// </summary>  
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>  
        /// <param name="headerRowIndex">Excel表头行索引</param>  
        /// <returns>DataSet</returns>  
        public static DataSet ImportFromExcel(string excelFilePath, int headerRowIndex)
        {
            try
            {
                using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
                {
                    bool isCompatible = GetIsCompatible(excelFilePath);
                    return ImportFromExcel(stream, headerRowIndex, isCompatible);
                }
            }
            catch
            {
                MessageBox.Show("请关闭文件,再导入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DataSet ds = new DataSet();
                return ds;

            }


        }





        /// <summary>  
        /// 将Excel的列索引转换为列名，列索引从0开始，列名从A开始。如第0列为A，第1列为B...  
        /// </summary>  
        /// <param name="index">列索引</param>  
        /// <returns>列名，如第0列为A，第1列为B...</returns>  
        public static string ConvertColumnIndexToColumnName(int index)
        {
            index = index + 1;
            int system = 26;
            char[] digArray = new char[100];
            int i = 0;
            while (index > 0)
            {
                int mod = index % system;
                if (mod == 0) mod = system;
                digArray[i++] = (char)(mod - 1 + 'A');
                index = (index - 1) / 26;
            }
            StringBuilder sb = new StringBuilder(i);
            for (int j = i - 1; j >= 0; j--)
            {
                sb.Append(digArray[j]);
            }
            return sb.ToString();
        }


        /// <summary>  
        /// 转化日期  
        /// </summary>  
        /// <param name="date">日期</param>  
        /// <returns></returns>  
        public static DateTime ConvertDate(object date)
        {
            string dtStr = (date ?? "").ToString();

            DateTime dt = new DateTime();

            if (DateTime.TryParse(dtStr, out dt))
            {
                return dt;
            }

            try
            {
                string spStr = "";
                if (dtStr.Contains("-"))
                {
                    spStr = "-";
                }
                else if (dtStr.Contains("/"))
                {
                    spStr = "/";
                }
                string[] time = dtStr.Split(spStr.ToCharArray());
                int year = Convert.ToInt32(time[2]);
                int month = Convert.ToInt32(time[0]);
                int day = Convert.ToInt32(time[1]);
                string years = Convert.ToString(year);
                string months = Convert.ToString(month);
                string days = Convert.ToString(day);
                if (months.Length == 4)
                {
                    dt = Convert.ToDateTime(date);
                }
                else
                {
                    string rq = "";
                    if (years.Length == 1)
                    {
                        years = "0" + years;
                    }
                    if (months.Length == 1)
                    {
                        months = "0" + months;
                    }
                    if (days.Length == 1)
                    {
                        days = "0" + days;
                    }
                    rq = "20" + years + "-" + months + "-" + days;
                    dt = Convert.ToDateTime(rq);
                }
            }
            catch
            {
                throw new Exception("日期格式不正确，转换日期失败！");
            }
            return dt;
        }

        /// <summary>  
        /// 转化数字  
        /// </summary>  
        /// <param name="d">数字字符串</param>  
        /// <returns></returns>  
        public static decimal ConvertDecimal(object d)
        {
            string dStr = (d ?? "").ToString();
            decimal result = 0;
            if (decimal.TryParse(dStr, out result))
            {
                return result;
            }
            else
            {
                throw new Exception("数字格式不正确，转换数字失败！");
            }

        }




        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank: //空数据类型 这里类型注意一下，不同版本NPOI大小写可能不一样,有的版本是Blank（首字母大写)  
                    return string.Empty;
                case CellType.Boolean: //bool类型  
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric: //数字类型  
                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型  
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //其它数字  
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown: //无法识别类型  
                default: //默认类型  
                    return cell.ToString();//  
                case CellType.String: //string 类型  
                    return cell.StringCellValue;
                case CellType.Formula: //带公式类型  
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }







    }
}
#endregion