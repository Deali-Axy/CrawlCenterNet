using System.Diagnostics;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.Tasks {
    public class RunCrawl {
        public void Run(CrawlTask task) {
            var procInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = $"/c \"{task.Cmd}\"",
                WorkingDirectory = task.CodeDir
            };
            using var proc= Process.Start(procInfo);
        }
    }
}