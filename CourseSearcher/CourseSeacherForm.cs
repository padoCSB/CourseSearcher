using System.Data;
using CourseSearcher.DataHelpers;

namespace CourseSearcher
{
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

    public partial class CourseSeacherForm : Form
    {

        private DataTable table = new DataTable();
        private bool canPress = true;
        private int columnToHide;

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
            Init();
        }

        private async void Init()
        {
            if (CourseRetriever.Instance.GetAllCourses.Count == 0)
            {
                Form form = new Form()
                {
                    Size = new Size(750, 250),
                    ShowIcon = false,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    TopMost = true,
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowInTaskbar = true,
                    SizeGripStyle = SizeGripStyle.Hide,
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    Text = "Initializing",
                };
                ProgressBar bar = new ProgressBar() { Dock = DockStyle.Bottom, Size = new Size(0,10)};
                TextBox textBox = new TextBox() { Dock = DockStyle.Fill, TextAlign = HorizontalAlignment.Left, Font = new Font(FontFamily.GenericMonospace, 8.5f), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical};
                form.Controls.Add(textBox);
                form.Controls.Add(bar);
                await CourseRetriever.Instance.GetData(false, bar, null, textBox, form);
            }
        }

        private async void GetAllCourses()
        {
            canPress = false;
            await CourseRetriever.Instance.GetData(refreshCheckBox.Checked, pbWeb, lastUpdateLabel);
            
            List<ClassEnrollment> filteredList = new List<ClassEnrollment>();
            filteredList = FilterListFromRecord(CourseRetriever.Instance.GetAllCourses);

            if (filteredList == null || filteredList.Count == 0)
                filteredList = CourseRetriever.Instance.GetAllCourses;

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
            var filteredSchools = ProjectSettings.Instance.GetData<FilteredCourses>();
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

            CreateRows(filteredList);
            canPress = true;
        }

        private List<ClassEnrollment>? FilterListFromRecord(List<ClassEnrollment> list)
        {
            if (!Recorder.Instance.NameExist(comboBox1.Text.Trim()))
            {
                return null;
            }

            var data = Recorder.Instance.GetData(comboBox1.Text.Trim());
            if (data == null) { return null; }

            return list.Where(x => data.IsAllowed(x.Day, x.Time)).ToList();
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
            string text = "This is a Course Searcher to find available courses for the term.\nThe data is taken from the SIS page from the Course Offerings.\n\nCreated by the SMIT-IEMC faculty.";
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

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            comboBox1.Items.AddRange(Recorder.Instance.GetAllNames().ToArray());
        }

        private void createTSM_Click(object sender, EventArgs e)
        {
            Form schedForm = new ScheduleForm();
            schedForm.ShowDialog(this);
        }
    }

    
}
