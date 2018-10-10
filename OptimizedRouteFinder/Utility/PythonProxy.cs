using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  /// <summary>
  /// C#プログラムとPythonスクリプトの仲介をするクラス
  /// </summary>
  class PythonProxy {
    static System.Diagnostics.Process Process;
    static System.IO.StreamWriter StreamWriter;
    public static bool ErrorFlag;

    /// <summary>
    /// Pythonの環境設定をする
    /// </summary>
    public static void StartUpPython() {
      Process = new System.Diagnostics.Process();
      Process.StartInfo.UseShellExecute = false;
      Process.StartInfo.RedirectStandardInput = true;
      Process.StartInfo.RedirectStandardOutput = true;
      Process.StartInfo.RedirectStandardError = true;
      Process.OutputDataReceived += p_OutputDataReceived;
      Process.ErrorDataReceived += p_ErrorDataReceived;

      Process.StartInfo.FileName =
          System.Environment.GetEnvironmentVariable("ComSpec");
      Process.StartInfo.CreateNoWindow = true;

      Process.Start();

      Process.BeginOutputReadLine();
      Process.BeginErrorReadLine();
      StreamWriter = Process.StandardInput;
      if (StreamWriter.BaseStream.CanWrite) {
        StreamWriter.WriteLine(MySettings.GetInstance().AnacondaPath);
        StreamWriter.WriteLine(@"activate " + MySettings.GetInstance().AnacondaEnv);
      }
    }

    /// <summary>
    /// Pythonスクリプトを用いて複数対戦の評価をする
    /// </summary>
    /// <param name="path">今は指定の必要なし．将来的に動的に評価スクリプトを変化した場合に用いるとよい</param>
    /// <returns>評価が無事に終了するとtrue，失敗するとfalseを返す</returns>
    public static bool PythonPredictBranch(string path = "") {
      ErrorFlag = false;
      var work_path = MySettings.GetInstance().WorkingPath;
      if (path == "") path = MySettings.GetInstance().PredictBranchScriptName;

      if (StreamWriter.BaseStream.CanWrite) {
        StreamWriter.WriteLine(@"cd " + work_path);
        StreamWriter.WriteLine(@"python " + path);
      }

      bool exist_flag = false;
      while (!exist_flag) {
        exist_flag = File.Exists(MySettings.GetInstance().OutputFlagPath);
        System.Threading.Thread.Sleep(100);
        if (PythonProxy.ErrorFlag) {
          System.Threading.Thread.Sleep(100);
          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// pythonスクリプトの出力時のイベント処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void p_OutputDataReceived(object sender,
        System.Diagnostics.DataReceivedEventArgs e) {
      Console.WriteLine(e.Data);
    }

    /// <summary>
    /// pythonスクリプトのエラー出力時のイベント処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void p_ErrorDataReceived(object sender,
        System.Diagnostics.DataReceivedEventArgs e) {
      Console.WriteLine("ERR>{0}", e.Data);
      //ErrorFlag = true;
    }
  }
}
