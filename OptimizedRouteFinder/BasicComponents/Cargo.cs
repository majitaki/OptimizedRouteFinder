using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  class Cargo : I_Cargo {
    public int CargoID { get; }
    public Dictionary<string, double> CargoColumnDic { get; }

    public Cargo() {
      this.CargoColumnDic = new Dictionary<string, double>();
    }

    public void Register(List<string> cargo_list, List<string> columns) {
      for (int i = 0; i < columns.Count; i++) {
        var data = cargo_list[i];
        if (data == "") data = "-1";
        this.CargoColumnDic.Add(columns[i], double.Parse(data));
      }
    }
  }
}
