namespace Cambly_Reports.UI.Dialogs {
    partial class LessonEditDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tHomework = new System.Windows.Forms.TextBox();
            this.tTopic = new System.Windows.Forms.TextBox();
            this.tDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.bSaveClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.18892F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.81108F));
            this.tableLayoutPanel1.Controls.Add(this.tHomework, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTopic, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tDate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tName, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 191);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tHomework
            // 
            this.tHomework.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tHomework.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tHomework.Location = new System.Drawing.Point(103, 136);
            this.tHomework.Multiline = true;
            this.tHomework.Name = "tHomework";
            this.tHomework.Size = new System.Drawing.Size(291, 52);
            this.tHomework.TabIndex = 8;
            // 
            // tTopic
            // 
            this.tTopic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tTopic.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tTopic.Location = new System.Drawing.Point(103, 79);
            this.tTopic.Multiline = true;
            this.tTopic.Name = "tTopic";
            this.tTopic.Size = new System.Drawing.Size(291, 51);
            this.tTopic.TabIndex = 7;
            // 
            // tDate
            // 
            this.tDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tDate.Font = new System.Drawing.Font("IBM Plex Sans KR SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tDate.Location = new System.Drawing.Point(103, 42);
            this.tDate.Name = "tDate";
            this.tDate.ReadOnly = true;
            this.tDate.Size = new System.Drawing.Size(291, 30);
            this.tDate.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(6, 133);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(91, 27);
            this.label4.TabIndex = 4;
            this.label4.Text = "Homework:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(41, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(56, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(45, 76);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(52, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Topic:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(49, 38);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(48, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date:";
            // 
            // bClose
            // 
            this.bClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bClose.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bClose.Location = new System.Drawing.Point(52, 5);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(94, 29);
            this.bClose.TabIndex = 1;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bSaveClose
            // 
            this.bSaveClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bSaveClose.Font = new System.Drawing.Font("IBM Plex Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bSaveClose.Location = new System.Drawing.Point(233, 5);
            this.bSaveClose.Name = "bSaveClose";
            this.bSaveClose.Size = new System.Drawing.Size(128, 29);
            this.bSaveClose.TabIndex = 2;
            this.bSaveClose.Text = "Save and Close";
            this.bSaveClose.UseVisualStyleBackColor = true;
            this.bSaveClose.Click += new System.EventHandler(this.bSaveClose_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.bClose, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bSaveClose, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 209);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(397, 39);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tName
            // 
            this.tName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tName.Font = new System.Drawing.Font("IBM Plex Sans KR SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tName.Location = new System.Drawing.Point(103, 4);
            this.tName.Name = "tName";
            this.tName.ReadOnly = true;
            this.tName.Size = new System.Drawing.Size(291, 30);
            this.tName.TabIndex = 6;
            // 
            // LessonEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 256);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LessonEditDialog";
            this.Text = "Edit Lesson";
            this.Load += new System.EventHandler(this.LessonEditDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bSaveClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tHomework;
        private System.Windows.Forms.TextBox tTopic;
        private System.Windows.Forms.TextBox tDate;
        private System.Windows.Forms.TextBox tName;
    }
}