using Newtonsoft.Json;

namespace CourseSearcher.DataHelpers
{
    internal class ProjectSettings
    {
        private static ProjectSettings? instance;
        public static ProjectSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectSettings();
                }

                return instance;
            }
        }

        public ProjectSettings()
        {

        }
        private string SavePathName => Path.Combine(Environment.CurrentDirectory, "ProjectSettings.json");
        private ProjectSettingsData? ProjectSettingsData;
        public void SaveSettings(object[] args)
        {
            if (ProjectSettingsData == null)
            {
                ProjectSettingsData = new ProjectSettingsData();
            }
            foreach (object item in args)
            {
                if (item is FilteredCourses data)
                {
                    ProjectSettingsData.FilteredCourseData = data;
                }

                if (item is ColorData colorData)
                {
                    ProjectSettingsData.ColorData = colorData;
                }
            }

            string serialize = JsonConvert.SerializeObject(ProjectSettingsData);

            // Check if file already exists. If yes, delete it.
            if (File.Exists(SavePathName))
            {
                File.Delete(SavePathName);
            }

            File.WriteAllText(SavePathName, serialize);
        }

        public T GetData<T>()
        {
            if (ProjectSettingsData == null)
            {
                if (!File.Exists(SavePathName))
                {
                    ProjectSettingsData = new ProjectSettingsData();
                    SaveSettings([]);
                }
                else
                {
                    try
                    {
                        using (StreamReader sr = File.OpenText(SavePathName))
                        {
                            var b = JsonConvert.DeserializeObject<ProjectSettingsData>(sr.ReadToEnd());
                            ProjectSettingsData = b;
                        }
                    }
                    catch
                    {
                        File.Delete(SavePathName);

                        SaveSettings([]);
                    }
                }
            }
            return (T)ProjectSettingsData?.GetType()?.GetProperty(typeof(T).Name)?.GetValue(ProjectSettingsData) ?? default;
        }
    }

    public class ProjectSettingsData
    {
        public FilteredCourses FilteredCourseData { get; set; }
        public ColorData ColorData { get; set; }

        public ProjectSettingsData()
        {
            FilteredCourseData = new FilteredCourses();
            ColorData = new ColorData();
        }

    }

    interface IResettable
    {
        void Reset();
    }
}
