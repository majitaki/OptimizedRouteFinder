using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder
{
  /// <summary>
  /// このプログラムのメインクラス．
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      PythonProxy.StartUpPython();

      //トーナメントに使うデータを読み込む
      var group = InOutput.ReadRaw(MySettings.GetInstance().InputLearningCsvPath).GroupBy(line => new { start = line.MyRouteList[0].RouteColumnDic["a0"], goal = line.MyRouteList[0].RouteColumnDic["g0"] });
      var group_list = group.OrderByDescending(g => g.Count()).ToList();
      var tournament_data = group.ToList().OrderByDescending(g => g.Count()).FirstOrDefault().ToList();

      foreach (var group_data in group_list)
      {
        Console.WriteLine(group_data.Key);
        Console.WriteLine(group_data.Count());
        new Rapid_Experiment().Execute(group_data.ToList());
      }

      //new Simple_Experiment().Execute();
      //new Rapid_Experiment().Execute(tournament_data);

    }
  }
}
