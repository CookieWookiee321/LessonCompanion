
namespace Cambly_Reports
{
    partial class LessonList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LessonList));
            this.lbStudentList = new System.Windows.Forms.ListBox();
            this.dgvTopicList = new System.Windows.Forms.DataGridView();
            this.lblLessonCount = new System.Windows.Forms.Label();
            this.chbRecent = new System.Windows.Forms.CheckBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopicList)).BeginInit();
            this.SuspendLayout();
            // 
            // lbStudentList
            // 
            this.lbStudentList.FormattingEnabled = true;
            this.lbStudentList.ItemHeight = 20;
            this.lbStudentList.Location = new System.Drawing.Point(12, 45);
            this.lbStudentList.Name = "lbStudentList";
            this.lbStudentList.Size = new System.Drawing.Size(150, 364);
            this.lbStudentList.TabIndex = 2;
            this.lbStudentList.SelectedIndexChanged += new System.EventHandler(this.lbStudentList_SelectedIndexChanged);
            // 
            // dgvTopicList
            // 
            this.dgvTopicList.AllowUserToAddRows = false;
            this.dgvTopicList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dgvTopicList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopicList.Location = new System.Drawing.Point(168, 45);
            this.dgvTopicList.Name = "dgvTopicList";
            this.dgvTopicList.RowHeadersWidth = 51;
            this.dgvTopicList.RowTemplate.Height = 29;
            this.dgvTopicList.Size = new System.Drawing.Size(525, 392);
            this.dgvTopicList.TabIndex = 3;
            // 
            // lblLessonCount
            // 
            this.lblLessonCount.AutoSize = true;
            this.lblLessonCount.Location = new System.Drawing.Point(13, 417);
            this.lblLessonCount.Name = "lblLessonCount";
            this.lblLessonCount.Size = new System.Drawing.Size(0, 20);
            this.lblLessonCount.TabIndex = 4;
            // 
            // chbRecent
            // 
            this.chbRecent.AutoSize = true;
            this.chbRecent.Location = new System.Drawing.Point(12, 12);
            this.chbRecent.Name = "chbRecent";
            this.chbRecent.Size = new System.Drawing.Size(106, 24);
            this.chbRecent.TabIndex = 5;
            this.chbRecent.Text = "Only recent";
            this.chbRecent.UseVisualStyleBackColor = true;
            this.chbRecent.CheckedChanged += new System.EventHandler(this.chbRecent_CheckedChanged);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(518, 12);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(175, 27);
            this.tbSearch.TabIndex = 6;
            this.tbSearch.Text = "Search...";
            this.tbSearch.Click += new System.EventHandler(this.tbSearch_Click);
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // LessonList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 454);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.chbRecent);
            this.Controls.Add(this.lblLessonCount);
            this.Controls.Add(this.dgvTopicList);
            this.Controls.Add(this.lbStudentList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LessonList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lesson List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopicList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lbStudentList;
        private System.Windows.Forms.DataGridView dgvTopicList;
        private System.Windows.Forms.Label lblLessonCount;
        private System.Windows.Forms.CheckBox chbRecent;
        private System.Windows.Forms.TextBox tbSearch;
    }
}