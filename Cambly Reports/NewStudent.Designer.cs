
namespace Cambly_Reports
{
    partial class childNewStudent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(childNewStudent));
            this.lblCName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCName = new System.Windows.Forms.TextBox();
            this.tbxCCountry = new System.Windows.Forms.TextBox();
            this.tbxCFirstLesson = new System.Windows.Forms.TextBox();
            this.btnCAdd = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTopic = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.Location = new System.Drawing.Point(12, 9);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(52, 20);
            this.lblCName.TabIndex = 0;
            this.lblCName.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "First Lesson:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Country:";
            // 
            // tbxCName
            // 
            this.tbxCName.Location = new System.Drawing.Point(132, 6);
            this.tbxCName.Name = "tbxCName";
            this.tbxCName.Size = new System.Drawing.Size(151, 27);
            this.tbxCName.TabIndex = 10;
            // 
            // tbxCCountry
            // 
            this.tbxCCountry.Location = new System.Drawing.Point(132, 39);
            this.tbxCCountry.Name = "tbxCCountry";
            this.tbxCCountry.Size = new System.Drawing.Size(151, 27);
            this.tbxCCountry.TabIndex = 11;
            // 
            // tbxCFirstLesson
            // 
            this.tbxCFirstLesson.Location = new System.Drawing.Point(132, 72);
            this.tbxCFirstLesson.Name = "tbxCFirstLesson";
            this.tbxCFirstLesson.Size = new System.Drawing.Size(151, 27);
            this.tbxCFirstLesson.TabIndex = 12;
            this.tbxCFirstLesson.Click += new System.EventHandler(this.tbxCFirstLesson_Click);
            // 
            // btnCAdd
            // 
            this.btnCAdd.Location = new System.Drawing.Point(101, 146);
            this.btnCAdd.Name = "btnCAdd";
            this.btnCAdd.Size = new System.Drawing.Size(94, 29);
            this.btnCAdd.TabIndex = 14;
            this.btnCAdd.Text = "Add";
            this.btnCAdd.UseVisualStyleBackColor = true;
            this.btnCAdd.Click += new System.EventHandler(this.btnCAdd_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(12, 6);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 15;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            this.monthCalendar1.MouseLeave += new System.EventHandler(this.monthCalendar1_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Lesson Topic:";
            // 
            // tbTopic
            // 
            this.tbTopic.Location = new System.Drawing.Point(132, 108);
            this.tbTopic.Name = "tbTopic";
            this.tbTopic.Size = new System.Drawing.Size(151, 27);
            this.tbTopic.TabIndex = 17;
            this.tbTopic.Click += new System.EventHandler(this.tbTopic_Click);
            this.tbTopic.Leave += new System.EventHandler(this.tbTopic_Leave);
            // 
            // childNewStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 213);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.tbTopic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCAdd);
            this.Controls.Add(this.tbxCFirstLesson);
            this.Controls.Add(this.tbxCCountry);
            this.Controls.Add(this.tbxCName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "childNewStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Student";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.childNewStudent_FormClosing);
            this.Load += new System.EventHandler(this.childNewStudent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCName;
        private System.Windows.Forms.TextBox tbxCCountry;
        private System.Windows.Forms.TextBox tbxCFirstLesson;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnCAdd;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTopic;
    }
}