using LessonCompanion.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cambly_Reports.UI.Dialogs {
    public partial class LessonEditDialog : Form {
        public LessonEditDialog(string studentName, string date, string topic, string homework) {
            InitializeComponent();

            this.StudentName = studentName;
            this.Date = date;
            this.Topic = topic;
            this.Homework = homework;
        }

        //EVENT HANDLERS
        private void LessonEditDialog_Load(object sender, EventArgs e) {
            //set values of boxes to match selected lesson input
            tName.Text = StudentName;
            tDate.Text = Date;
            tTopic.Text = Topic;
            tHomework.Text = Homework;
        }

        private void bClose_Click(object sender, EventArgs e) {
            this.Dispose();
        }
        
        private void bSaveClose_Click(object sender, EventArgs e) {
            var dateDetails = Date.Split('/');
            var dateTrue = new DateTime(
                int.Parse(dateDetails[0]),
                int.Parse(dateDetails[1]),
                int.Parse(dateDetails[2])
            );

            var res = DBConnect.UpdateLessonDetails(StudentName, dateTrue, tTopic.Text, tHomework.Text);

            if (res) {
                this.Dispose();
            }
            else {
                MessageBox.Show("There was an error updating this Lesson.");
                //TODO: Add error log dump file
            }
        }

        //PROPERTIES
        private string Date { get; set; }
        private string StudentName { get; set; }
        private string Topic { get; set; }
        private string Homework { get; set; }
    }
}
