using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  class OneRow : I_OneRow {
    public int ColumnID { get; private set; }
    public int CorrectNumber { get; private set; }
    public I_Cargo MyCargo { get; private set; }
    public List<I_Route> MyRouteList { get; private set; }

    public OneRow() {
      this.MyCargo = new Cargo();
      this.MyRouteList = new List<I_Route>();
    }

    public void Register(List<string> one_row, List<string> columns) {
      this.ColumnID = int.Parse(one_row[1]);
      this.CorrectNumber = int.Parse(one_row[2]);

      List<string> cargo_str_list = new List<string>();
      List<string> cargo_columns = new List<string>();
      for (int i = 3; i <= 17; i++) {
        cargo_str_list.Add(one_row[i]);
        cargo_columns.Add(columns[i]);
      }
      this.MyCargo.Register(cargo_str_list, cargo_columns);

      List<string> route00_str_list = new List<string>();
      List<string> route00_columns = new List<string>();
      for (int i = 18; i <= 34; i++) {
        route00_str_list.Add(one_row[i]);
        route00_columns.Add(columns[i]);
      }
      var route00 = new Route();
      route00.Register(route00_str_list, route00_columns);
      this.MyRouteList.Add(route00);

      List<string> route01_str_list = new List<string>();
      List<string> route01_columns = new List<string>();
      for (int i = 35; i <= 51; i++) {
        route01_str_list.Add(one_row[i]);
        route01_columns.Add(columns[i]);
      }
      var route01 = new Route();
      route01.Register(route01_str_list, route01_columns);
      this.MyRouteList.Add(route01);

      List<string> route02_str_list = new List<string>();
      List<string> route02_columns = new List<string>();
      for (int i = 52; i <= 68; i++) {
        route02_str_list.Add(one_row[i]);
        route02_columns.Add(columns[i]);
      }
      var route02 = new Route();
      route02.Register(route02_str_list, route02_columns);
      this.MyRouteList.Add(route02);

      List<string> route03_str_list = new List<string>();
      List<string> route03_columns = new List<string>();
      for (int i = 69; i <= 85; i++) {
        route03_str_list.Add(one_row[i]);
        route03_columns.Add(columns[i]);
      }
      var route03 = new Route();
      route03.Register(route03_str_list, route03_columns);
      this.MyRouteList.Add(route03);

    }
  }
}
