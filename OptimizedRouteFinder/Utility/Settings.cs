using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  /// <summary>
  /// プログラムの設定に関するクラス．シングルトンの仕組みが実装されているため，常に同じ設定内容を取得できる．staticとして設計しなかった理由は継承で変更した設定を拡張によって構成できるようにするため．
  /// </summary>
  public sealed class MySettings {
    private static MySettings SingletonSettings = new MySettings();

    //anaconda
    public string AnacondaPath { get; private set; }
    public string AnacondaEnv { get; private set; }
    //base
    public string BasePath { get; private set; }
    //output folder
    public string OutputFolderName { get; private set; }
    public string WorkingFolderName { get; private set; }
    public string WorkingPath { get; private set; }
    //input file
    public string InputCsvName { get; private set; }
    public string InputCsvPath { get; private set; }
    //output file
    public string OutputResultCsvName { get; private set; }
    public string OutputAllResultCsvName { get; private set; }
    public string OutputResultCsvPath { get; private set; }
    public string OutputAllResultCsvPath { get; private set; }
    public string OutputFlagPath { get; private set; }
    public string OutputPredictCsvName { get; private set; }
    public string OutputPredictCsvPath { get; private set; }
    //script
    public string PredictBranchScriptName { get; private set; }
    public string PredictBranchScriptPath { get; private set; }

    //column
    public Tuple<int, int> CargoColumDuration { get; private set; }
    public Tuple<int, int> Route00_ColumDuration { get; private set; }
    public Tuple<int, int> Route01_ColumDuration { get; private set; }
    public Tuple<int, int> Route02_ColumDuration { get; private set; }
    public Tuple<int, int> Route03_ColumDuration { get; private set; }

    public static MySettings GetInstance() {
      return SingletonSettings;
    }

    /// <summary>
    /// プログラムの設定項目
    /// 入出力ファイルやフォルダの設定など
    /// </summary>
    private MySettings() {
      this.AnacondaPath = "C:\\ProgramData\\Anaconda3\\Scripts\\activate.bat";
      this.AnacondaEnv = "";
      //this.BasePath = Environment.CurrentDirectory;
      this.BasePath = "./";
      this.OutputFolderName = "Output";

      this.WorkingFolderName = "Working";
      this.WorkingPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName;

      this.InputCsvName = "random_honsyu.csv";
      this.InputCsvPath = this.WorkingPath + "\\" + "data" + "\\" + this.InputCsvName;

      this.OutputResultCsvName = "result.csv";
      this.OutputAllResultCsvName = "result_all.csv";
      this.OutputResultCsvPath = this.WorkingPath + "\\" + this.OutputResultCsvName;
      this.OutputAllResultCsvPath = this.WorkingPath + "\\" + this.OutputAllResultCsvName;
      this.OutputFlagPath = this.WorkingPath + "\\" + "flag";
      this.OutputPredictCsvName = "sample.csv";
      this.OutputPredictCsvPath = this.WorkingPath + "\\" + this.OutputPredictCsvName;

      this.PredictBranchScriptName = "predict_branch.py";
      this.PredictBranchScriptPath = this.WorkingPath + "\\" + this.PredictBranchScriptName;

      this.CargoColumDuration = new Tuple<int, int>(3, 17);
      this.Route00_ColumDuration = new Tuple<int, int>(18, 34);
      this.Route01_ColumDuration = new Tuple<int, int>(35, 51);
      this.Route02_ColumDuration = new Tuple<int, int>(52, 68);
      this.Route03_ColumDuration = new Tuple<int, int>(69, 85);
    }
  }
}
