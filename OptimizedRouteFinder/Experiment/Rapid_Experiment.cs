using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder
{
  class Rapid_Experiment
  {
    /// <summary>
    /// 対戦は1回ずつやる必要は実はなく，複数対戦をまとめて実行することが出来る．出力時間を節約して最終勝利するルートを求める．
    /// </summary>
    public void Execute(List<CargoAndRoutes> tournament_data)
    {
      //貨物
      var cargo = tournament_data[0].MyCargo;

      //勝利ルート
      var final_win_route = new Route();
      //乱数
      var random = new System.Random();

      //トーナメント開始
      while (tournament_data.Count >= 1)
      {
        //複数対戦
        var win_routes = Predict.PredictBranchs(tournament_data);
        tournament_data.Clear();
        if (win_routes.Count == 1)
        {
          final_win_route = win_routes[0];
          break;
        }

        //ダミーをwin_routesの中からランダムに選んだルートをコピーすることで作る
        if (win_routes.Count % 4 != 0)
        {
          int short_count = (4 - win_routes.Count % 4);
          while (short_count > 0)
          {
            var copy_route = new Route(win_routes[random.Next() % win_routes.Count]);
            win_routes.Add(copy_route);
            short_count--;
          }
        }

        int count = 0;
        for (int i = 0; i < win_routes.Count / 4; i++)
        {
          var cargo_and_route = new CargoAndRoutes(false);
          var routes = new List<Route>();
          for (int j = 0; j < 4; j++)
          {
            routes.Add(win_routes[count]);
            count++;
          }
          cargo_and_route.Register(cargo, routes);
          tournament_data.Add(cargo_and_route);
        }

        Console.WriteLine($"{tournament_data.Count}");
      }

      Console.WriteLine($"{final_win_route.PublicRouteID} です");
      Console.WriteLine($"最終勝利ルートは {final_win_route.PublicRouteID} です");
      Console.WriteLine($"最終勝利ルートの正解率 {final_win_route.PredictValue} です");
      Console.ReadLine();
    }

  }
}
