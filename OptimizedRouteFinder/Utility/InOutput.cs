using OptimizedRouteFinder.BasicComponents;
using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  static class InOutput {
    public static List<I_CargoAndRoutes> ReadRaw(string csv_path) {
      var row_str_list = new List<List<string>>();
      var column_list = new List<string>();

      for (int count = 0; count < 5; count++) {
        try {
          using (var sr = new StreamReader(csv_path, System.Text.Encoding.GetEncoding("utf-8"))) {
            column_list.AddRange(sr.ReadLine().Split(','));
            while (sr.EndOfStream == false) {
              var str_list = new List<string>();
              str_list.AddRange(sr.ReadLine().Split(','));
              row_str_list.Add(str_list);
            }
            break;
          }
        } catch {
          System.Threading.Thread.Sleep(100);
        }
        return null;
      }

      var all_data = new List<I_CargoAndRoutes>();
      foreach (var row_str in row_str_list) {
        var one_row = new CargoAndRoutes(true);
        one_row.Register(row_str, column_list);
        all_data.Add(one_row);

      }

      return all_data;
    }

    public static void WriteRaw(List<I_CargoAndRoutes> car) {
      string sp = string.Empty;
      var filepath = MySettings.GetInstance().OutputResultCsvPath;

      using (StreamWriter sw = new StreamWriter(filepath, false, Encoding.UTF8)) {

        foreach (DataColumn col in dt.Columns) {
          sw.Write(sp + col.ToString());
          sp = ",";
        }
        sw.WriteLine();

        foreach (DataRow row in dt.Rows) {
          sp = string.Empty;
          for (int i = 0; i < dt.Columns.Count; i++) {
            sw.Write(sp + row[i].ToString());
            sp = ",";

          }
          sw.WriteLine();
        }
      }
    }

    public static List<string[]> GetResultList() {
      string result_filePath = MySettings.GetInstance().OutputResultCsvPath;

      var raw_result_list = new List<string[]>();

      for (int count = 0; count <= 5; count++) {
        try {
          using (var sr = new StreamReader(result_filePath, System.Text.Encoding.GetEncoding("utf-8"))) {
            //sr.ReadLine().Split(',');
            while (sr.EndOfStream == false) {
              raw_result_list.Add(sr.ReadLine().Split(','));
            }
            Console.WriteLine("ok Success Get RawResultList");
            return raw_result_list;
          }
        } catch {
          System.Threading.Thread.Sleep(100);
        }
      }
      Console.WriteLine("no Failure Get RawResultList");
      return null;
    }


    public static List<int> ConvertResults(List<string[]> raw_result_list) {
      var result_list = new List<int>();

      foreach (var result in raw_result_list) {
        result_list.Add(int.Parse(result[1]));
      }
      return result_list;
    }

    public static bool DeltaResults() {
      var result_path = MySettings.GetInstance().OutputResultCsvPath;
      var all_result_path = MySettings.GetInstance().OutputAllResultCsvPath;
      var flag_path = MySettings.GetInstance().OutputFlagPath;


      File.Delete(result_path);
      File.Delete(flag_path);
      return true;
      //string[] filePaths = Directory.GetFiles(result_path);





      // try {
      //   foreach (string filePath in filePaths) {
      //     File.SetAttributes(filePath, FileAttributes.Normal);
      //     File.Delete(filePath);
      //   }
      //   Console.WriteLine("ok Delete Output Result");
      //   return true;
      // } catch (Exception) {
      //   Console.WriteLine("no Failure Delete Output Result");
      //   return false;
      // }
    }
  }
}
