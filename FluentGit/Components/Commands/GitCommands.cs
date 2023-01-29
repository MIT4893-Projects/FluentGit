using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGit.Components.Commands
{
    public static class GitCommands
    {
        private static void ConfigureProcess(Process process, string arguments)
        {
            process.StartInfo.FileName = "git";
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
        }

        private static int? TryStartProcess(Process process)
        {
            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                DialogDisplayer.ShowMessage("Git is not installed or added to PATH.", "Error");
                return null;
            }
            return process.ExitCode;
        }

        private static int? StartGitProcess(string arguments)
        {
            Process process = new();
            ConfigureProcess(process, arguments);
            return TryStartProcess(process);
        }

        public static int? Init(string dir)
        {
            return StartGitProcess($"init {dir}");
        }
    }
}
