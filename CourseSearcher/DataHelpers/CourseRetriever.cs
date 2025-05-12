using HtmlAgilityPack;
using System.Windows.Forms;

namespace CourseSearcher.DataHelpers
{
    public struct EnrollmentData
    {
        public EnrollmentData() {
            AllCourses = new List<ClassEnrollment>();
        }
        public DateTime LastUpdate { get; set; }
        public List<ClassEnrollment> AllCourses { get; set; }
    }

    public class CourseRetriever
    {
        private static CourseRetriever? instance;
        public static CourseRetriever Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CourseRetriever();
                }

                return instance;
            }
        }
        private const int MaxID = 10;
        private const string htmlSite = "https://apps.benilde.edu.ph/sis/reminders_actualcount.asp?id=";

        //private string PathName => Path.Combine(Environment.CurrentDirectory,"Data", "CourseData.json");
        private EnrollmentData enrollmentData = new EnrollmentData();

        public List<ClassEnrollment> GetAllCourses => enrollmentData.AllCourses;

        private CourseRetriever()
        {

        }

        private void SetLastUpdatedText(Label? label, DateTime date)
        {
            if (label == null)
                return;

            label.Text = $"Last Update: {date.ToString("MM-dd-yy hh:mm tt")}";
            label.Visible = true;
        }
        public async Task GetData(bool forceLoad = false, ProgressBar? progressBar = null, Label? lastUpateLabel = null, TextBox? currentActionLabel = null, Form? form = null)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (GetAllCourses.Count == 0 || forceLoad)
            {
                if (progressBar != null)
                {
                    progressBar.Maximum = MaxID;
                    progressBar.Value = 0;
                }

                if (FileManager.FileExists<EnrollmentData>() && !forceLoad)
                {
                    EnrollmentData? data = FileManager.Load<EnrollmentData>();
                    if(data != null)
                    {
                        enrollmentData = (EnrollmentData)data;
                        if (progressBar != null)
                        {
                            progressBar.Value = progressBar.Maximum;
                        }
                    }
                    else
                    {
                        await GetData();
                    }
                }
                else
                {
                    var htmlData = await GetHTMLData(progressBar, form);
                    var list = htmlData;
                    enrollmentData = new EnrollmentData()
                    {
                        AllCourses = list,
                        LastUpdate = DateTime.Now
                    };

                    FileManager.Save(enrollmentData);
                }
            }

            SetLastUpdatedText(lastUpateLabel, enrollmentData.LastUpdate);
            Cursor.Current = Cursors.Default;
        }

        private async Task<List<ClassEnrollment>> GetHTMLData(ProgressBar? progressBar, Form? form)
        {
            form?.Show();
            TextBox currentActionLabel = form?.Controls.OfType<TextBox>().SingleOrDefault();
            void AddText(string text)
            {
                if (currentActionLabel != null)
                    currentActionLabel.AppendText(text);
            }

            var context = SynchronizationContext.Current;
            var list = new List<ClassEnrollment>();
            AddText("Retrieving data from SIS...\n");
            AddText(Environment.NewLine);
            try
            {
                for (int i = 1; i <= MaxID; i++)
                {
                    string html = $"{htmlSite}{i}";
                    AddText($"Getting data from {html}...");
                    await ConvertHtmlToPlainText(html, list);
                    if (progressBar != null)
                    {
                        context?.Post(_ => { progressBar.Value += 1; }, null);
                    }
                    AddText("Done!");
                    AddText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                AddText($"\nRetrieval failed!\n{ex.Message}");
                return list;
            }
            AddText($"\nRetrieval successful!");
            return list;
        }

        private async Task ConvertHtmlToPlainText(string html, List<ClassEnrollment> totalList)
        {
            var document = new HtmlWeb().LoadFromWebAsync(html);
            await document;
            var list = document.Result.DocumentNode.QuerySelectorAll("tr").Skip(1);
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

    }
}
