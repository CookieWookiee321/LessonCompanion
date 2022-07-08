
namespace LessonCompanion
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvNewLanguage = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblVocab = new System.Windows.Forms.Label();
            this.dgvPronunciation = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvCorrections = new System.Windows.Forms.DataGridView();
            this.colCorOrig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCorrNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGrammar = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.diaSave = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lHomework = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTopic = new System.Windows.Forms.Label();
            this.tTopic = new System.Windows.Forms.TextBox();
            this.tHomework = new System.Windows.Forms.TextBox();
            this.cbStudentName = new System.Windows.Forms.ComboBox();
            this.tDate = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbReport = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dialogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lessonListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newWindowStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsDefaultOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFormOnSubmitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPronunciation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorrections)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.dgvNewLanguage);
            this.splitContainer1.Panel1.Controls.Add(this.lblVocab);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.dgvPronunciation);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            // 
            // dgvNewLanguage
            // 
            resources.ApplyResources(this.dgvNewLanguage, "dgvNewLanguage");
            this.dgvNewLanguage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNewLanguage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewLanguage.ColumnHeadersVisible = false;
            this.dgvNewLanguage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvNewLanguage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvNewLanguage.Name = "dgvNewLanguage";
            this.dgvNewLanguage.RowHeadersVisible = false;
            this.dgvNewLanguage.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dgvNewLanguage.RowTemplate.Height = 29;
            this.dgvNewLanguage.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvNewLanguage_EditingControlShowing);
            this.dgvNewLanguage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvNewLanguage_KeyUp);
            this.dgvNewLanguage.Leave += new System.EventHandler(this.dgvNewLanguage_Leave);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // lblVocab
            // 
            resources.ApplyResources(this.lblVocab, "lblVocab");
            this.lblVocab.Name = "lblVocab";
            // 
            // dgvPronunciation
            // 
            resources.ApplyResources(this.dgvPronunciation, "dgvPronunciation");
            this.dgvPronunciation.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPronunciation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPronunciation.ColumnHeadersVisible = false;
            this.dgvPronunciation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvPronunciation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPronunciation.Name = "dgvPronunciation";
            this.dgvPronunciation.RowHeadersVisible = false;
            this.dgvPronunciation.RowTemplate.Height = 29;
            this.dgvPronunciation.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvPronunciation_EditingControlShowing);
            this.dgvPronunciation.Leave += new System.EventHandler(this.dgvPronunciation_Leave);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            this.splitContainer2.Panel2.Controls.Add(this.dgvCorrections);
            this.splitContainer2.Panel2.Controls.Add(this.lblGrammar);
            // 
            // dgvCorrections
            // 
            resources.ApplyResources(this.dgvCorrections, "dgvCorrections");
            this.dgvCorrections.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCorrections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorrections.ColumnHeadersVisible = false;
            this.dgvCorrections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCorOrig,
            this.colCorrNew});
            this.dgvCorrections.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCorrections.Name = "dgvCorrections";
            this.dgvCorrections.RowHeadersVisible = false;
            this.dgvCorrections.RowTemplate.Height = 29;
            this.dgvCorrections.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCorrections_EditingControlShowing);
            this.dgvCorrections.Leave += new System.EventHandler(this.dgvCorrections_Leave);
            // 
            // colCorOrig
            // 
            this.colCorOrig.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colCorOrig.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.colCorOrig, "colCorOrig");
            this.colCorOrig.Name = "colCorOrig";
            // 
            // colCorrNew
            // 
            this.colCorrNew.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colCorrNew.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.colCorrNew, "colCorrNew");
            this.colCorrNew.Name = "colCorrNew";
            // 
            // lblGrammar
            // 
            resources.ApplyResources(this.lblGrammar, "lblGrammar");
            this.lblGrammar.Name = "lblGrammar";
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lHomework, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTopic, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tTopic, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tHomework, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbStudentName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tDate, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lHomework
            // 
            resources.ApplyResources(this.lHomework, "lHomework");
            this.lHomework.Name = "lHomework";
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
            // lblTopic
            // 
            resources.ApplyResources(this.lblTopic, "lblTopic");
            this.lblTopic.Name = "lblTopic";
            // 
            // tTopic
            // 
            resources.ApplyResources(this.tTopic, "tTopic");
            this.tTopic.Name = "tTopic";
            // 
            // tHomework
            // 
            resources.ApplyResources(this.tHomework, "tHomework");
            this.tHomework.Name = "tHomework";
            // 
            // cbStudentName
            // 
            resources.ApplyResources(this.cbStudentName, "cbStudentName");
            this.cbStudentName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudentName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbStudentName.FormattingEnabled = true;
            this.cbStudentName.Name = "cbStudentName";
            this.cbStudentName.TextChanged += new System.EventHandler(this.cbStudentName_TextChanged);
            this.cbStudentName.Leave += new System.EventHandler(this.cbStudentName_Leave);
            // 
            // tDate
            // 
            resources.ApplyResources(this.tDate, "tDate");
            this.tDate.Name = "tDate";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.splitContainer2);
            this.groupBox2.Controls.Add(this.chbReport);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // chbReport
            // 
            resources.ApplyResources(this.chbReport, "chbReport");
            this.chbReport.Checked = true;
            this.chbReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbReport.Name = "chbReport";
            this.chbReport.UseVisualStyleBackColor = true;
            this.chbReport.CheckedChanged += new System.EventHandler(this.chbReportOn_CheckedChanged);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialogsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // dialogsToolStripMenuItem
            // 
            resources.ApplyResources(this.dialogsToolStripMenuItem, "dialogsToolStripMenuItem");
            this.dialogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lessonListToolStripMenuItem,
            this.studentListToolStripMenuItem,
            this.reportHistoryToolStripMenuItem,
            this.dictionaryToolStripMenuItem,
            this.toolStripSeparator1,
            this.newWindowStripMenuItem});
            this.dialogsToolStripMenuItem.Name = "dialogsToolStripMenuItem";
            // 
            // lessonListToolStripMenuItem
            // 
            resources.ApplyResources(this.lessonListToolStripMenuItem, "lessonListToolStripMenuItem");
            this.lessonListToolStripMenuItem.Name = "lessonListToolStripMenuItem";
            this.lessonListToolStripMenuItem.Click += new System.EventHandler(this.lessonListToolStripMenuItem_Click);
            // 
            // studentListToolStripMenuItem
            // 
            resources.ApplyResources(this.studentListToolStripMenuItem, "studentListToolStripMenuItem");
            this.studentListToolStripMenuItem.Name = "studentListToolStripMenuItem";
            // 
            // reportHistoryToolStripMenuItem
            // 
            resources.ApplyResources(this.reportHistoryToolStripMenuItem, "reportHistoryToolStripMenuItem");
            this.reportHistoryToolStripMenuItem.Name = "reportHistoryToolStripMenuItem";
            // 
            // dictionaryToolStripMenuItem
            // 
            resources.ApplyResources(this.dictionaryToolStripMenuItem, "dictionaryToolStripMenuItem");
            this.dictionaryToolStripMenuItem.Name = "dictionaryToolStripMenuItem";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // newWindowStripMenuItem
            // 
            resources.ApplyResources(this.newWindowStripMenuItem, "newWindowStripMenuItem");
            this.newWindowStripMenuItem.Name = "newWindowStripMenuItem";
            this.newWindowStripMenuItem.Click += new System.EventHandler(this.newWindowStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.reportsDefaultOnToolStripMenuItem,
            this.clearFormOnSubmitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            resources.ApplyResources(this.alwaysOnTopToolStripMenuItem, "alwaysOnTopToolStripMenuItem");
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_CheckStateChanged);
            // 
            // reportsDefaultOnToolStripMenuItem
            // 
            resources.ApplyResources(this.reportsDefaultOnToolStripMenuItem, "reportsDefaultOnToolStripMenuItem");
            this.reportsDefaultOnToolStripMenuItem.Checked = true;
            this.reportsDefaultOnToolStripMenuItem.CheckOnClick = true;
            this.reportsDefaultOnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reportsDefaultOnToolStripMenuItem.Name = "reportsDefaultOnToolStripMenuItem";
            // 
            // clearFormOnSubmitToolStripMenuItem
            // 
            resources.ApplyResources(this.clearFormOnSubmitToolStripMenuItem, "clearFormOnSubmitToolStripMenuItem");
            this.clearFormOnSubmitToolStripMenuItem.CheckOnClick = true;
            this.clearFormOnSubmitToolStripMenuItem.Name = "clearFormOnSubmitToolStripMenuItem";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPronunciation)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorrections)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblGrammar;
        private System.Windows.Forms.Label lblVocab;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog diaSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dialogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lessonListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dictionaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsDefaultOnToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvNewLanguage;
        private System.Windows.Forms.DataGridView dgvPronunciation;
        private System.Windows.Forms.DataGridView dgvCorrections;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCorNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem newWindowStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lHomework;
        private System.Windows.Forms.ComboBox cbStudentName;
        private System.Windows.Forms.TextBox tDate;
        private System.Windows.Forms.TextBox tTopic;
        private System.Windows.Forms.TextBox tHomework;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCorOrig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCorrNew;
        private System.Windows.Forms.ToolStripMenuItem clearFormOnSubmitToolStripMenuItem;
    }
}

