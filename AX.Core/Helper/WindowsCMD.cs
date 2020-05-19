using System;
using System.Diagnostics;

namespace AX.Core.Helper
{
    public class CMD
    {
        public CMD()
        { }

        private Process _proc { get; set; } = new Process();

        public void Run(string cmd)
        {
            _proc.StartInfo.CreateNoWindow = true;
            _proc.StartInfo.FileName = "cmd.exe";
            _proc.StartInfo.UseShellExecute = false;
            _proc.StartInfo.RedirectStandardInput = true;
            _proc.StartInfo.RedirectStandardOutput = true;
            _proc.StartInfo.RedirectStandardError = true;

            _proc.Start();
            var cmdWriter = _proc.StandardInput;
            _proc.BeginOutputReadLine();
            if (!String.IsNullOrEmpty(cmd))
            {
                cmdWriter.WriteLine(cmd);
            }
            cmdWriter.Close();
            _proc.Close();
        }
    }
}