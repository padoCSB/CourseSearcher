using CourseSearcher.DataHelpers;

namespace CourseSearcher
{
    public partial class SaveForm : Form
    {
        public SaveForm()
        {
            blockedTimeList = new List<BlockedTime>();
            InitializeComponent();
            saveBtn.Enabled = false;
            saveCloseBtn.Enabled = false;
        }
        private List<BlockedTime> blockedTimeList;
        public SaveForm(List<BlockedTime> blockedTimeList) : this()
        {
            this.blockedTimeList = blockedTimeList;
        }

        private bool SaveRecord()
        {
            List<BlockedTime> records = blockedTimeList;

            string name = nameTextBox.Text.Trim();
            if (Recorder.Instance.NameExist(name))
            {
                DialogResult result = MessageBox.Show($"Overwrite {name} record?", "Overwrite", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    Recorder.Instance.RemoveData(name);
                }
            }

            RecordData data = new RecordData()
            {
                Name = name,
                BlockedTimes = records
            };
            Recorder.Instance.AddRecordData(data);
            return true;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = ((System.Windows.Forms.TextBox)sender).Text;
            bool enable = !string.IsNullOrEmpty(text);
            saveBtn.Enabled = enable;
            saveCloseBtn.Enabled = enable;
        }

        private void saveCloseBtn_Click(object sender, EventArgs e)
        {
            if (SaveRecord())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
