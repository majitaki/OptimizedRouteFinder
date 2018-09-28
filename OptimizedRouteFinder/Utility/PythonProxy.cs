using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.Utility {
  class PythonProxy {
    static System.Diagnostics.Process Process;
    static System.IO.StreamWriter StreamWriter;
    public static bool ErrorFlag;

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


    static void p_OutputDataReceived(object sender,
        System.Diagnostics.DataReceivedEventArgs e) {
      Console.WriteLine(e.Data);
    }

    static void p_ErrorDataReceived(object sender,
        System.Diagnostics.DataReceivedEventArgs e) {
      Console.WriteLine("ERR>{0}", e.Data);
      ErrorFlag = true;
    }
  }
}
