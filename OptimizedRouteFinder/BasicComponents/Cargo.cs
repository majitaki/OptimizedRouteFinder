using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  /// <summary>
  /// 貨物のオブジェクト
  /// </summary>
  class Cargo {
    /// <summary>
    /// 貨物オブジェクトのID．今はCargoAndRoutesの中に貨物は一つしかないないため，用いられていない．
    /// </summary>
    public int CargoID { get; }

    /// <summary>
    /// 貨物オブジェクトの辞書型による表現．Keyが貨物の属性，Valueがその属性のdouble型の値
    /// </summary>
    public Dictionary<string, double> CargoColumnDic { get; }

    public Cargo() {
      this.CargoColumnDic = new Dictionary<string, double>();
    }

    /// <summary>
    /// string型の貨物データを読み込んで，オブジェクトに変換する．
    /// </summary>
    /// <param name="cargo_list">string型の貨物データ</param>
    /// <param name="columns">貨物の属性データ</param>
    public void Register(List<string> cargo_list, List<string> columns) {
      for (int i = 0; i < columns.Count; i++) {
        var data = cargo_list[i];
        if (data == "") data = "-1";
        this.CargoColumnDic.Add(columns[i], double.Parse(data));
      }
    }
  }
}
