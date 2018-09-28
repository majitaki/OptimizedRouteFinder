using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
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

    private MySettings() {
      this.AnacondaPath = "C:\\ProgramData\\Anaconda3\\Scripts\\activate.bat";
      this.AnacondaEnv = "";
      //this.BasePath = Environment.CurrentDirectory;
      this.BasePath = "./";
      this.OutputFolderName = "Output";
      this.WorkingFolderName = "Working";
      this.WorkingPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName;
      this.InputCsvName = "random_honsyu.csv";
      this.InputCsvPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName + "\\" + this.InputCsvName;
      this.OutputResultCsvName = "result.csv";
      this.OutputAllResultCsvName = "result_all.csv";
      this.OutputResultCsvPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName + "\\" + this.OutputResultCsvName;
      this.OutputAllResultCsvPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName + "\\" + this.OutputAllResultCsvName;
      this.OutputFlagPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName + "\\" + "flag";
      this.PredictBranchScriptName = "predict_branch.py";
      this.PredictBranchScriptPath = this.BasePath + "\\" + this.OutputFolderName + "\\" + this.WorkingFolderName + "\\" + this.PredictBranchScriptName;

      this.CargoColumDuration = new Tuple<int, int>(3, 17);
      this.Route00_ColumDuration = new Tuple<int, int>(18, 34);
      this.Route01_ColumDuration = new Tuple<int, int>(35, 51);
      this.Route02_ColumDuration = new Tuple<int, int>(52, 68);
      this.Route03_ColumDuration = new Tuple<int, int>(69, 85);
    }
  }
}
