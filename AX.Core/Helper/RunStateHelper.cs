using System;
using System.Text;

namespace AX.Core.Helper
{
    public class RunStateHelper
    {
        public string SystemInfo()
        {
            var result = new StringBuilder();
            result.AppendLine($"当前工作目录的完全限定路径 {Environment.CurrentDirectory}");
            result.AppendLine($"当前托管线程唯一标识符 {Environment.CurrentManagedThreadId}");
            result.AppendLine($"当前操作系统是否为 64 位操作系统 {Environment.Is64BitOperatingSystem}");
            result.AppendLine($"当前进程是否为 64 位进程 {Environment.Is64BitProcess}");
            result.AppendLine($"本地计算机的 NetBIOS 名称 {Environment.MachineName}");
            result.AppendLine($"当前平台标识符和版本号 {Environment.OSVersion}");
            result.AppendLine($"当前计算机上的处理器数 {Environment.ProcessorCount}");
            result.AppendLine($"系统目录的完全限定路径 {Environment.SystemDirectory}");
            result.AppendLine($"系统启动后经过的毫秒数 {Environment.TickCount}");
            result.AppendLine($"公共语言运行时的主要版本号、次要版本号、内部版本号和修订号组成的版本 {Environment.Version}");
            result.AppendLine($"当前工作目录的完全限定路径 {Environment.CurrentDirectory}");
            return result.ToString();
        }
    }
}