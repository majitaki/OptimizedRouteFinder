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

      //貨物
       var cargo = tournament_data[0].MyCargo;

      //トーナメント1試合目
      //経路4つ
      var route10 = tournament_data[0].MyRouteList[0];
      var route11 = tournament_data[0].MyRouteList[1];
      var route12 = tournament_data[0].MyRouteList[2];
      var route13 = tournament_data[0].MyRouteList[3];
      //経路をまとめる
      var route_list_1 = new List<Route>();
      route_list_1.Add(route10);
      route_list_1.Add(route11);
      route_list_1.Add(route12);
      route_list_1.Add(route13);
      //貨物と経路をまとめる
      var cargo_and_route_1 = new CargoAndRoutes(false);
      cargo_and_route_1.Register(cargo, route_list_1);
      //試合をする
      var win_route_1 = Predict.PredictBranchs(cargo_and_route_1);


      //トーナメント2試合目
      //経路4つ
      var route20 = tournament_data[1].MyRouteList[0];
      var route21 = tournament_data[1].MyRouteList[1];
      var route22 = tournament_data[1].MyRouteList[2];
      var route23 = tournament_data[1].MyRouteList[3];
      //経路をまとめる
      var route_list_2 = new List<Route>();
      route_list_2.Add(route20);
      route_list_2.Add(route21);
      route_list_2.Add(route22);
      route_list_2.Add(route23);
      //貨物と経路をまとめる
      var cargo_and_route_2 = new CargoAndRoutes(false);
      cargo_and_route_2.Register(cargo, route_list_2);
      //試合をする
      var win_route_2 = Predict.PredictBranchs(cargo_and_route_2);


      //トーナメント3試合目
      //経路4つ
      var route30 = tournament_data[2].MyRouteList[0];
      var route31 = tournament_data[2].MyRouteList[1];
      var route32 = tournament_data[2].MyRouteList[2];
      var route33 = tournament_data[2].MyRouteList[3];
      //経路をまとめる
      var route_list_3 = new List<Route>();
      route_list_3.Add(route30);
      route_list_3.Add(route31);
      route_list_3.Add(route32);
      route_list_3.Add(route33);
      //貨物と経路をまとめる
      var cargo_and_route_3 = new CargoAndRoutes(false);
      cargo_and_route_3.Register(cargo, route_list_3);
      //試合をする
      var win_route_3 = Predict.PredictBranchs(cargo_and_route_3);


      //トーナメント4試合目
      //経路4つ
      var route40 = tournament_data[3].MyRouteList[0];
      var route41 = tournament_data[3].MyRouteList[1];
      var route42 = tournament_data[3].MyRouteList[2];
      var route43 = tournament_data[3].MyRouteList[3];
      //経路をまとめる
      var route_list_4 = new List<Route>();
      route_list_4.Add(route40);
      route_list_4.Add(route41);
      route_list_4.Add(route42);
      route_list_4.Add(route43);
      //貨物と経路をまとめる
      var cargo_and_route_4 = new CargoAndRoutes(false);
      cargo_and_route_4.Register(cargo, route_list_4);
      //試合をする
      var win_route_4 = Predict.PredictBranchs(cargo_and_route_4);

      //トーナメント最終試合
      //4試合の勝利経路をまとめる
      List<Route> win_route_list = new List<Route>();
      win_route_list.AddRange(win_route_1);
      win_route_list.AddRange(win_route_2);
      win_route_list.AddRange(win_route_3);
      win_route_list.AddRange(win_route_4);
      //貨物と上の勝利経路4つをまとめる
      CargoAndRoutes car = new CargoAndRoutes(false);
      car.Register(cargo, win_route_list);
      //試合をする
      var final_win_route = Predict.PredictBranchs(car);
    }
  }
}
