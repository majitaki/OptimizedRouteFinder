using OptimizedRouteFinder.BasicComponents;
using OptimizedRouteFinder.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder {
  /// <summary>
  /// このプログラムのメインクラス．
  /// </summary>
  class Program {
    static void Main(string[] args) {
      PythonProxy.StartUpPython();

      //トーナメントに使うデータを読み込む
      var tournament_data = InOutput.ReadRaw(MySettings.GetInstance().InputTournamentCsvPath);

      var cargo = tournament_data[0].MyCargo;
      var win_route_1 = Predict.PredictBranchs(tournament_data[0]);
      var win_route_2 = Predict.PredictBranchs(tournament_data[1]);
      var win_route_3 = Predict.PredictBranchs(tournament_data[2]);
      var win_route_4 = Predict.PredictBranchs(tournament_data[3]);

      List<Route> win_route_list = new List<Route>();
      win_route_list.AddRange(win_route_1);
      win_route_list.AddRange(win_route_2);
      win_route_list.AddRange(win_route_3);
      win_route_list.AddRange(win_route_4);

      CargoAndRoutes car = new CargoAndRoutes(false);
      car.Register(cargo, win_route_list);
      

    }
  }
}
