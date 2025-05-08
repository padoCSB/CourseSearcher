using System.ComponentModel;
using System.Text.Json;

namespace CourseSearcher
{
    public partial class FilterSchoolsForm : Form
    {
        private static string AllowCoursePathName => Path.Combine(Environment.CurrentDirectory, "AllowedCourses.json");
        public static FilteredCourses? GetAllowCourses()
        {
            if (!File.Exists(AllowCoursePathName))
                return null;
            try
            {
                using (StreamReader sr = File.OpenText(AllowCoursePathName))
                {
                    return JsonSerializer.Deserialize<FilteredCourses>(sr.ReadToEnd());
                }
            }
            catch
            {
                File.Delete(AllowCoursePathName);

                return new FilteredCourses();
            }
        }
    

        public FilterSchoolsForm()
        {
            InitializeComponent();

            if (!File.Exists(AllowCoursePathName))
                return;

            var data = GetAllowCourses();
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is CheckBox box)
                {
                    string name = box.Name.Replace("checkBox", "");
                    var property = data?.GetType().GetProperty(name);
                    var val = property?.GetValue(data);
                    box.Checked = val == null || (bool)val;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilteredCourses allowCourses = new FilteredCourses();
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is CheckBox box) 
                {
                    string name = box.Name.Replace("checkBox", "");
                    var property = allowCourses.GetType().GetProperty(name);
                    if (property == null) continue;
                    property.SetValue(allowCourses, box.Checked);
                }
            }

            string serialize = JsonSerializer.Serialize(allowCourses);

            // Check if file already exists. If yes, delete it.
            if (File.Exists(AllowCoursePathName))
            {
                File.Delete(AllowCoursePathName);
            }

            File.WriteAllText(AllowCoursePathName, serialize);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class CustomName : Attribute
    {
        public string Data { get; }
        public CustomName(string data) { Data = data; }
    }
    public class FilteredCourses
    {
        public bool SACP { get; set; } = true;
        public bool SNMA { get; set; } = true;
        public bool SED { get; set; } = true;
        public bool SDEAS { get; set; } = true;
        public bool SMIT { get; set; } = true;
        public bool SHRIM { get; set; } = true;
        [CustomName("SMIT-CDP")]
        public bool SMITCDP { get; set; } = true;
        public bool SDG { get; set; } = true;
        public bool SMS { get; set; } = true;
        public bool SDA { get; set; } = true;

        public bool IsAllowed(string text)
        {
            text = text.Replace("-", "");
            var field = typeof(FilteredCourses).GetProperties().SingleOrDefault(x => x.Name == text, null);

            if (field == null)
                return true;

            var val = field.GetValue(this);

            if (val == null) return true;

            return (bool)val;
        }
    }
}
