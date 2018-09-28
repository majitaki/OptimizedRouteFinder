using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Interfaces {
  interface I_Cargo {
    int CargoID { get;}
    Dictionary<string, double> CargoColumnDic { get; }
    void Register(List<string> cargo_list, List<string> columns);
  }
}
