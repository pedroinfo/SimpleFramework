using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SimpleFramework.Utils.Excel
{
    public static class ExcelHelper
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataTable">Your DataTable</param>
        /// <param name="fileName">File Location</param>
        public static void SaveDataTableToExcel(DataTable dataTable, string fileName)
        {
            var lines = new List<string>();

            string[] columnNames = dataTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);

            var valueLines = dataTable.AsEnumerable()
                .Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);

            File.WriteAllLines(fileName, lines);
        }


        //todo: implement this
        public static DataTable GetExcelToDataTable(string fileName)
        {
            return null;
        }

       
    }
}
