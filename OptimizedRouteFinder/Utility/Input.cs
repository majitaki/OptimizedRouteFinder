using OptimizedRouteFinder.BasicComponents;
using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  static class Input {
    public static List<I_OneRow> ReadRaw(string csv_path) {
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

      var all_data = new List<I_OneRow>();
      foreach (var row_str in row_str_list) {
        var one_row = new OneRow();
        one_row.Register(row_str, column_list);
        all_data.Add(one_row);

      }

      return all_data;
    }

  }
}
