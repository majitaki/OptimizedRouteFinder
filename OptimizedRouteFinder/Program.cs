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

      var all_data = InOutput.ReadRaw(MySettings.GetInstance().InputCsvPath);
      var cargo_10017 = all_data.Where(data => data.MyCargo.CargoColumnDic["A"] == 10044).ToList();
      var win_route_list = Predict.PredictBranchs(cargo_10017);
    }
  }
}
