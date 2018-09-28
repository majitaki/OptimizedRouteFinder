using OptimizedRouteFinder.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder {
  class Program {
    static void Main(string[] args) {
      PythonProxy.StartUpPython();

      var all_data = InOutput.ReadRaw(MySettings.GetInstance().InputCsvPath);
      var sample = all_data.Where(data => data.MyCargo.CargoColumnDic["A"] == 10017).ToList();
      var result = Predict.PredictBranchs(sample);
    }
  }
}
