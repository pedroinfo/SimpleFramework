using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;

namespace SimpleFramework.Utils.Excel
{
    public static class ExcelEpplusHelper
    {
        public static DataTable ExcelToDataTable(this ExcelWorksheet ws, bool hasHeaderRow = true)
        {
            var dataTable = new DataTable();
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                dataTable.Columns.Add(hasHeaderRow ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }
            var startRow = hasHeaderRow ? 2 : 1;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var row = dataTable.NewRow();
                foreach (var cell in wsRow) row[cell.Start.Column - 1] = cell.Text;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    
        public static List<T> ExcelToList<T>(this ExcelWorksheet excel)
        {
            return new List<T>();
        }

        public static ExcelWorksheet DataTableToExcel(this DataTable dataTable)
        {
            return null;
        }

        public static ExcelWorksheet ListToExcel(this List<T> list)
        {
            return null;
        }

        public static void SaveDataTableToExcel(DataTable dataTable)
        {

        }

        public static void SaveListToExcel<T>(List<T> list)
        {

        }
    }
}
