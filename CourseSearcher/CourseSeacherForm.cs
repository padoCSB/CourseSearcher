using System.Data;
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
        private int columnToHide;
        private string PathName => Path.Combine(Environment.CurrentDirectory, "CourseData.json");
        public CourseSeacherForm()
        {
            InitializeComponent();
            if (table.Columns.Count == 0)
            {
                string[] columnNames = new string[] { "Course", "Section", "Day", "Time", "Room", "School", "Status" };
                foreach (var item in columnNames)
                {
                    DataColumn col = new DataColumn(item, typeof(string));
                    table.Columns.Add(col);
                }
                gridView.DataSource = table;
            }
            showToolStripMenuItem.DropDown.Items.Add("Status");
            showToolStripMenuItem.Enabled = true;
            gridView.Columns["Status"].Visible = false;
            ToolStripItemEventHandler action = (x, y) => 
            {
                showToolStripMenuItem.Enabled = showToolStripMenuItem.DropDown.Items.Count > 0;
            };
            showToolStripMenuItem.DropDown.ItemAdded += action;
            showToolStripMenuItem.DropDown.ItemRemoved += action;

            showToolStripMenuItem.DropDown.ItemClicked += contextMenuStrip1_ItemClicked;
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
                    try
                    {
                        using (StreamReader sr = File.OpenText(PathName))
                        {
                            var data = JsonSerializer.Deserialize<EnrollmentData>(sr.ReadToEnd());
                            totalList = data.AllCourses;
                            SetLastUpdatedText(data.LastUpdate);
                        }
                        pbWeb.Value = pbWeb.Maximum;
                    }
                    catch
                    {
                        File.Delete(PathName);
                        totalList.Clear();
                        GetAllCourses();
                        return;
                    }
                }
                else
                {
                    var context = SynchronizationContext.Current;
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

            List<string> conditionList = new List<string>();

            // Finding all courses
            if (!string.IsNullOrEmpty(enrollmentTextBox.Text))
            {
                var criteria = enrollmentTextBox.Text.Replace('\n', ' ')
                    .Split(' ')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(y => $"Course like '%{y.Trim()}%'").ToList();
                conditionList.Add($"({string.Join(" OR ", criteria)})");
            }

            // Exclude Closed status
            if (!checkBoxIncludeClosed.Checked)
            {
                conditionList.Add("Status = 'Open'");
            }

            // Exclude Filtered Schools
            var filteredSchools = FilterSchoolsForm.GetFilteredCourses();
            if (filteredSchools != null)
            {
                var schools = filteredSchools.GetSchoolList();
                if (schools.Count > 0)
                {
                    schools = schools.Select(y => $"School <> '{y}'").ToList();
                    conditionList.Add($"({string.Join(" AND ", schools)})");
                }
            }
            table.DefaultView.RowFilter = string.Join(" AND ", conditionList);
            
            CreateRows(totalList);
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
        private void ConvertHtmlToPlainText(string html, List<ClassEnrollment> totalList)
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

        private void CreateRows(List<ClassEnrollment>? enrollmentList)
        {
            table.Rows.Clear();
            if (enrollmentList == null || enrollmentList.Count == 0)
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
                row[6] = enrollment.IsOpen ? "Open" : "Closed";
                table.Rows.Add(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!canPress) return;

            GetAllCourses();
            Cursor.Current = Cursors.Default;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Put in the COURSE CODE in the TextBox on the left. You can put multiple course codes by putting a space in between course codes.\n\n" +
                "Press the search button to search through all the available course offerings that are open.\n\n" +
                "Tick the Refresh Search to get an updated search.\n\n" +
                "To filter Schools, go to File > Filter Schools\n\n" +
                "To hide columns, right click the column head, and press Hide. \nTo show the column, right click any column head, press Show then click on the Column Name to show it again";
            MessageBox.Show(text, "Help");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "This is a Course Searcher to find available courses for the term.\nThe data is taken from the SIS page from the Course Offerings.\n\nCreated by the SMIT-IEMC.";
            MessageBox.Show(text, "About");
        }

        private void schoolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterSchoolsForm form = new FilterSchoolsForm();
            form.Show(this);
            form.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
        }

        private void contextMenuStrip1_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            var menuText = e.ClickedItem?.Text;
            gridView.Columns[menuText].Visible = true;
            if (e.ClickedItem == null)
                return;
            showToolStripMenuItem.DropDown.Items.Remove(e.ClickedItem);
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = gridView.HitTest(e.X, e.Y);
                if (hti.RowY != 1 || hti.ColumnX == 1)
                    return;

                columnToHide = hti.ColumnIndex;
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var menuText = e.ClickedItem?.Text;
            if (menuText != "Hide")
                return;
            var column = gridView.Columns[columnToHide];
            column.Visible = false;
            showToolStripMenuItem.DropDown.Items.Add(column.Name);
        }
    }
}
