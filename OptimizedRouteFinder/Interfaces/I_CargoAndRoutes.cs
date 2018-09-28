using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Interfaces {
  interface I_CargoAndRoutes {
    bool IsRaw { get; }
    int ColumnID { get; }
    int CorrectNumber { get; }
    I_Cargo MyCargo { get; }
    List<I_Route> MyRouteList { get; }

    void Register(List<string> one_row, List<string> column);
    void Register(I_Cargo cargo, List<I_Route> route_list);
  }
}
