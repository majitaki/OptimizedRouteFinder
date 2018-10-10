using OptimizedRouteFinder.BasicComponents;
using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  /// <summary>
  /// 入出力に関するクラス
  /// </summary>
  static class InOutput {

    /// <summary>
    /// csvの学習データを読み込む
    /// </summary>
    /// <param name="csv_path">学習データのパス名</param>
    /// <returns>CargoAndRoutes型に変換されたデータを返す</returns>
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

    /// <summary>
    /// CargoAndRoute型のデータをcsvファイルに書き込む
    /// </summary>
    /// <param name="car_list">CargoAndRoute型のリスト</param>
    public static void WriteRaw(List<I_CargoAndRoutes> car_list) {
      var filepath = MySettings.GetInstance().OutputPredictCsvPath;
      List<string> column_list = new List<string>();
      File.Delete(filepath);
      System.Threading.Thread.Sleep(100);

      column_list.Add("random");
      column_list.Add("number");
      column_list.Add("cor_number");
      foreach (var cargo_column in car_list.First().MyCargo.CargoColumnDic.Keys) {
        column_list.Add(cargo_column);
      }

      foreach (var route in car_list.First().MyRouteList) {
        foreach (var route_column in route.RouteColumnDic.Keys) {
          column_list.Add(route_column);
        }
      }

      string sp = string.Empty;
      using (StreamWriter sw = new StreamWriter(filepath, false, Encoding.UTF8)) {

        foreach (var column in column_list) {
          sw.Write(sp + column);
          sp = ",";
        }
        sw.WriteLine();

        foreach (var car in car_list) {
          var cargo_values = car.MyCargo.CargoColumnDic.Values;
          sp = string.Empty;

          for (int i = 0; i < 3; i++) {
            sw.Write(sp);
            sp = ",";
          }

          foreach (var cargo_value in cargo_values) {
            sw.Write(sp + cargo_value);
            sp = ",";
          }

          foreach (var route in car.MyRouteList) {
            foreach (var route_value in route.RouteColumnDic.Values) {
              sw.Write(sp + route_value);
              sp = ",";
            }
          }
          sw.WriteLine();
        }
      }
    }

    /// <summary>
    /// Pythonスクリプトの出力結果を読み込む
    /// </summary>
    /// <returns>string[]のリストで出力結果を返す</returns>
    public static List<string[]> GetResultList() {
      string result_filePath = MySettings.GetInstance().OutputResultCsvPath;

      var raw_result_list = new List<string[]>();

      for (int count = 0; count <= 5; count++) {
        try {
          using (var sr = new StreamReader(result_filePath, System.Text.Encoding.GetEncoding("utf-8"))) {
            sr.ReadLine().Split(',');
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

    /// <summary>
    /// GetResultListで読み込んだPythonスクリプトの出力結果をint型に変換する
    /// </summary>
    /// <param name="raw_result_list">int型のリストを返す</param>
    /// <returns></returns>
    public static List<int> ConvertRouteIDResults(List<string[]> raw_result_list) {
      var result_list = new List<int>();

      foreach (var result in raw_result_list) {
        result_list.Add(int.Parse(result[1]));
      }
      return result_list;
    }

    public static List<double> ConvertRouteValueResults(List<string[]> raw_result_list) {
      var result_list = new List<double>();

      foreach (var result in raw_result_list) {
        result_list.Add(double.Parse(result[2]));
      }
      return result_list;
    }


    /// <summary>
    /// 過去のPythonスクリプトの出力結果を消去する
    /// </summary>
    /// <returns>消去に成功するとtrueを返す</returns>
    public static bool DeltaResults() {
      var result_path = MySettings.GetInstance().OutputResultCsvPath;
      var all_result_path = MySettings.GetInstance().OutputAllResultCsvPath;
      var flag_path = MySettings.GetInstance().OutputFlagPath;


      File.Delete(result_path);
      File.Delete(flag_path);
      System.Threading.Thread.Sleep(100);
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
