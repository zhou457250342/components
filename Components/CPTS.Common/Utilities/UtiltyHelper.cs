using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Utilities
{
    public static class UtiltyHelper
    {
        #region Extsion
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ParseType<T>(this object obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(T);
            }
        }
        public static string GetFullMessage(this Exception ex)
        {
            if (ex == null) return string.Empty;
            return string.Format("Msg:{0}---InnerMsg:{1}-----Source:{2}", ex.Message,
                ex.InnerException != null ? ex.InnerException.Message : string.Empty, ex.Source + "|" + ex.StackTrace);
        }
        #endregion

        public static void WriteFile(string path, string context, bool isAutoCreateDir = true, bool isCreateNew = false)
        {
            if (!File.Exists(path))
            {
                if (!isAutoCreateDir) return;
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            using (FileStream write = new FileStream(path, isCreateNew ? FileMode.Create : FileMode.OpenOrCreate))
            {
                using (StreamWriter stream = new StreamWriter(write))
                {
                    stream.Write(context);
                    stream.Flush();
                }
            }
        }
        public static string ReaderFile(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

    }
}
