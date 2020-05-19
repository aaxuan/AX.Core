using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AX.Core.Helper
{
    public static class EXCEL
    {
        //public static MemoryStream ToEXCEL(DataTable dataTable, string sheetName, bool hasColumnName = true)
        //{
        //    if (string.IsNullOrWhiteSpace(sheetName))
        //    { sheetName = dataTable.TableName; }

        //    IWorkbook excel = new HSSFWorkbook();
        //    ISheet sheet = excel.CreateSheet(sheetName);

        //    var index = 0;

        //    if (hasColumnName)
        //    {
        //        IRow row = sheet.CreateRow(index);
        //        for (int i = 0; i < dataTable.Columns.Count; i++)
        //        {
        //            ICell cell = row.CreateCell(i);
        //            cell.SetCellValue(dataTable.Columns[i].ColumnName);
        //        }
        //        index++;
        //    }

        //    for (int i = 0; i < dataTable.Rows.Count; i++)
        //    {
        //        var row = sheet.CreateRow(i + index);
        //        for (int j = 0; j < dataTable.Columns.Count; j++)
        //        {
        //            row.CreateCell(j).SetCellValue(dataTable.Rows[i][j].ToString());
        //            sheet.AutoSizeColumn(j);
        //        }
        //    }

        //    MemoryStream stream = new MemoryStream();
        //    excel.Write(stream);
        //    return stream;
        //}

        //public static DataTable ToTable()
        //{
        //    var result = new DataTable();
        //    IWorkbook workbook;

        //    using (FileStream fs = new MemoryStream())
        //    {
        //        //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
        //        if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
        //        if (workbook == null) { return null; }
        //        ISheet sheet = workbook.GetSheetAt(0);

        //        //表头
        //        IRow header = sheet.GetRow(sheet.FirstRowNum);
        //        List<int> columns = new List<int>();
        //        for (int i = 0; i < header.LastCellNum; i++)
        //        {
        //            object obj = GetValueType(header.GetCell(i));
        //            if (obj == null || obj.ToString() == string.Empty)
        //            {
        //                dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
        //            }
        //            else
        //                dt.Columns.Add(new DataColumn(obj.ToString()));
        //            columns.Add(i);
        //        }
        //        //数据
        //        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
        //        {
        //            DataRow dr = dt.NewRow();
        //            bool hasValue = false;
        //            foreach (int j in columns)
        //            {
        //                dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
        //                if (dr[j] != null && dr[j].ToString() != string.Empty)
        //                {
        //                    hasValue = true;
        //                }
        //            }
        //            if (hasValue)
        //            {
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //    }
        //    return dt;
        //}
    }
}
