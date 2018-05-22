using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExCsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Read CSV");

            //var enumCsv =
            //    from field in
            //        UtilityCsv.Context(@"C:\WORK\PG\C#\ExCsvParser\ExCsvParser\bin\Debug\CSV\47OKINAW.CSV",
            //        ",", Encoding.GetEncoding(932))
            //    select new string[] { field[2], field[6], field[7], field[8] };

            IEnumerable<string[]> results =
                from csvpath in
                    Directory.GetFiles("CSV_FILES", "*.csv", System.IO.SearchOption.TopDirectoryOnly)
                from fields in
                    UtilityCsv.Context(csvpath, ",", Encoding.GetEncoding(932))
                select new string[] { fields[2], fields[6], fields[7], fields[8] };

            var xx = Directory.GetFiles("CSV_FILES", "*.csv", System.IO.SearchOption.TopDirectoryOnly)
                .SelectMany(csvPath => UtilityCsv.Context(csvPath, ",", Encoding.GetEncoding(932))
                )
                .Select(fields => new string[] { fields[2], fields[6] });

            var x = UtilityCsv.Context(@"CSV_FILES\47OKINAW.CSV", ",", Encoding.GetEncoding(932))
                .Select(fields => new string[]{fields[2], fields[6]});

            UtilityCsv.WriteFileCsv(
                @"CONVERT_CSV\Converted.CSV",
                xx);

            Console.ReadKey(); 
        }
    }
}
