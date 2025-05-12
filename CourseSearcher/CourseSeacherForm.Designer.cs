namespace CourseSearcher
{
    partial class CourseSeacherForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseSeacherForm));
            btn1 = new Button();
            pbWeb = new ProgressBar();
            label1 = new Label();
            comboBox1 = new ComboBox();
            checkBoxIncludeClosed = new CheckBox();
            refreshCheckBox = new CheckBox();
            lastUpdateLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            searchLabel = new Label();
            enrollmentTextBox = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel2 = new Panel();
            panel3 = new Panel();
            dataPanel = new Panel();
            splitContainer1 = new SplitContainer();
            gridView = new DataGridView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            schoolsToolStripMenuItem = new ToolStripMenuItem();
            createTSM = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            showToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
            menuStrip1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn1.Location = new Point(2, 82);
            btn1.Margin = new Padding(2);
            btn1.Name = "btn1";
            btn1.Size = new Size(274, 26);
            btn1.TabIndex = 1;
            btn1.Text = "Search";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += button1_Click;
            // 
            // pbWeb
            // 
            pbWeb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbWeb.Location = new Point(2, 112);
            pbWeb.Margin = new Padding(2);
            pbWeb.Name = "pbWeb";
            pbWeb.Size = new Size(274, 22);
            pbWeb.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Record Data";
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Bottom;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0, 21);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(272, 23);
            comboBox1.TabIndex = 1;
            comboBox1.DropDown += comboBox1_DropDown;
            // 
            // checkBoxIncludeClosed
            // 
            checkBoxIncludeClosed.AutoSize = true;
            checkBoxIncludeClosed.Dock = DockStyle.Right;
            checkBoxIncludeClosed.Location = new Point(168, 0);
            checkBoxIncludeClosed.Margin = new Padding(4, 3, 4, 3);
            checkBoxIncludeClosed.Name = "checkBoxIncludeClosed";
            checkBoxIncludeClosed.Size = new Size(104, 24);
            checkBoxIncludeClosed.TabIndex = 0;
            checkBoxIncludeClosed.TabStop = false;
            checkBoxIncludeClosed.Text = "Include Closed";
            checkBoxIncludeClosed.UseVisualStyleBackColor = true;
            // 
            // refreshCheckBox
            // 
            refreshCheckBox.AutoSize = true;
            refreshCheckBox.Dock = DockStyle.Left;
            refreshCheckBox.Location = new Point(0, 0);
            refreshCheckBox.Margin = new Padding(4, 3, 4, 3);
            refreshCheckBox.Name = "refreshCheckBox";
            refreshCheckBox.Size = new Size(103, 24);
            refreshCheckBox.TabIndex = 0;
            refreshCheckBox.TabStop = false;
            refreshCheckBox.Text = "Refresh Search";
            refreshCheckBox.UseVisualStyleBackColor = true;
            // 
            // lastUpdateLabel
            // 
            lastUpdateLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lastUpdateLabel.Location = new Point(302, 13);
            lastUpdateLabel.Name = "lastUpdateLabel";
            lastUpdateLabel.Size = new Size(263, 23);
            lastUpdateLabel.TabIndex = 1;
            lastUpdateLabel.Text = "Last Update: AAAAAAAA";
            lastUpdateLabel.TextAlign = ContentAlignment.MiddleRight;
            lastUpdateLabel.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 292F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(dataPanel, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(884, 537);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(searchLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(enrollmentTextBox, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel2.Location = new Point(4, 3);
            tableLayoutPanel2.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel2.Size = new Size(284, 531);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // searchLabel
            // 
            searchLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            searchLabel.AutoSize = true;
            searchLabel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            searchLabel.Location = new Point(2, 0);
            searchLabel.Margin = new Padding(2, 0, 2, 0);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new Size(280, 46);
            searchLabel.TabIndex = 0;
            searchLabel.Text = "Course/s to Search";
            searchLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // enrollmentTextBox
            // 
            enrollmentTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            enrollmentTextBox.BorderStyle = BorderStyle.FixedSingle;
            enrollmentTextBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            enrollmentTextBox.Location = new Point(2, 48);
            enrollmentTextBox.Margin = new Padding(2);
            enrollmentTextBox.Multiline = true;
            enrollmentTextBox.Name = "enrollmentTextBox";
            enrollmentTextBox.Size = new Size(280, 304);
            enrollmentTextBox.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(btn1, 0, 2);
            tableLayoutPanel3.Controls.Add(panel2, 0, 0);
            tableLayoutPanel3.Controls.Add(pbWeb, 0, 3);
            tableLayoutPanel3.Controls.Add(panel3, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 357);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.Size = new Size(278, 171);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(comboBox1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(272, 44);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Controls.Add(refreshCheckBox);
            panel3.Controls.Add(checkBoxIncludeClosed);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 53);
            panel3.Name = "panel3";
            panel3.Size = new Size(272, 24);
            panel3.TabIndex = 4;
            // 
            // dataPanel
            // 
            dataPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataPanel.Controls.Add(splitContainer1);
            dataPanel.Location = new Point(314, 2);
            dataPanel.Margin = new Padding(2);
            dataPanel.Name = "dataPanel";
            dataPanel.Size = new Size(568, 533);
            dataPanel.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lastUpdateLabel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(gridView);
            splitContainer1.Size = new Size(568, 533);
            splitContainer1.SplitterDistance = 47;
            splitContainer1.TabIndex = 3;
            splitContainer1.TabStop = false;
            // 
            // gridView
            // 
            gridView.AllowDrop = true;
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToOrderColumns = true;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridView.BackgroundColor = SystemColors.ButtonFace;
            gridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridView.Dock = DockStyle.Fill;
            gridView.Location = new Point(0, 0);
            gridView.Margin = new Padding(2);
            gridView.Name = "gridView";
            gridView.ReadOnly = true;
            gridView.RowHeadersWidth = 51;
            gridView.RowTemplate.Height = 24;
            gridView.Size = new Size(568, 482);
            gridView.TabIndex = 2;
            gridView.MouseDown += gridView_MouseDown;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(884, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { schoolsToolStripMenuItem, createTSM, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // schoolsToolStripMenuItem
            // 
            schoolsToolStripMenuItem.Name = "schoolsToolStripMenuItem";
            schoolsToolStripMenuItem.Size = new Size(144, 22);
            schoolsToolStripMenuItem.Text = "Filter Schools";
            schoolsToolStripMenuItem.Click += schoolsToolStripMenuItem_Click;
            // 
            // createTSM
            // 
            createTSM.Name = "createTSM";
            createTSM.Size = new Size(144, 22);
            createTSM.Text = "Scheduler";
            createTSM.Click += createTSM_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(144, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { showToolStripMenuItem, toolStripMenuItem1 });
            contextMenuStrip2.Name = "contextMenuStrip1";
            contextMenuStrip2.Size = new Size(104, 48);
            contextMenuStrip2.ItemClicked += contextMenuStrip2_ItemClicked;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Enabled = false;
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(103, 22);
            showToolStripMenuItem.Text = "Show";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(103, 22);
            toolStripMenuItem1.Text = "Hide";
            // 
            // CourseSeacherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(884, 561);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            MinimumSize = new Size(900, 600);
            Name = "CourseSeacherForm";
            Text = "Course Searcher";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            dataPanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridView).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.ProgressBar pbWeb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox refreshCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox enrollmentTextBox;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.DataGridView gridView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Label lastUpdateLabel;
        private ToolStripMenuItem schoolsToolStripMenuItem;
        private CheckBox checkBoxIncludeClosed;
        private SplitContainer splitContainer1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem showToolStripMenuItem;
        private ComboBox comboBox1;
        private Label label1;
        private ToolStripMenuItem createTSM;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel2;
        private Panel panel3;
    }
}

