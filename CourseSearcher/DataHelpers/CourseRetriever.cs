using HtmlAgilityPack;
using Newtonsoft.Json;

namespace CourseSearcher.DataHelpers
{
    public class CourseRetriever
    {
        public struct EnrollmentData
        {
            public DateTime LastUpdate { get; set; }
            public List<ClassEnrollment> AllCourses { get; set; }
        }

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

        private string PathName => Path.Combine(Environment.CurrentDirectory, "CourseData.json");

        private List<ClassEnrollment> totalList = new List<ClassEnrollment>();
        public async Task<List<ClassEnrollment>> GetAllCourses()
        {
            if (totalList.Count == 0)
            {
                await GetData();
            }
            return totalList;
        }

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
        public async Task GetData(bool forceLoad = false, ProgressBar? progressBar = null, Label? label = null)
        {
            if (totalList.Count == 0 || forceLoad)
            {
                totalList.Clear();
                if (progressBar != null)
                {
                    progressBar.Maximum = MaxID;
                    progressBar.Value = 0;
                }

                if (File.Exists(PathName) && !forceLoad)
                {
                    try
                    {
                        using (StreamReader sr = File.OpenText(PathName))
                        {
                            var data = JsonConvert.DeserializeObject<EnrollmentData>(sr.ReadToEnd());
                            totalList = data.AllCourses;
                            SetLastUpdatedText(label, data.LastUpdate);
                        }
                        if (progressBar != null)
                        {
                            progressBar.Value = progressBar.Maximum;
                        }
                    }
                    catch
                    {
                        File.Delete(PathName);
                        totalList.Clear();
                        await GetData();
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
                            if (progressBar != null)
                            {
                                context?.Post(_ => { progressBar.Value += 1; }, null);
                            }
                        }
                    });
                    EnrollmentData enrollmentData = new EnrollmentData()
                    {
                        AllCourses = totalList,
                        LastUpdate = DateTime.Now
                    };

                    SetLastUpdatedText(label, enrollmentData.LastUpdate);
                    SaveJSONData(enrollmentData);
                }
            }
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

        private void SaveJSONData(EnrollmentData enrollmentData)
        {
            string serialize = JsonConvert.SerializeObject(enrollmentData);

            // Check if file already exists. If yes, delete it.
            if (File.Exists(PathName))
            {
                File.Delete(PathName);
            }

            File.WriteAllText(PathName, serialize);
        }
    }
}
