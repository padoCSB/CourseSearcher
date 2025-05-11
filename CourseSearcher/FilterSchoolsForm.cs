using System.Reflection;
using CourseSearcher.DataHelpers;

namespace CourseSearcher
{
    public partial class FilterSchoolsForm : Form
    {
        private static string AllowCoursePathName => Path.Combine(Environment.CurrentDirectory, "AllowedCourses.json");

        public FilterSchoolsForm()
        {
            InitializeComponent();

            var data = ProjectSettings.Instance.GetData<FilteredCourses>();
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

            ProjectSettings.Instance.SaveSettings([allowCourses]);
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
    public class FilteredCourses : IResettable
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

        public List<string> GetSchoolList(bool isOpen = false)
        {
            var schools = this.GetType().GetProperties().Where(x =>
            {
                var val = x.GetValue(this);
                if (val is bool open)
                    return open == isOpen;
                return false;
            }).Select(z => z.GetCustomAttribute<CustomName>()?.Data ?? z.Name).ToList();

            return schools;
        }

        public void Reset()
        {
            var a = this.GetType().GetProperties().Where(x => x.GetType() == typeof(bool));
            foreach (var item in a)
            {
                item.SetValue(this, true);
            }
        }
    }
}
