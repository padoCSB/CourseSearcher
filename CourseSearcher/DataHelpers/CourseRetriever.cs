using HtmlAgilityPack;

namespace CourseSearcher.DataHelpers
{
    public struct EnrollmentData
    {
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

        private List<ClassEnrollment> totalList = new List<ClassEnrollment>();
        public List<ClassEnrollment> GetAllCourses => totalList;

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
        public async Task GetData(bool forceLoad = false, ProgressBar? progressBar = null, Label? label = null, Action? onLoading = null, Action? onFinish = null)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (totalList.Count == 0 || forceLoad)
            {
                if (progressBar != null)
                {
                    progressBar.Maximum = MaxID;
                    progressBar.Value = 0;
                }

                if (FileManager.FileExists<EnrollmentData>() && !forceLoad)
                {
                    EnrollmentData? d = FileManager.Load<EnrollmentData>();
                    if(d != null)
                    {
                        EnrollmentData data = (EnrollmentData)d;
                        totalList = data.AllCourses;
                        SetLastUpdatedText(label, data.LastUpdate);
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
                    var context = SynchronizationContext.Current;
                    var list = new List<ClassEnrollment>();
                    if (onLoading != null)
                        onLoading.Invoke();
                    await Task.Run(() =>
                    {
                        try
                        {
                            for (int i = 1; i <= MaxID; i++)
                            {
                                string html = $"{htmlSite}{i}";
                                ConvertHtmlToPlainText(html, list);
                                if (progressBar != null)
                                {
                                    context?.Post(_ => { progressBar.Value += 1; }, null);
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Problem with loading the sites.");
                            return;
                        }
                    });
                    if (onFinish != null)
                        onFinish.Invoke();

                    totalList = list;
                    EnrollmentData enrollmentData = new EnrollmentData()
                    {
                        AllCourses = totalList,
                        LastUpdate = DateTime.Now
                    };

                    SetLastUpdatedText(label, enrollmentData.LastUpdate);
                    FileManager.Save(enrollmentData);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void ConvertHtmlToPlainText(string html, List<ClassEnrollment> totalList)
        {
            var document = new HtmlWeb().Load(html);
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

    }
}
