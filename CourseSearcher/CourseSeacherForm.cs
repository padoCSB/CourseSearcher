using System.Data;
using System.Text;
using HtmlAgilityPack;
using System.Text.Json;

namespace CourseSearcher
{
    public partial class CourseSeacherForm : Form
    {
        public struct EnrollmentData
        {
            public DateTime LastUpdate { get; set; }
            public List<ClassEnrollment> AllCourses { get; set; }
        }
        public struct ClassEnrollment
        {
            public string Course { get; set; }
            public string Section { get; set; }
            public string Time { get; set; }
            public string Day { get; set; }
            public string Room { get; set; }
            public bool IsOpen { get; set; }
            public string School { get; set; }
        }

        private const int MaxID = 10;
        private const string htmlSite = "https://apps.benilde.edu.ph/sis/reminders_actualcount.asp?id=";

        private DataTable table = new DataTable();
        private List<ClassEnrollment> totalList = new List<ClassEnrollment>();
        private bool canPress = true;

        private string PathName => Path.Combine(Environment.CurrentDirectory, "CourseData.json");
        public CourseSeacherForm()
        {
            InitializeComponent();
            if (table.Columns.Count == 0)
            {
                string[] columnNames = new string[] { "Course", "Section", "Day", "Time", "Room", "School" };
                foreach (var item in columnNames)
                {
                    DataColumn col = new DataColumn(item, typeof(string));
                    table.Columns.Add(col);
                }
                gridView.DataSource = table;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!canPress) return;

            GetAllCourses();
            Cursor.Current = Cursors.Default;
        }

        private async void GetAllCourses()
        {
            canPress = false;
            if (totalList.Count == 0 || refreshCheckBox.Checked)
            {
                totalList.Clear();
                pbWeb.Maximum = MaxID;
                pbWeb.Value = 0;
                if (File.Exists(PathName) && !refreshCheckBox.Checked)
                {
                    using (StreamReader sr = File.OpenText(PathName))
                    {
                        var data = JsonSerializer.Deserialize<EnrollmentData>(sr.ReadToEnd());
                        totalList = data.AllCourses;
                        SetLastUpdatedText(data.LastUpdate);
                    }
                    pbWeb.Value = pbWeb.Maximum;
                }
                else
                {
                    var context = SynchronizationContext.Current;
                    List<Task> tasks = new List<Task>();
                    await Task.Run(() =>
                    {
                        for (int i = 1; i <= MaxID; i++)
                        {
                            string html = $"{htmlSite}{i}";
                            ConvertHtmlToPlainText(html, totalList);
                            context?.Post(_ => { pbWeb.Value += 1; }, null);
                        }
                    });
                    EnrollmentData enrollmentData = new EnrollmentData()
                    {
                        AllCourses = totalList,
                        LastUpdate = DateTime.Now
                    };

                    SetLastUpdatedText(enrollmentData.LastUpdate);
                    SaveJSONData(enrollmentData);
                }
            }

            var searched = totalList?.OrderBy(x => x.Course)
                .Where(y => string.IsNullOrEmpty(enrollmentTextBox.Text) ? y.IsOpen : enrollmentTextBox.Text.ToUpper().Contains(y.Course) && y.IsOpen);
            var listdata = searched.ToList().Count;
            var allowCourse = AllowCourseForm.GetAllowCourses();
            if (allowCourse != null && searched != null)
            {
                searched = searched.Where(x => allowCourse.IsAllowed(x.School));
            }
            CreateGridView(searched?.ToList());
            canPress = true;
        }
        private void SetLastUpdatedText(DateTime date)
        {
            lastUpdateLabel.Text = $"Last Update: {date.ToString("MM-dd-yy hh:mm tt")}";
            lastUpdateLabel.Visible = true;
        }
        private void SaveJSONData(EnrollmentData enrollmentData)
        {
            string serialize = JsonSerializer.Serialize(enrollmentData);

            // Check if file already exists. If yes, delete it.
            if (File.Exists(PathName))
            {
                File.Delete(PathName);
            }

            File.WriteAllText(PathName, serialize);
        }
        public void ConvertHtmlToPlainText(string html, List<ClassEnrollment> totalList)
        {
            var web = new HtmlWeb();
            var document = web.Load(html);
            var list = document.DocumentNode.QuerySelectorAll("tr").Skip(1);
            foreach (var a in list)
            {
                var text = a.InnerText;
                var arr = text.Split('\n').Select(y => y.Replace("&nbsp;", "").Trim()).Skip(2).ToArray();
                ClassEnrollment classEnrollment = new ClassEnrollment
                {
                    Course = arr[0],
                    Section = arr[1],
                    Day = arr[2],
                    Time = arr[3],
                    Room = arr[4],
                    IsOpen = arr[6] == "Open",
                    School = arr[8]
                };

                totalList.Add(classEnrollment);
            }
        }

        private void CreateGridView(List<ClassEnrollment>? enrollmentList)
        {
            table.Rows.Clear();
            if (enrollmentList == null)
                return;

            foreach (ClassEnrollment enrollment in enrollmentList)
            {
                DataRow row = table.NewRow();
                row[0] = enrollment.Course;
                row[1] = enrollment.Section;
                row[2] = enrollment.Day;
                row[3] = enrollment.Time;
                row[4] = enrollment.Room;
                row[5] = enrollment.School;
                table.Rows.Add(row);
            }

            gridView.DataSource = table;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Put in the COURSE CODE in the TextBox on the left. You can put multiple course codes by putting a space in between course codes.\n\n" +
                "Press the search button to search through all the available course offerings that are open.\n\n" +
                "Tick the Refresh Search to get an updated search.\n\n"+
                "To filter Schools, go to File > Filter Schools";
            MessageBox.Show(text, "Help");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "This is a Course Searcher to find available courses for the term.\nThe data is taken from the SIS page from the Course Offerings.\n\nCreated by the SMIT-IEMC.";
            MessageBox.Show(text, "About");
        }

        private void schoolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowCourseForm form = new AllowCourseForm();
            form.Show();
        }
    }
}
