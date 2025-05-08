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
            btn1 = new Button();
            pbWeb = new ProgressBar();
            middlePanel = new Panel();
            checkBoxIncludeClosed = new CheckBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            refreshCheckBox = new CheckBox();
            lastUpdateLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            searchLabel = new Label();
            enrollmentTextBox = new TextBox();
            dataPanel = new Panel();
            splitContainer1 = new SplitContainer();
            gridView = new DataGridView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            schoolsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            showToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            middlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
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
            btn1.Anchor = AnchorStyles.Bottom;
            btn1.Location = new Point(53, 464);
            btn1.Margin = new Padding(2);
            btn1.Name = "btn1";
            btn1.Size = new Size(182, 30);
            btn1.TabIndex = 1;
            btn1.Text = "Search";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += button1_Click;
            // 
            // pbWeb
            // 
            pbWeb.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbWeb.Location = new Point(5, 499);
            pbWeb.Margin = new Padding(2);
            pbWeb.Name = "pbWeb";
            pbWeb.Size = new Size(277, 22);
            pbWeb.TabIndex = 3;
            // 
            // middlePanel
            // 
            middlePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            middlePanel.AutoSize = true;
            middlePanel.Controls.Add(checkBoxIncludeClosed);
            middlePanel.Controls.Add(pictureBox2);
            middlePanel.Controls.Add(pictureBox1);
            middlePanel.Controls.Add(refreshCheckBox);
            middlePanel.Controls.Add(pbWeb);
            middlePanel.Controls.Add(btn1);
            middlePanel.Location = new Point(294, 2);
            middlePanel.Margin = new Padding(2);
            middlePanel.Name = "middlePanel";
            middlePanel.Size = new Size(288, 533);
            middlePanel.TabIndex = 5;
            // 
            // checkBoxIncludeClosed
            // 
            checkBoxIncludeClosed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxIncludeClosed.AutoSize = true;
            checkBoxIncludeClosed.Location = new Point(179, 440);
            checkBoxIncludeClosed.Margin = new Padding(4, 3, 4, 3);
            checkBoxIncludeClosed.Name = "checkBoxIncludeClosed";
            checkBoxIncludeClosed.Size = new Size(104, 19);
            checkBoxIncludeClosed.TabIndex = 10;
            checkBoxIncludeClosed.TabStop = false;
            checkBoxIncludeClosed.Text = "Include Closed";
            checkBoxIncludeClosed.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.GD_Benilde_Logo;
            pictureBox2.Location = new Point(5, 180);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(277, 145);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.smit_logo;
            pictureBox1.Location = new Point(5, 3);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(277, 171);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // refreshCheckBox
            // 
            refreshCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            refreshCheckBox.AutoSize = true;
            refreshCheckBox.Location = new Point(5, 439);
            refreshCheckBox.Margin = new Padding(4, 3, 4, 3);
            refreshCheckBox.Name = "refreshCheckBox";
            refreshCheckBox.Size = new Size(103, 19);
            refreshCheckBox.TabIndex = 5;
            refreshCheckBox.TabStop = false;
            refreshCheckBox.Text = "Refresh Search";
            refreshCheckBox.UseVisualStyleBackColor = true;
            // 
            // lastUpdateLabel
            // 
            lastUpdateLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lastUpdateLabel.Location = new Point(390, 13);
            lastUpdateLabel.Name = "lastUpdateLabel";
            lastUpdateLabel.Size = new Size(263, 23);
            lastUpdateLabel.TabIndex = 9;
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
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 292F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(dataPanel, 2, 0);
            tableLayoutPanel1.Controls.Add(middlePanel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1244, 537);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(searchLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(enrollmentTextBox, 0, 1);
            tableLayoutPanel2.Location = new Point(4, 3);
            tableLayoutPanel2.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(284, 531);
            tableLayoutPanel2.TabIndex = 2;
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
            searchLabel.TabIndex = 1;
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
            enrollmentTextBox.Size = new Size(280, 481);
            enrollmentTextBox.TabIndex = 0;
            // 
            // dataPanel
            // 
            dataPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataPanel.Controls.Add(splitContainer1);
            dataPanel.Location = new Point(586, 2);
            dataPanel.Margin = new Padding(2);
            dataPanel.Name = "dataPanel";
            dataPanel.Size = new Size(656, 533);
            dataPanel.TabIndex = 6;
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
            splitContainer1.Size = new Size(656, 533);
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
            gridView.Size = new Size(656, 482);
            gridView.TabIndex = 2;
            gridView.MouseDown += gridView_MouseDown;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1244, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { schoolsToolStripMenuItem, exitToolStripMenuItem });
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
            ClientSize = new Size(1244, 561);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            MinimumSize = new Size(1000, 600);
            Name = "CourseSeacherForm";
            Text = "Course Searcher";
            middlePanel.ResumeLayout(false);
            middlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.Panel middlePanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox refreshCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox enrollmentTextBox;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private PictureBox pictureBox2;
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
    }
}

