
namespace Cambly_Reports
{
    partial class ReportCreator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCreator));
            this.cmbxStudentName = new System.Windows.Forms.ComboBox();
            this.rtxbxGrammar = new System.Windows.Forms.RichTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txbxDate = new System.Windows.Forms.TextBox();
            this.btnTodaysDate = new System.Windows.Forms.Button();
            this.txbxTopic = new System.Windows.Forms.TextBox();
            this.lblTopic = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStudentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblGrammar = new System.Windows.Forms.Label();
            this.rtxbxVocab = new System.Windows.Forms.RichTextBox();
            this.lblVocab = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.diaSave = new System.Windows.Forms.SaveFileDialog();
            this.calDate = new System.Windows.Forms.MonthCalendar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.studentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbxStudentName
            // 
            resources.ApplyResources(this.cmbxStudentName, "cmbxStudentName");
            this.cmbxStudentName.FormattingEnabled = true;
            this.cmbxStudentName.Name = "cmbxStudentName";
            // 
            // rtxbxGrammar
            // 
            resources.ApplyResources(this.rtxbxGrammar, "rtxbxGrammar");
            this.rtxbxGrammar.Name = "rtxbxGrammar";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblDate
            // 
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Name = "lblDate";
            // 
            // txbxDate
            // 
            resources.ApplyResources(this.txbxDate, "txbxDate");
            this.txbxDate.Name = "txbxDate";
            this.txbxDate.Click += new System.EventHandler(this.txbxDate_Click);
            // 
            // btnTodaysDate
            // 
            resources.ApplyResources(this.btnTodaysDate, "btnTodaysDate");
            this.btnTodaysDate.Name = "btnTodaysDate";
            this.btnTodaysDate.UseVisualStyleBackColor = true;
            this.btnTodaysDate.Click += new System.EventHandler(this.TodaysDate_Click);
            // 
            // txbxTopic
            // 
            resources.ApplyResources(this.txbxTopic, "txbxTopic");
            this.txbxTopic.Name = "txbxTopic";
            // 
            // lblTopic
            // 
            resources.ApplyResources(this.lblTopic, "lblTopic");
            this.lblTopic.Name = "lblTopic";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.viewStudentListToolStripMenuItem,
            this.studentListToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // msFile
            // 
            resources.ApplyResources(this.msFile, "msFile");
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewStudent,
            this.tsmiRestart,
            this.closeToolStripMenuItem});
            this.msFile.Name = "msFile";
            // 
            // tsmiNewStudent
            // 
            resources.ApplyResources(this.tsmiNewStudent, "tsmiNewStudent");
            this.tsmiNewStudent.Name = "tsmiNewStudent";
            this.tsmiNewStudent.Click += new System.EventHandler(this.tsmiNewStudent_Click);
            // 
            // tsmiRestart
            // 
            resources.ApplyResources(this.tsmiRestart, "tsmiRestart");
            this.tsmiRestart.Name = "tsmiRestart";
            this.tsmiRestart.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewStudentListToolStripMenuItem
            // 
            resources.ApplyResources(this.viewStudentListToolStripMenuItem, "viewStudentListToolStripMenuItem");
            this.viewStudentListToolStripMenuItem.Name = "viewStudentListToolStripMenuItem";
            this.viewStudentListToolStripMenuItem.Click += new System.EventHandler(this.viewStudentListToolStripMenuItem_Click);
            // 
            // lblGrammar
            // 
            resources.ApplyResources(this.lblGrammar, "lblGrammar");
            this.lblGrammar.Name = "lblGrammar";
            // 
            // rtxbxVocab
            // 
            resources.ApplyResources(this.rtxbxVocab, "rtxbxVocab");
            this.rtxbxVocab.Name = "rtxbxVocab";
            // 
            // lblVocab
            // 
            resources.ApplyResources(this.lblVocab, "lblVocab");
            this.lblVocab.Name = "lblVocab";
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExport.FlatAppearance.BorderSize = 12;
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // diaSave
            // 
            resources.ApplyResources(this.diaSave, "diaSave");
            // 
            // calDate
            // 
            resources.ApplyResources(this.calDate, "calDate");
            this.calDate.Name = "calDate";
            this.calDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calDate_DateChanged);
            this.calDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calDate_DateSelected);
            this.calDate.Leave += new System.EventHandler(this.calDate_Leave);
            this.calDate.MouseLeave += new System.EventHandler(this.calDate_MouseLeave);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lblTopic);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.cmbxStudentName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.txbxTopic);
            this.groupBox1.Controls.Add(this.btnTodaysDate);
            this.groupBox1.Controls.Add(this.txbxDate);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // studentListToolStripMenuItem
            // 
            resources.ApplyResources(this.studentListToolStripMenuItem, "studentListToolStripMenuItem");
            this.studentListToolStripMenuItem.Name = "studentListToolStripMenuItem";
            this.studentListToolStripMenuItem.Click += new System.EventHandler(this.studentListToolStripMenuItem_Click);
            // 
            // ReportCreator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.calDate);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblVocab);
            this.Controls.Add(this.rtxbxVocab);
            this.Controls.Add(this.lblGrammar);
            this.Controls.Add(this.rtxbxGrammar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ReportCreator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportCreator_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportCreator_FormClosed);
            this.Load += new System.EventHandler(this.ReportCreator_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbxStudentName;
        private System.Windows.Forms.RichTextBox rtxbxGrammar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txbxDate;
        private System.Windows.Forms.Button btnTodaysDate;
        private System.Windows.Forms.TextBox txbxTopic;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem msFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewStudent;
        private System.Windows.Forms.Label lblGrammar;
        private System.Windows.Forms.RichTextBox rtxbxVocab;
        private System.Windows.Forms.Label lblVocab;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog diaSave;
        private System.Windows.Forms.MonthCalendar calDate;
        private System.Windows.Forms.ToolStripMenuItem tsmiRestart;
        private System.Windows.Forms.ToolStripMenuItem viewStudentListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem studentListToolStripMenuItem;
    }
}

