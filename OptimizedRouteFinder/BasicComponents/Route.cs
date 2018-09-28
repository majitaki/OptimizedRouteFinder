using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  class Route : I_Route {
    public int RouteID { get; }
    public Dictionary<string, double> RouteColumnDic { get; }

    public Route(int id) {
      this.RouteID = id;
      this.RouteColumnDic = new Dictionary<string, double>();
    }

    public void Register(List<string> route_list, List<string> columns) {
      for (int i = 0; i < columns.Count; i++) {
        var data = route_list[i];
        if (data == "") data = "-1";
        this.RouteColumnDic.Add(columns[i], double.Parse(data));
      }
    }
  }
}
