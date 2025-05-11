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
        private ProjectSettingsData? data;
        public void SaveSettings(object[] args)
        {
            if (data == null)
            {
                data = new ProjectSettingsData();
            }
            foreach (object item in args)
            {
                if (item is FilteredCourses data)
                {
                    this.data.FilteredCourses = data;
                }

                if (item is ColorData colorData)
                {
                    this.data.ColorData = colorData;
                }
            }

            FileManager.Save(data);
        }

        public T GetData<T>()
        {
            data = FileManager.Load<ProjectSettingsData>();

            var property = typeof(ProjectSettingsData).GetProperty(typeof(T).Name);
            var val = (T)(Activator.CreateInstance<T>());
            if (property == null)
                return val;

            var actualValue = (T)property.GetValue(data);
            if (actualValue == null)
                return val;

            return actualValue;
        }
    }

    public class ProjectSettingsData
    {
        public FilteredCourses FilteredCourses { get; set; }
        public ColorData ColorData { get; set; }

        public ProjectSettingsData()
        {
            FilteredCourses = new FilteredCourses();
            ColorData = new ColorData();
        }

    }

    interface IResettable
    {
        void Reset();
    }
}
