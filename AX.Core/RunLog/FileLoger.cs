using System;
using System.IO;

namespace AX.Core.RunLog
{
    public class FileLoger : BaseLoger
    {
        public void WriteFileLog(string msg)
        {
            var logPath = LogDirectoryPath + "\\" + $"log_{DateTime.Now.ToString("yyyy-mm-dd")}.log";
            StreamWriter sw = new StreamWriter(logPath, true);
            sw.WriteLine(CreateLogMsg(msg));
            sw.Close();
        }

        /// <summary>
        /// 实例化 文件日志记录
        /// 路径不传则取当前目录
        /// </summary>
        /// <param name="logDirectoryPath"></param>
        public FileLoger(string logDirectoryPath = null)
        {
            //AppDomain.CurrentDomain.BaseDirectory  D:\MyProject\AX.Core\NetCoreUseDemo\bin\Debug\netcoreapp3.1\
            if (string.IsNullOrWhiteSpace(logDirectoryPath))
            { logDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "log"; }

            if (Directory.Exists(logDirectoryPath) == false)
            { Directory.CreateDirectory(logDirectoryPath); }

            LogDirectoryPath = logDirectoryPath;
        }

        ///// <summary>
        ///// 日志队列
        ///// </summary>
        //private Queue<string> Msgs;

        /// <summary>
        /// 日志文件路径
        /// </summary>
        private string LogDirectoryPath;

        ///// <summary>
        ///// 日志写入线程的控制标记
        ///// </summary>
        //private bool State;

        ///// <summary>
        ///// 日志文件写入流对象
        ///// </summary>
        //private static StreamWriter Writer;

        ////日志文件写入线程执行的方法
        //private void work()
        //{
        //    while (true)
        //    {
        //        //判断队列中是否存在待写入的日志
        //        if (msgs.Count > 0)
        //        {
        //            Msg msg = null;
        //            lock (msgs)
        //            {
        //                msg = msgs.Dequeue();
        //            }
        //            if (msg != null)
        //            {
        //                FileWrite(msg);
        //            }
        //        }
        //        else
        //        {
        //            //判断是否已经发出终止日志并关闭的消息
        //            if (state)
        //            {
        //                Thread.Sleep(1);
        //            }
        //            else
        //            {
        //                FileClose();
        //            }
        //        }
        //    }
        //}

        ////写入日志文本到文件的方法
        //private void FileWrite(Msg msg)
        //{
        //    try
        //    {
        //        if (writer == null)
        //        {
        //            FileOpen();
        //        }
        //        else
        //        {
        //            //判断文件到期标志，如果当前文件到期则关闭当前文件创建新的日志文件
        //            if (DateTime.Now >= TimeSign)
        //            {
        //                FileClose();
        //                FileOpen();
        //            }
        //            writer.Write(msg.Datetime);
        //            writer.Write('\t');
        //            writer.Write(msg.Type);
        //            writer.Write('\t');
        //            writer.WriteLine(msg.Text);
        //            writer.Flush();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Out.Write(e);
        //    }
        //}

        ////打开文件准备写入
        //private void FileOpen()
        //{
        //    writer = new StreamWriter(path + GetFilename(), true, Encoding.UTF8);
        //}

        ////关闭打开的日志文件
        //private void FileClose()
        //{
        //    if (writer != null)
        //    {
        //        writer.Flush();
        //        writer.Close();
        //        writer.Dispose();
        //        writer = null;
        //    }
        //}

        public override void Info(string msg)
        {
            WriteFileLog("[信息] " + msg);
        }

        public override void Err(string msg)
        {
            WriteFileLog("[异常] " + msg);
        }

        public override void Waring(string msg)
        {
            WriteFileLog("[警告] " + msg);
        }

        public override void Line()
        {
            WriteFileLog($"---------- ---------- ---------- ---------- ---------- ----------");
        }

        public override void EmptyLine()
        {
            WriteFileLog(string.Empty);
        }
    }
}