using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CourseSearcher.DataHelpers.CourseRetriever;
using static CourseSearcher.DataHelpers.Recorder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseSearcher.DataHelpers
{
    public static class FileManager
    {
        private static string SavePathName => Path.Combine(Environment.CurrentDirectory, "Data");
        private static string GetPathName<T>() => Path.Combine(SavePathName, GetFileName(typeof(T)));
        public static void Save<T>(T data)
        {
            string serialize = JsonConvert.SerializeObject(data);
            string pathName = GetPathName<T>();
            // Check if file already exists. If yes, delete it.
            if (File.Exists(pathName))
            {
                File.Delete(pathName);
            }
            string folderPath = Path.GetDirectoryName(pathName);
            if (!Directory.Exists(folderPath) && !string.IsNullOrEmpty(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            File.WriteAllText(pathName, serialize);
        }

        public static T? Load<T>()
        {
            T data;
            if (!File.Exists(GetPathName<T>()))
            {
                data = (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                try
                {
                    using (StreamReader sr = File.OpenText(GetPathName<T>()))
                    {
                        data = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                    }
                }
                catch
                {
                    File.Delete(GetPathName<T>());

                    data = (T)Activator.CreateInstance(typeof(T));
                }
            }

            return data;
        }
        public static void DeleteFile<T>()
        {
            if (File.Exists(GetPathName<T>()))
            {
                File.Delete(GetPathName<T>());
            }
        }
        public static bool FileExists<T>()
        {
            return File.Exists(GetPathName<T>());
        }
        private static string GetFileName(Type dataType) 
        {
            string fileName = null;
            if(dataType == typeof(ProjectSettingsData)) 
            {
                fileName = "ProjectSettings";
            }
            if(dataType == typeof(RecordDatas))
            {
                fileName = "RecordData";
            }
            if(dataType == typeof(EnrollmentData))
            {
                fileName = "CourseData";
            }

            if (fileName == null)
                fileName = dataType.Name;

            return fileName + ".json";
        }
    }
}
