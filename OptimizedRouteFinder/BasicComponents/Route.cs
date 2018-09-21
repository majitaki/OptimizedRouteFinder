using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  class Route : I_Route {
    public int RouteID { get; }
    public Dictionary<string, double> RouteColumnList { get; }

    public Route() {
      this.RouteColumnList = new Dictionary<string, double>();
    }

    public void Register(List<string> route_list, List<string> columns) {
      for (int i = 0; i < columns.Count; i++) {
        this.RouteColumnList.Add(columns[i], double.Parse(route_list[i]));
      }
    }
  }
}
