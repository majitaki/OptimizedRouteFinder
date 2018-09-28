using OptimizedRouteFinder.Interfaces;
using OptimizedRouteFinder.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  class CargoAndRoutes : I_CargoAndRoutes {
    public bool IsRaw { get; }

    int _colum_id;
    public int ColumnID {
      get {
        if (this.IsRaw) return _colum_id;
        return -1;
      }
      private set {
        _colum_id = value;
      }
    }

    int _cor_num;
    public int CorrectNumber {
      get {
        if (this.IsRaw) return _cor_num;
        return -1;
      }
      private set {
        _cor_num = value;
      }
    }

    public I_Cargo MyCargo { get; private set; }
    public List<I_Route> MyRouteList { get; private set; }

    public CargoAndRoutes(bool is_raw) {
      this.IsRaw = is_raw;
      this.MyCargo = new Cargo();
      this.MyRouteList = new List<I_Route>();
    }

    public void Register(List<string> one_row, List<string> columns) {
      var setting = MySettings.GetInstance();
      this.ColumnID = int.Parse(one_row[1]);
      this.CorrectNumber = int.Parse(one_row[2]);

      List<string> cargo_str_list = new List<string>();
      List<string> cargo_columns = new List<string>();
      for (int i = setting.CargoColumDuration.Item1; i <= setting.CargoColumDuration.Item2; i++) {
        cargo_str_list.Add(one_row[i]);
        cargo_columns.Add(columns[i]);
      }
      this.MyCargo.Register(cargo_str_list, cargo_columns);

      List<string> route00_str_list = new List<string>();
      List<string> route00_columns = new List<string>();
      for (int i = setting.Route00_ColumDuration.Item1; i <= setting.Route00_ColumDuration.Item2; i++) {
        route00_str_list.Add(one_row[i]);
        route00_columns.Add(columns[i]);
      }
      var route00 = new Route(0);
      route00.Register(route00_str_list, route00_columns);
      this.MyRouteList.Add(route00);

      List<string> route01_str_list = new List<string>();
      List<string> route01_columns = new List<string>();
      for (int i = setting.Route01_ColumDuration.Item1; i <= setting.Route01_ColumDuration.Item2; i++) {
        route01_str_list.Add(one_row[i]);
        route01_columns.Add(columns[i]);
      }
      var route01 = new Route(1);
      route01.Register(route01_str_list, route01_columns);
      this.MyRouteList.Add(route01);

      List<string> route02_str_list = new List<string>();
      List<string> route02_columns = new List<string>();
      for (int i = setting.Route02_ColumDuration.Item1; i <= setting.Route02_ColumDuration.Item2; i++) {
        route02_str_list.Add(one_row[i]);
        route02_columns.Add(columns[i]);
      }
      var route02 = new Route(2);
      route02.Register(route02_str_list, route02_columns);
      this.MyRouteList.Add(route02);

      List<string> route03_str_list = new List<string>();
      List<string> route03_columns = new List<string>();
      for (int i = setting.Route03_ColumDuration.Item1; i <= setting.Route03_ColumDuration.Item2; i++) {
        route03_str_list.Add(one_row[i]);
        route03_columns.Add(columns[i]);
      }
      var route03 = new Route(3);
      route03.Register(route03_str_list, route03_columns);
      this.MyRouteList.Add(route03);

    }

    public void Register(I_Cargo cargo, List<I_Route> route_list) {
      this.MyCargo = cargo;
      this.MyRouteList = route_list;
    }
  }
}
