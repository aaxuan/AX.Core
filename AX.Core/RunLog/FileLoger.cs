using System;
using System.IO;

namespace AX.Core.RunLog
{
    public class FileLoger : BaseLoger
    {
        public void WriteLog(string msg)
        {
            if (!Directory.Exists(LogPath))
            { Directory.CreateDirectory(LogPath); }

            string fileNamePath = "log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            fileNamePath = LogPath + "\\" + fileNamePath;

            FileStream fileStream;
            if (File.Exists(fileNamePath))
            { fileStream = new FileStream(fileNamePath, FileMode.Append, FileAccess.Write); }
            else
            { fileStream = new FileStream(fileNamePath, FileMode.Create, FileAccess.Write); }
            StreamWriter sw;
            sw = new StreamWriter(fileStream);
            sw.WriteLine(CreateLogMsg(msg));
            sw.Close();
            fileStream.Close();
        }

        public FileLoger(string logpath)
        {
            //Msgs = new Queue<string>();
            //if (string.IsNullOrWhiteSpace(logpath))
            //{
            //}
            //else
            //{
            //    if (Directory.Exists(logpath) == false)
            //    { Directory.CreateDirectory(logpath); }
            //    string sFileName = "log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            //}
            LogPath = logpath;
        }

        ///// <summary>
        ///// 日志队列
        ///// </summary>
        //private Queue<string> Msgs;

        /// <summary>
        /// 日志文件路径
        /// </summary>
        private string LogPath;

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
            WriteLog(msg);
        }

        public override void Err(string msg)
        {
            WriteLog(msg);
        }

        public override void Waring(string msg)
        {
            WriteLog(msg);
        }

        public override void Line()
        {
            WriteLog($"---------- ---------- ---------- ---------- ---------- ----------");
        }

        public override void EmptyLine()
        {
            WriteLog(string.Empty);
        }
    }
}