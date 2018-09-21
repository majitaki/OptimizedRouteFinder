using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Interfaces {
  interface I_Route {
    int RouteID { get; }
    Dictionary<string, double> RouteColumnList { get; }
    void Register(List<string> route_list, List<string> columns);
  }
}
