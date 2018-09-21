using OptimizedRouteFinder.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder {
  class Program {
    static void Main(string[] args) {
      Settings settings = Settings.GetInstance();

      var all_data = Input.ReadRaw(settings.InputCsvPath);
    }
  }
}
