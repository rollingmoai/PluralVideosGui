namespace PluralVideosGui
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.courseListView = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.readButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.coursePathTextBox = new System.Windows.Forms.TextBox();
            this.dbPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.coursePathButton = new System.Windows.Forms.Button();
            this.dbPathButton = new System.Windows.Forms.Button();
            this.optionPanel = new System.Windows.Forms.Panel();
            this.copyImageCheckbox = new System.Windows.Forms.CheckBox();
            this.deselectAllButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.openOutputButton = new System.Windows.Forms.Button();
            this.openDbButton = new System.Windows.Forms.Button();
            this.deleteCheckBox = new System.Windows.Forms.CheckBox();
            this.createSubCheckBox = new System.Windows.Forms.CheckBox();
            this.outputButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.outputPathTextBox = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlCourse = new System.Windows.Forms.Panel();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.bgwDecrypt = new System.ComponentModel.BackgroundWorker();
            this.bgwGetCourse = new System.ComponentModel.BackgroundWorker();
            this.formLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.formToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bottomStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tslToolVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslPOPVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.tlsHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.optionPanel.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCourse.SuspendLayout();
            this.formLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.bottomStatusStrip.SuspendLayout();
            this.pnl1.SuspendLayout();
            this.pnl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // courseListView
            // 
            this.courseListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.courseListView.BackColor = System.Drawing.SystemColors.Control;
            this.courseListView.CheckBoxes = true;
            this.courseListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.courseListView.HideSelection = false;
            this.courseListView.Location = new System.Drawing.Point(0, 0);
            this.courseListView.Margin = new System.Windows.Forms.Padding(4);
            this.courseListView.MultiSelect = false;
            this.courseListView.Name = "courseListView";
            this.courseListView.Size = new System.Drawing.Size(863, 343);
            this.courseListView.TabIndex = 0;
            this.courseListView.UseCompatibleStateImageBehavior = false;
            this.courseListView.ItemActivate += new System.EventHandler(this.courseListView_ItemActivate);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new System.Drawing.Size(16, 16);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(751, 36);
            this.readButton.Margin = new System.Windows.Forms.Padding(4);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(77, 23);
            this.readButton.TabIndex = 12;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(751, 63);
            this.runButton.Margin = new System.Windows.Forms.Padding(4);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(77, 23);
            this.runButton.TabIndex = 15;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // coursePathTextBox
            // 
            this.coursePathTextBox.Location = new System.Drawing.Point(110, 9);
            this.coursePathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.coursePathTextBox.Name = "coursePathTextBox";
            this.coursePathTextBox.Size = new System.Drawing.Size(381, 23);
            this.coursePathTextBox.TabIndex = 0;
            // 
            // dbPathTextBox
            // 
            this.dbPathTextBox.Location = new System.Drawing.Point(110, 36);
            this.dbPathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dbPathTextBox.Name = "dbPathTextBox";
            this.dbPathTextBox.Size = new System.Drawing.Size(381, 23);
            this.dbPathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Course path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "DB path:";
            // 
            // coursePathButton
            // 
            this.coursePathButton.Location = new System.Drawing.Point(498, 9);
            this.coursePathButton.Margin = new System.Windows.Forms.Padding(4);
            this.coursePathButton.Name = "coursePathButton";
            this.coursePathButton.Size = new System.Drawing.Size(77, 23);
            this.coursePathButton.TabIndex = 3;
            this.coursePathButton.Text = "Browse...";
            this.coursePathButton.UseVisualStyleBackColor = true;
            this.coursePathButton.Click += new System.EventHandler(this.coursePathButton_Click);
            // 
            // dbPathButton
            // 
            this.dbPathButton.Location = new System.Drawing.Point(498, 36);
            this.dbPathButton.Margin = new System.Windows.Forms.Padding(4);
            this.dbPathButton.Name = "dbPathButton";
            this.dbPathButton.Size = new System.Drawing.Size(77, 23);
            this.dbPathButton.TabIndex = 4;
            this.dbPathButton.Text = "Browse...";
            this.dbPathButton.UseVisualStyleBackColor = true;
            this.dbPathButton.Click += new System.EventHandler(this.dbPathButton_Click);
            // 
            // optionPanel
            // 
            this.optionPanel.Controls.Add(this.copyImageCheckbox);
            this.optionPanel.Controls.Add(this.deselectAllButton);
            this.optionPanel.Controls.Add(this.selectAllButton);
            this.optionPanel.Controls.Add(this.openOutputButton);
            this.optionPanel.Controls.Add(this.openDbButton);
            this.optionPanel.Controls.Add(this.deleteCheckBox);
            this.optionPanel.Controls.Add(this.createSubCheckBox);
            this.optionPanel.Controls.Add(this.runButton);
            this.optionPanel.Controls.Add(this.outputButton);
            this.optionPanel.Controls.Add(this.readButton);
            this.optionPanel.Controls.Add(this.label3);
            this.optionPanel.Controls.Add(this.outputPathTextBox);
            this.optionPanel.Controls.Add(this.coursePathButton);
            this.optionPanel.Controls.Add(this.dbPathButton);
            this.optionPanel.Controls.Add(this.coursePathTextBox);
            this.optionPanel.Controls.Add(this.label2);
            this.optionPanel.Controls.Add(this.dbPathTextBox);
            this.optionPanel.Controls.Add(this.label1);
            this.optionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.optionPanel.Location = new System.Drawing.Point(0, 343);
            this.optionPanel.Margin = new System.Windows.Forms.Padding(4);
            this.optionPanel.MinimumSize = new System.Drawing.Size(817, 125);
            this.optionPanel.Name = "optionPanel";
            this.optionPanel.Size = new System.Drawing.Size(863, 153);
            this.optionPanel.TabIndex = 1;
            // 
            // copyImageCheckbox
            // 
            this.copyImageCheckbox.AutoSize = true;
            this.copyImageCheckbox.Location = new System.Drawing.Point(744, 9);
            this.copyImageCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.copyImageCheckbox.Name = "copyImageCheckbox";
            this.copyImageCheckbox.Size = new System.Drawing.Size(90, 19);
            this.copyImageCheckbox.TabIndex = 18;
            this.copyImageCheckbox.Text = "Copy Image";
            this.copyImageCheckbox.UseVisualStyleBackColor = true;
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Location = new System.Drawing.Point(666, 63);
            this.deselectAllButton.Margin = new System.Windows.Forms.Padding(4);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(77, 23);
            this.deselectAllButton.TabIndex = 14;
            this.deselectAllButton.Text = "Deselect all";
            this.deselectAllButton.UseVisualStyleBackColor = true;
            this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(666, 36);
            this.selectAllButton.Margin = new System.Windows.Forms.Padding(4);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(77, 23);
            this.selectAllButton.TabIndex = 13;
            this.selectAllButton.Text = "Select all";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // openOutputButton
            // 
            this.openOutputButton.Location = new System.Drawing.Point(581, 63);
            this.openOutputButton.Margin = new System.Windows.Forms.Padding(4);
            this.openOutputButton.Name = "openOutputButton";
            this.openOutputButton.Size = new System.Drawing.Size(77, 23);
            this.openOutputButton.TabIndex = 16;
            this.openOutputButton.Text = "Open folder";
            this.openOutputButton.UseVisualStyleBackColor = true;
            this.openOutputButton.Click += new System.EventHandler(this.openOutputButton_Click);
            // 
            // openDbButton
            // 
            this.openDbButton.Location = new System.Drawing.Point(581, 36);
            this.openDbButton.Margin = new System.Windows.Forms.Padding(4);
            this.openDbButton.Name = "openDbButton";
            this.openDbButton.Size = new System.Drawing.Size(77, 23);
            this.openDbButton.TabIndex = 17;
            this.openDbButton.Text = "Open";
            this.openDbButton.UseVisualStyleBackColor = true;
            this.openDbButton.Click += new System.EventHandler(this.openDbButton_Click);
            // 
            // deleteCheckBox
            // 
            this.deleteCheckBox.AutoSize = true;
            this.deleteCheckBox.Checked = true;
            this.deleteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteCheckBox.Location = new System.Drawing.Point(675, 10);
            this.deleteCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.deleteCheckBox.Name = "deleteCheckBox";
            this.deleteCheckBox.Size = new System.Drawing.Size(59, 19);
            this.deleteCheckBox.TabIndex = 8;
            this.deleteCheckBox.Text = "Delete";
            this.formToolTip.SetToolTip(this.deleteCheckBox, "Delete course after decrypting");
            this.deleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // createSubCheckBox
            // 
            this.createSubCheckBox.AutoSize = true;
            this.createSubCheckBox.Checked = true;
            this.createSubCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createSubCheckBox.Location = new System.Drawing.Point(578, 10);
            this.createSubCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.createSubCheckBox.Name = "createSubCheckBox";
            this.createSubCheckBox.Size = new System.Drawing.Size(82, 19);
            this.createSubCheckBox.TabIndex = 7;
            this.createSubCheckBox.Text = "Create sub";
            this.formToolTip.SetToolTip(this.createSubCheckBox, "Create subtitles from course, if available");
            this.createSubCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputButton
            // 
            this.outputButton.Location = new System.Drawing.Point(498, 63);
            this.outputButton.Margin = new System.Windows.Forms.Padding(4);
            this.outputButton.Name = "outputButton";
            this.outputButton.Size = new System.Drawing.Size(77, 23);
            this.outputButton.TabIndex = 5;
            this.outputButton.Text = "Browse...";
            this.outputButton.UseVisualStyleBackColor = true;
            this.outputButton.Click += new System.EventHandler(this.outputPathButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output:";
            // 
            // outputPathTextBox
            // 
            this.outputPathTextBox.Location = new System.Drawing.Point(110, 63);
            this.outputPathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputPathTextBox.Name = "outputPathTextBox";
            this.outputPathTextBox.Size = new System.Drawing.Size(381, 23);
            this.outputPathTextBox.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlCourse);
            this.pnlMain.Controls.Add(this.optionPanel);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(4, 4);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMain.MinimumSize = new System.Drawing.Size(0, 496);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(863, 496);
            this.pnlMain.TabIndex = 12;
            // 
            // pnlCourse
            // 
            this.pnlCourse.Controls.Add(this.courseListView);
            this.pnlCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCourse.Location = new System.Drawing.Point(0, 0);
            this.pnlCourse.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCourse.Name = "pnlCourse";
            this.pnlCourse.Size = new System.Drawing.Size(863, 343);
            this.pnlCourse.TabIndex = 11;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.BackColor = System.Drawing.Color.Black;
            this.logRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.logRichTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.logRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.logRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.ShortcutsEnabled = false;
            this.logRichTextBox.ShowSelectionMargin = true;
            this.logRichTextBox.Size = new System.Drawing.Size(466, 429);
            this.logRichTextBox.TabIndex = 1;
            this.logRichTextBox.TabStop = false;
            this.logRichTextBox.Text = "https://github.com/naivey/PluralVideosGui";
            this.logRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.logRichTextBox_LinkClicked);
            // 
            // bgwDecrypt
            // 
            this.bgwDecrypt.WorkerReportsProgress = true;
            this.bgwDecrypt.WorkerSupportsCancellation = true;
            // 
            // bgwGetCourse
            // 
            this.bgwGetCourse.WorkerReportsProgress = true;
            this.bgwGetCourse.WorkerSupportsCancellation = true;
            // 
            // formLayoutPanel
            // 
            this.formLayoutPanel.ColumnCount = 2;
            this.formLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.formLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.formLayoutPanel.Controls.Add(this.pnlMain, 0, 0);
            this.formLayoutPanel.Controls.Add(this.panel1, 1, 0);
            this.formLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.formLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.formLayoutPanel.Name = "formLayoutPanel";
            this.formLayoutPanel.RowCount = 1;
            this.formLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.formLayoutPanel.Size = new System.Drawing.Size(1342, 435);
            this.formLayoutPanel.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logRichTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(874, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 429);
            this.panel1.TabIndex = 13;
            // 
            // bottomStatusStrip
            // 
            this.bottomStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.bottomStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslToolVersion,
            this.tslPOPVersion});
            this.bottomStatusStrip.Location = new System.Drawing.Point(0, 1);
            this.bottomStatusStrip.Name = "bottomStatusStrip";
            this.bottomStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.bottomStatusStrip.Size = new System.Drawing.Size(1342, 24);
            this.bottomStatusStrip.SizingGrip = false;
            this.bottomStatusStrip.TabIndex = 14;
            this.bottomStatusStrip.Text = "statusStrip1";
            // 
            // tslToolVersion
            // 
            this.tslToolVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tslToolVersion.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tslToolVersion.Name = "tslToolVersion";
            this.tslToolVersion.Size = new System.Drawing.Size(77, 19);
            this.tslToolVersion.Text = "Tool Version:";
            // 
            // tslPOPVersion
            // 
            this.tslPOPVersion.Name = "tslPOPVersion";
            this.tslPOPVersion.Size = new System.Drawing.Size(181, 19);
            this.tslPOPVersion.Text = "Pluralsight Offline Player Version:";
            // 
            // pnl1
            // 
            this.pnl1.Controls.Add(this.formLayoutPanel);
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl1.Location = new System.Drawing.Point(0, 0);
            this.pnl1.Margin = new System.Windows.Forms.Padding(4);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(1342, 435);
            this.pnl1.TabIndex = 15;
            // 
            // pnl2
            // 
            this.pnl2.Controls.Add(this.bottomStatusStrip);
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl2.Location = new System.Drawing.Point(0, 435);
            this.pnl2.Margin = new System.Windows.Forms.Padding(4);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(1342, 25);
            this.pnl2.TabIndex = 16;
            // 
            // tlsHelp
            // 
            this.tlsHelp.Name = "tlsHelp";
            this.tlsHelp.Size = new System.Drawing.Size(23, 23);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1342, 460);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.pnl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "PluralVideosGui";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.optionPanel.ResumeLayout(false);
            this.optionPanel.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlCourse.ResumeLayout(false);
            this.formLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.bottomStatusStrip.ResumeLayout(false);
            this.bottomStatusStrip.PerformLayout();
            this.pnl1.ResumeLayout(false);
            this.pnl2.ResumeLayout(false);
            this.pnl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView courseListView;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox coursePathTextBox;
        private System.Windows.Forms.TextBox dbPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button coursePathButton;
        private System.Windows.Forms.Button dbPathButton;
        private System.Windows.Forms.Panel optionPanel;
        private System.Windows.Forms.Button outputButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox outputPathTextBox;
        private System.Windows.Forms.CheckBox createSubCheckBox;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlCourse;
        private System.ComponentModel.BackgroundWorker bgwDecrypt;
        private System.ComponentModel.BackgroundWorker bgwGetCourse;
        private System.Windows.Forms.CheckBox deleteCheckBox;
        private System.Windows.Forms.TableLayoutPanel formLayoutPanel;
        private System.Windows.Forms.Button openOutputButton;
        private System.Windows.Forms.Button openDbButton;
        private System.Windows.Forms.Button deselectAllButton;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.ToolTip formToolTip;
        private System.Windows.Forms.CheckBox copyImageCheckbox;
        private System.Windows.Forms.StatusStrip bottomStatusStrip;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.ToolStripStatusLabel tslToolVersion;
        private System.Windows.Forms.ToolStripStatusLabel tslPOPVersion;
        private System.Windows.Forms.ToolStripDropDownButton tlsHelp;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Panel panel1;
    }
}

