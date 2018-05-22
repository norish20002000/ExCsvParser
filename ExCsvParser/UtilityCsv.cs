using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Data;
using System.Collections;

namespace ExCsvParser
{
    public static class UtilityCsv
    {
        public static IEnumerable<string[]> Context(
            string path, string separator = ",", Encoding encoding = null)
        {
            using (Stream stream =
                new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (TextFieldParser parser =
                new TextFieldParser(stream, encoding ?? Encoding.UTF8, true, false))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.Delimiters = new[] { separator };
                parser.HasFieldsEnclosedInQuotes = true;
                parser.TrimWhiteSpace = true;
                while (parser.EndOfData == false)
                {
                    string[] fields = parser.ReadFields();
                    yield return fields;
                }
            }
        }


        public static DataTable ConvertToDataTable(IEnumerable<string[]> context)
        {
            DataTable dataTable = new DataTable();

            foreach (string[] line in context)
            {
                int clumnNo = 0;
                DataRow row = dataTable.NewRow();
                foreach (string column in line)
                {
                    row[clumnNo] = column;
                    clumnNo++;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        public static DataSet MakeDataSet(IEnumerable<DataTable> enumDataTable)
        {
            DataSet dataSet = new DataSet();
            foreach (var dt in enumDataTable)
            {
                dataSet.Tables.Add(dt);
            }

            return dataSet;
        }

        public static void WriteFileCsv(string path, IEnumerable<string[]> context)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(932)))
            {
                foreach (var line in context)
                {
                    string csvLine = string.Join(",", line);
                    sw.WriteLine(csvLine);
                }
            }
        }
    }
}
