namespace CourseSearcher.DataHelpers
{
    public class Recorder : IResettable
    {
        private static Recorder? instance;
        public static Recorder Instance
        {
            get
            {
                if (instance == null)
                    instance = new Recorder();

                return instance;
            }
        }
        public struct RecordDatas
        {
            public List<RecordData> Record { get; set; }
        }

        private RecordDatas data;
        //private static string PathName => Path.Combine(Environment.CurrentDirectory,"Data", "RecordData.json");

        public RecordDatas LoadData(bool forceLoad = false)
        {
            if (data.Record != null && !forceLoad)
                return data;

            data = FileManager.Load<RecordDatas>();
           
            return data;
        }
        private void SaveData()
        {
            FileManager.Save(data);
        }
        public void AddRecordData(RecordData recordData)
        {
            LoadData();
            if (data.Record == null)
                data.Record = new List<RecordData>();

            if (recordData != null)
            {
                data.Record.Add(recordData);
            }
            SaveData();
        }

        public bool NameExist(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            LoadData();
            if (data.Record == null)
                return false;

            return data.Record.Any(x => x.Name == name);
        }

        public List<string> GetAllNames()
        {
            LoadData();
            if (data.Record == null)
                return new List<string>();
            return data.Record.Select(x => x.Name).ToList();
        }

        public RecordData? GetData(string name)
        {
            LoadData();

            if (string.IsNullOrEmpty(name) || !NameExist(name))
            {
                return null;
            }

            return data.Record.SingleOrDefault(x => x.Name == name);
        }

        public bool RemoveData(string name)
        {
            if (!NameExist(name))
            {
                return false;
            }

            data.Record.Remove(data.Record.Single(x => x.Name == name));
            SaveData();
            return true;
        }

        public void Reset()
        {
            FileManager.DeleteFile<RecordDatas>();

            data = new RecordDatas();
            SaveData();
        }
    }
}
