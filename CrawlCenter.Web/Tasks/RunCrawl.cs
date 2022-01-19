using System.Diagnostics;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.Tasks {
    // todo CrawlTask应该在构造函数里注入
    // todo 构造函数需要传入环境变量、初始化配置等参数
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