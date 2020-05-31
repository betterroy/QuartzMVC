using System;
using System.Configuration;
using System.IO;

namespace Tool.Log
{
    public static class LogTool
    {
        /// <summary>
        /// 日志方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="folderCrationType"></param>
        /// <param name="content"></param>
        /// <param name="isErasable"></param>
        /// <param name="filename"></param>
        public static void DetailLogRecord(string type, FolderCreationType folderCrationType, string content, bool isErasable, string filename = null)
        {
            string folderPrefixPath = (ConfigurationManager.AppSettings["localLogPath"] ?? "c:\\test_log_tem") + "\\" + type;
            string folderPath;
            switch (folderCrationType)
            {
                default: folderPath = folderPrefixPath; break;
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = folderPath + "\\" + (filename ?? DateTime.Now.ToString("yyyyMMdd")) + ".log";
            content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  :\r\n" + content + "\r\n";
            if (isErasable) RecordNewFileLog(filePath, content);
            else RecordConsecutiveLog(filePath, content);
        }

        /// <summary>
        /// 枚举
        /// </summary>
        public enum FolderCreationType
        {
            None
        }

        /// <summary>
        ///  日志写入
        /// </summary>
        /// <param name="filePhysicalUrl"></param>
        /// <param name="pursuitContent"></param>
        private static void RecordConsecutiveLog(string filePhysicalUrl, string pursuitContent)
        {
            FileStream fs = new FileStream(filePhysicalUrl, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter mStreamWriter = new StreamWriter(fs);
            mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);

            string resultStr = Environment.NewLine + pursuitContent;

            mStreamWriter.WriteLine(resultStr);
            mStreamWriter.Flush();
            mStreamWriter.Close();
            fs.Close();
        }
        private static void RecordNewFileLog(string filePhysicalUrl, string content)
        {
            StreamWriter sw = new StreamWriter(filePhysicalUrl);
            sw.WriteLine(content);
            sw.Close();
        }
    }
}