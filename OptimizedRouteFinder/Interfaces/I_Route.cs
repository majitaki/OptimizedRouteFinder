using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Interfaces {
  /// <summary>
  /// 経路クラスのインターフェース
  /// </summary>
  interface I_Route {
    int RouteID { get; }
    Dictionary<string, double> RouteColumnDic { get; }
    void Register(List<string> route_list, List<string> columns);
  }
}
