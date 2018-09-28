using OptimizedRouteFinder.BasicComponents;
using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  /// <summary>
  /// Pythonによる評価に関するクラス
  /// </summary>
  static class Predict {

    /// <summary>
    /// 複数対戦(トーナメントの枝群)をPythonスクリプトで評価する
    /// </summary>
    /// <param name="car_list">複数の対戦データ</param>
    /// <returns>対戦の結果，勝利したルートを入力した対戦順にリストにして返す</returns>
    public static List<I_Route> PredictBranchs(List<I_CargoAndRoutes> car_list) {

      InOutput.WriteRaw(car_list);

      var state = 0;
      switch (state) {
        case 0:
          Console.WriteLine("-----");
          Console.WriteLine("ok Start Predict");
          var delete_success = InOutput.DeltaResults();
          if (!delete_success) goto default;

          var python_success = PythonProxy.PythonPredictBranch();
          if (!python_success) goto default;

          var raw_result_list = InOutput.GetResultList();
          if (raw_result_list == null) goto default;

          Console.WriteLine("ok Load Raw Result");
          var result_list = InOutput.ConvertResults(raw_result_list);
          if (result_list == null) goto default;

          Console.WriteLine("ok Success Predict Branchs");

          var route_list = new List<I_Route>();
          for (int i = 0; i < result_list.Count; i++) {
            int predicted_route_id = result_list[i];
            route_list.Add(car_list[i].MyRouteList.First(route => route.RouteID == predicted_route_id));
          }

          return route_list;
        default:
          Console.WriteLine("no Failure Predict Branchs");
          return null;
      }

    }

  }
}
