﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder
{
  class Route
  {
    /// <summary>
    /// 経路オブジェクトのID
    /// </summary>
    public int RouteID { get; set; }
    public int PublicRouteID { get; set; }
    public double PredictValue { get; set; }
    /// <summary>
    /// 経路オブジェクトの辞書による表現
    /// </summary>
    public Dictionary<string, double> RouteColumnDic { get; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="id">経路オブジェクトのID</param>
    public Route(int id, int public_id)
    {
      this.RouteID = id;
      this.PublicRouteID = public_id;
      this.PredictValue = 0.0;
      this.RouteColumnDic = new Dictionary<string, double>();
    }

    public Route()
    {
    }

    public Route(Route route)
    {
      this.RouteID = route.RouteID;
      this.PublicRouteID = route.PublicRouteID;
      this.PredictValue = route.PredictValue;
      this.RouteColumnDic = new Dictionary<string, double>(route.RouteColumnDic);
    }

    /// <summary>
    /// string型で表された経路データと属性データを経路オブジェクトに変換し，登録する．
    /// </summary>
    /// <param name="route_list">string型の経路データ</param>
    /// <param name="columns">string型の属性データ</param>
    public void Register(List<string> route_list, List<string> columns)
    {
      for (int i = 0; i < columns.Count; i++)
      {
        var data = route_list[i];
        if (data == "") data = "-1";
        this.RouteColumnDic.Add(columns[i], double.Parse(data));
      }
    }
  }
}
