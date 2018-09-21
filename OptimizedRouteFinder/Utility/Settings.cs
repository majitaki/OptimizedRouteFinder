using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  public sealed class Settings {
    private static Settings SingletonSettings = new Settings();

    public string BasePath { get; private set; }
    public string InputFolderName { get; private set; }
    public string InputCsvName { get; private set; }
    public string InputCsvPath { get; private set; }
    //public List<string> OtherColumnList { get; private set; }
    //public List<string> CargoColumnList { get; private set; }
    //public List<string> Route00_ColumnList { get; private set; }
    //public List<string> Route01_ColumnList { get; private set; }
    //public List<string> Route02_ColumnList { get; private set; }
    //public List<string> Route03_ColumnList { get; private set; }

    public static Settings GetInstance() {
      return SingletonSettings;
    }

    private Settings() {
      this.DoInputSettings();
      this.DoColumnsSettings();
    }

    void DoInputSettings() {
      this.BasePath = Environment.CurrentDirectory;
      this.InputFolderName = "Input";
      this.InputCsvName = "random_honsyu.csv";
      this.SafeCreateDirectory(this.BasePath + "\\" + this.InputFolderName);
      this.InputCsvPath = this.BasePath + "\\" + this.InputFolderName + "\\" + this.InputCsvName;
    }

    void DoColumnsSettings() {

      //this.CargoColumnList = new List<string> {
      //  "A","B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O"
      //};

      //this.Route00_ColumnList = new List<string> {
      //  "a0","b0", "c0", "d0", "e0", "f0", "g0", "h0", "i0", "j0", "k0", "l0", "m0", "n0", "o0", "p0", "q0"
      //};

      //this.Route01_ColumnList = new List<string> {
      //  "a1","b1", "c1", "d1", "e1", "f1", "g1", "h1", "i1", "j1", "k1", "l1", "m1", "n1", "o1", "p1", "q1"
      //};

      //this.Route02_ColumnList = new List<string> {
      //  "a2","b2", "c2", "d2", "e2", "f2", "g2", "h2", "i2", "j2", "k2", "l2", "m2", "n2", "o2", "p2", "q2"
      //};

      //this.Route03_ColumnList = new List<string> {
      //  "a3","b3", "c3", "d3", "e3", "f3", "g3", "h3", "i3", "j3", "k3", "l3", "m3", "n3", "o3", "p3", "q3"
      //};

    }

    void SafeCreateDirectory(string path) {
      if (Directory.Exists(path)) {
        return;
      }
      Directory.CreateDirectory(path);
    }
  }
}
