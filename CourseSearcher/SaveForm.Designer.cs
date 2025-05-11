namespace CourseSearcher
{
    partial class SaveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            nameTextBox = new TextBox();
            saveBtn = new Button();
            saveCloseBtn = new Button();
            cancelBtn = new Button();
            SuspendLayout();
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(12, 12);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(272, 23);
            nameTextBox.TabIndex = 0;
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(12, 71);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(118, 23);
            saveBtn.TabIndex = 1;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // saveCloseBtn
            // 
            saveCloseBtn.Location = new Point(12, 41);
            saveCloseBtn.Name = "saveCloseBtn";
            saveCloseBtn.Size = new Size(118, 24);
            saveCloseBtn.TabIndex = 2;
            saveCloseBtn.Text = "Save and Close";
            saveCloseBtn.UseVisualStyleBackColor = true;
            saveCloseBtn.Click += saveCloseBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(166, 42);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(118, 23);
            cancelBtn.TabIndex = 3;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // SaveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(296, 104);
            Controls.Add(cancelBtn);
            Controls.Add(saveCloseBtn);
            Controls.Add(saveBtn);
            Controls.Add(nameTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SaveForm";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SaveForm";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nameTextBox;
        private Button saveBtn;
        private Button saveCloseBtn;
        private Button cancelBtn;
    }
}