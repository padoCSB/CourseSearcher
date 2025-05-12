using CourseSearcher.DataHelpers;
using System.Windows.Forms;

namespace CourseSearcher
{
    public partial class ScheduleForm : Form
    {
        const int Rows = 13;
        const int Columns = 6;
        const int Div = 6;
        private List<TimeColumn> buttonColumns = new List<TimeColumn>();
        private ColorData? colorData = new ColorData();
        private List<TimeColumn> interactedColumns = new List<TimeColumn>();
        private TimeColumn? latestColumn;
        private ColorData ColorData
        {
            get
            {
                if(colorData == null)
                {
                    colorData = new ColorData();
                }
                return colorData;
            }
            set
            {
                colorData = value;
            }
        }
        public ScheduleForm()
        {
            InitializeComponent();
            SetLoadData();
            ColorData = ProjectSettings.Instance.GetData<ColorData>();
            TimeSpan time = TimeSpan.FromHours(8);
            for (int i = 1; i <= Rows; i++)
            {
                if (tableLayoutPanel1.GetControlFromPosition(0, i) is Label label)
                {
                    label.Text = time.ToString(@"hh\:mm");
                    time = time.Add(TimeSpan.FromHours(1));
                }
            }

            for (int i = 1; i <= Columns; i++)
            {
                for (int j = 1; j <= Rows; j++)
                {
                    TimeColumn panel = new TimeColumn();
                    buttonColumns.Add(panel);
                    panel.ColorData = ColorData;
                    tableLayoutPanel1.Controls.Add(panel);
                    tableLayoutPanel1.SetCellPosition(panel, new TableLayoutPanelCellPosition(i, j));
                    panel.Dock = DockStyle.Fill;
                    panel.Margin = new Padding(0);
                }
            }

            MouseHandler gmh = new MouseHandler();
            gmh.MouseMove += Gmh_MouseMove;
            gmh.MouseDown += Gmh_MouseDown;
            gmh.MouseUp += Gmh_MouseUp;
            Application.AddMessageFilter(gmh);

            highlightToolStripMenuItem.Click += ColorStripMenu_Click;
            selectedToolStripMenuItem.Click += ColorStripMenu_Click;
            hoveringSelectedToolStripMenuItem.Click += ColorStripMenu_Click;
        }
        #region MouseEvents
        private void Gmh_MouseUp(Point point)
        {
            foreach (TimeColumn column in interactedColumns)
            {
                column.MouseUpOnPanel(point);
            }

            latestColumn = null;
        }

        private void Gmh_MouseDown(Point point)
        {
            Control? control = GetControlOnPoint(point);
            if (control is TimeColumn column)
            {
                if (!interactedColumns.Contains(column))
                {
                    interactedColumns.Add(column);
                }

                column.MouseDownOnPanel(point);

                latestColumn = column;
            }
        }

        private void Gmh_MouseMove(Point point, bool isPressed)
        {
            Control? control = GetControlOnPoint(point);

            if (control is TimeColumn column)
            {
                if (isPressed)
                {
                    if (!interactedColumns.Contains(column))
                    {
                        interactedColumns.Add(column);
                        column.MouseDownOnPanel(point);
                    }
                }

                if (latestColumn == null)
                {
                    latestColumn = column;
                }
                else if (latestColumn != column)
                {
                    latestColumn.MouseLeftPanel(point);
                    latestColumn = column;
                }

                column.MouseMoveOnPanel(point, isPressed);
            }
            else if (latestColumn != null)
            {
                latestColumn.MouseLeftPanel(point);
                latestColumn = null;
            }
        }

        private Control? GetControlOnPoint(Point point)
        {
            Point newPoint = tableLayoutPanel1.PointToClient(point);
            return tableLayoutPanel1.GetChildAtPoint(newPoint);
        }
        #endregion

        private void SetLoadData()
        {
            loadToolStripMenuItem.DropDownItems.Clear();
            foreach (string name in Recorder.Instance.GetAllNames())
            {
                loadToolStripMenuItem.DropDownItems.Add(name, null, (x, y) =>
                {
                    LoadData(name);
                });
            }

            loadToolStripMenuItem.Enabled = loadToolStripMenuItem.HasDropDownItems;
        }

        private void ColorStripMenu_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                colorDialog1.ShowDialog();
                ChangeColor(menuItem.Text);
            }
        }

        private void ChangeColor(string? toolName)
        {
            if (buttonColumns.Count == 0 || string.IsNullOrEmpty(toolName))
                return;

            switch (toolName)
            {
                case "Highlight":
                    buttonColumns.ForEach(x => ColorData.HighLightColor = colorDialog1.Color);
                    ColorData.HighLightColor = colorDialog1.Color;
                    break;
                case "Selected":
                    buttonColumns.ForEach(x => ColorData.SelectColor = colorDialog1.Color);
                    ColorData.SelectColor = colorDialog1.Color;
                    break;
                case "Hovering Selected":
                    buttonColumns.ForEach(x => ColorData.HoverColor = colorDialog1.Color);
                    ColorData.HoverColor = colorDialog1.Color;
                    break;
            }
            ProjectSettings.Instance.SaveSettings([ColorData]);
            RefreshColumns();
        }

        private void ScheduleForm_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void ScheduleForm_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeLayout();
        }

        private void ResetColors_Click(object sender, EventArgs e)
        {
            ColorData.Reset();
            ProjectSettings.Instance.SaveSettings([ColorData]);
            RefreshColumns();
        }

        private void ShowHelp_Click(object sender, EventArgs e)
        {
            string helpString = "This is a form to block all the time slots that are not available.\n\n" +
                "Click and drag on the times that are not available.\n\n" +
                "Change colors under the Options tool\n\n" +
                "Import Course data by typing the Course and Section with a space in between. For multiple Course and Sections, put a comma in between.\n\n" +
                "Example: CAPSTN1 BTIE1, CSBGRAD BLDI15\n\n";

            MessageBox.Show(helpString);
        }

        // 0 - Monday
        private List<BlockedTime> GetBlockedSlots(int day = 0)
        {
            var a = Enumerable.Range(1, tableLayoutPanel1.RowCount)
                 .Select(x => ((TimeColumn)tableLayoutPanel1.GetControlFromPosition(day + 1, x))?.GetBlockedSlots())
                 .Where(z => z != null)
                 .SelectMany(y => y).ToList();

            bool applying = false;
            BlockedTime time = new BlockedTime();
            List<BlockedTime> allBlockedTimes = new List<BlockedTime>();
            int div = 6;
            for (int i = 0; i < a.Count; i++)
            {
                // Each hour is made up of 'div' slots
                int hour = 8 + (int)Math.Floor((float)i / div);
                int minute = (60 / div) * (i % div);

                if (a[i] && !applying)
                {
                    time.Start = new TimeSpan(day, hour, minute, 0);
                    applying = true;
                }
                else if ((!a[i] || (i == a.Count - 1)) && applying)
                {
                    time.End = new TimeSpan(day, hour, minute, 0);
                    applying = false;
                    allBlockedTimes.Add(time);
                }
            }

            return allBlockedTimes;
        }

        private List<BlockedTime> GetAllBlockedSlots()
        {
            List<BlockedTime> records = new List<BlockedTime>();
            for (int i = 0; i < Columns; i++)
            {
                var a = GetBlockedSlots(i);
                if (a.Count > 0)
                    records.AddRange(a);
            }

            return records;
        }

        private void ClearAllColumns()
        {
            buttonColumns.ForEach(x => x.Clear());
        }

        private void RefreshColumns()
        {
            buttonColumns.ForEach(x => x.Invalidate());
        }

        private void BlockTimeData(BlockedTime item)
        {
            int minutes = 10;
            TimeSpan adjusted = new TimeSpan(0, item.Start.Hours - 8, item.Start.Minutes, 0);
            TimeSpan adjustedEnd = new TimeSpan(0, item.End.Hours - 8, item.End.Minutes, 0);
            int rowIndex = (item.Start.Days * Rows);

            while (adjusted.CompareTo(adjustedEnd) < 0)
            {
                int currentRowIndex = rowIndex + (int)Math.Floor((float)adjusted.TotalMinutes / 60f);
                int index = (int)(Math.Floor(adjusted.TotalMinutes % 60 / minutes));
                buttonColumns[currentRowIndex].AddToSelected(index);
                adjusted = adjusted.Add(new TimeSpan(0, minutes, 0));
            }
        }

        private void LoadData(string name)
        {
            if (string.IsNullOrEmpty(name) || !Recorder.Instance.NameExist(name))
            {
                return;
            }

            var data = Recorder.Instance.GetData(name);
            if (data == null)
                return;

            foreach (var item in data.BlockedTimes)
            {
                BlockTimeData(item);
            }
            RefreshColumns();
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearAllColumns();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var coursesToImport = textBox2.Text.ToUpper().Split(',').Where(y=> y.Trim().Split(' ').Length == 2).Select(x =>
            {
                string[] split = x.Trim().Split(' ');
                return new ClassEnrollment() { Course = split[0], Section = split[1] };
            });

            var allCourses = CourseRetriever.Instance.GetAllCourses;
            allCourses = allCourses.Where(x => coursesToImport.Any(y => y.Course == x.Course && y.Section == x.Section)).ToList();

            List<BlockedTime> records = GetAllBlockedSlots();

            foreach (ClassEnrollment enrollment in allCourses)
            {
                if (!RecordData.IsAllowed(records, enrollment.Day, enrollment.Time))
                    continue;

                foreach (char c in enrollment.Day)
                {
                    BlockedTime? time = RecordData.ConvertDayTime(c, enrollment.Time);

                    if (time == null)
                        continue;
                    BlockTimeData((BlockedTime)time);
                }
            }

            RefreshColumns();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var records = GetAllBlockedSlots();
            if (records.Count == 0)
            {
                MessageBox.Show("No slots were blocked off");
                return;
            }

            SaveForm form = new SaveForm(records);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.Close();
                return;
            }
            SetLoadData();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearRecordDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recorder.Instance.Reset();
            SetLoadData();
        }

        private void clearColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAllColumns();
        }

        // Speeds up redrawing of table when changing window size
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }

    public class ColorData : IResettable
    {
        private static readonly Color DefaultHighlightColor = Color.LawnGreen;
        private static readonly Color DefaultSelectColor = Color.SeaGreen;
        private static readonly Color DefaultHoverColor = Color.LightGray;

        public Color HoverColor { get; set; } = DefaultHoverColor;
        public Color SelectColor { get; set; } = DefaultSelectColor;
        public Color HighLightColor { get; set; } = DefaultHighlightColor;
        public ColorData(Color hoverColor, Color selectColor, Color highLightColor)
        {
            HoverColor = hoverColor;
            SelectColor = selectColor;
            HighLightColor = highLightColor;
        }
        public void SetColor(ref Color hoverColor, ref Color selectColor, ref Color highLightColor)
        {
            hoverColor = HoverColor;
            selectColor = SelectColor;
            highLightColor = HighLightColor;
        }
        public ColorData()
        {
            Reset();
        }
        public void Reset()
        {
            HoverColor = DefaultHoverColor;
            SelectColor = DefaultSelectColor;
            HighLightColor = DefaultHighlightColor;
        }
    }

    public struct BlockedTime
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }

    public class RecordData {
        public RecordData()
        {
            Name = string.Empty;
            BlockedTimes = new List<BlockedTime>();
        }
        public string Name { get; set; }
        public List<BlockedTime> BlockedTimes { get; set; }
        public static int CharToInt(char c) => c switch { 'M' => 0, 'T' => 1, 'W' => 2, 'H' => 3, 'F' => 4, 'S' => 5, _ => -1 };
        public bool IsAllowed(string day, string time)
        {
            return IsAllowed(BlockedTimes, day, time);
        }
        public static BlockedTime? ConvertDayTime(char day, string time)
        {
            if (!time.Contains(':'))
                return null;

            int dayIndex = CharToInt(day);
            var startEnd = time.Split('-')
                .Select(x => x.Split(':'))
                .Select(z => new TimeSpan(dayIndex, int.Parse(z[0]), int.Parse(z[1]), 0)).ToArray();

            return new BlockedTime() { Start = startEnd[0], End = startEnd[1] };
        }
        public static bool IsAllowed(List<BlockedTime> blockedTimes, string day, string time)
        {
            if(string.IsNullOrEmpty(day) || string.IsNullOrEmpty(time)) return true;

            foreach(char c in day)
            {
                int dayIndex = CharToInt(c);

                if (dayIndex < 0)
                    continue;

                var sameDay = blockedTimes.Where(x => x.Start.Days == dayIndex);
                var result = ConvertDayTime(c, time);

                if (sameDay == null || result == null)
                    continue;

                bool allowed = !sameDay.Any(y => y.Start.CompareTo(((BlockedTime)result).Start) < 0 && 
                                        y.End.CompareTo(((BlockedTime)result).End) > 0);
                if (!allowed)
                    return false;
            }

            return true;
        }
    }
}
