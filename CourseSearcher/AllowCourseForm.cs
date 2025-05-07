using System.Text.Json;

namespace CourseSearcher
{
    public partial class AllowCourseForm : Form
    {
        public class AllowCourses
        {
            public bool SACP { get; set; }
            public bool SNMA { get; set; }
            public bool SED { get; set; }
            public bool SDEAS { get; set; }
            public bool SMIT { get; set; }
            public bool SHRIM { get; set; }
            public bool SMITCDP { get; set; }
            public bool SDG { get; set; }
            public bool SMS { get; set; }
            public bool SDA { get; set; }

            public bool IsAllowed(string text)
            {
                text = text.Replace("-", "");
                var field = typeof(AllowCourses).GetProperties().SingleOrDefault(x => x.Name == text, null);

                if (field == null)
                    return true;

                var val = field.GetValue(this);

                if(val == null) return true;

                return (bool)val;
            }
        }

        private static string AllowCoursePathName => Path.Combine(Environment.CurrentDirectory, "AllowedCourses.json");
        public static AllowCourses? GetAllowCourses()
        {
            if (!File.Exists(AllowCoursePathName))
                return null;
            using (StreamReader sr = File.OpenText(AllowCoursePathName))
            {
                return JsonSerializer.Deserialize<AllowCourses>(sr.ReadToEnd());
            }
    }

        public AllowCourseForm()
        {
            InitializeComponent();

            if (File.Exists(AllowCoursePathName))
            {
                using (StreamReader sr = File.OpenText(AllowCoursePathName))
                {
                    var data = JsonSerializer.Deserialize<AllowCourses>(sr.ReadToEnd());
                    checkBoxSACP.Checked = data.SACP;
                    checkBoxSNMA.Checked = data.SNMA;
                    checkBoxSED.Checked = data.SED;
                    checkBoxSMS.Checked = data.SMS;
                    checkBoxSMITCDP.Checked = data.SMITCDP;
                    checkBoxSMIT.Checked = data.SMITCDP;
                    checkBoxSDG.Checked = data.SDG;
                    checkBoxSDA.Checked = data.SDA;
                    checkBoxSDEAS.Checked = data.SDEAS;
                    checkBoxSHRIM.Checked = data.SHRIM;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllowCourses allowCourses = new AllowCourses()
            {
                SACP = checkBoxSACP.Checked,
                SNMA = checkBoxSNMA.Checked,
                SDA = checkBoxSDA.Checked,
                SED = checkBoxSED.Checked,
                SDEAS = checkBoxSDEAS.Checked,
                SDG = checkBoxSDG.Checked,
                SHRIM = checkBoxSHRIM.Checked,
                SMIT = checkBoxSMIT.Checked,
                SMITCDP = checkBoxSMITCDP.Checked,
                SMS = checkBoxSMS.Checked
            };

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
}
