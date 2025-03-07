using System;
using System.IO;

namespace Services
{
    public class LogFile
    {
        public static void WriteLog(string strlog, string fileName, string filePath = "Warning")
        {
            string strFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log\\{filePath}";
            string strFileName = $"{DateTime.Now:yyyyMMdd}{fileName}.log";
            strFileName = strFilePath + "\\" + strFileName;
            if (!Directory.Exists(strFilePath)) Directory.CreateDirectory(strFilePath);

            FileStream fs;
            StreamWriter sw;
            if (File.Exists(strFileName))
                fs = new FileStream(strFileName, FileMode.Append, FileAccess.Write);
            else
                fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "  " + strlog);
            sw.Close();
            fs.Close();
        }

        public static string[] ReadLog(string fileName, string filePath = "Warning")
        {
            string strFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log\\{filePath}";
            string strFileName = fileName + ".log";
            string path = strFilePath + "\\" + strFileName;

            if (!Directory.Exists(strFilePath)) Directory.CreateDirectory(strFilePath);
            if (File.Exists(path))
                return File.ReadAllLines(path);
            else
                return new string[1] { "没有数据" };
        }
    }

    public enum LogType
    {
        Error, Warning, Modification, Monitor
    }

    public class LogManager
    {
        public static void WriteLog(string strlog, LogType logType)
        {
            switch (logType)
            {
                case LogType.Error:
                    LogFile.WriteLog(strlog, "错误记录", "Error");
                    break;
                case LogType.Warning:
                    LogFile.WriteLog(strlog, "报警记录", "Warning");
                    break;
                case LogType.Modification:
                    LogFile.WriteLog(strlog, "更改记录", "Modification");
                    break;
                case LogType.Monitor:
                    LogFile.WriteLog(strlog, "监视记录", "Monitor");
                    break;
            }
        }
    }
}