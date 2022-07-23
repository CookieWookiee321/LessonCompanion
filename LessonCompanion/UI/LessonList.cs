using LessonCompanion.Logic;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;


namespace LessonCompanion {
    public partial class LessonList : Form {
        public Dictionary<int, string> allStudents;
        List<string> recentStudents;

        public LessonList() {
            InitializeComponent();
        }
        //--------------------------------------------------------------------------------------------------
        private void FindLessonsForStudent() {
            string stuName;

            if(lbStudentList.SelectedItem != null) {
                stuName = lbStudentList.SelectedItem.ToString();

                var results = DBConnect.FindLessonsByStudent(stuName);
                foreach(var entry in results) {
                    dgvTopicList.Rows.Add(new string[] { entry[0], entry[1], entry[2] });
                }

                lLessonCount.Text = $"Total lesson count: {dgvTopicList.Rows.Count}";
            }
        }

        private void SetUpStorage() {
            try {
                //Populating allStudents ArrayList
                using var conn = DBConnect.Connection;
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT stuID, stuName 
                    FROM Students 
                    ORDER BY stuName";

                using SQLiteDataReader reader = cmd.ExecuteReader();
                if(reader != null && reader.HasRows) {
                    while(reader.Read()) {
                        allStudents.Add((int)reader["stuID"], (string)reader["stuName"]);
                    }
                }
                reader.Close();

                cmd.CommandText = @"
                    SELECT DISTINCT s.stuName 
                    FROM Students as s 
                    JOIN Lessons as l 
                        ON s.stuID = l.stuID 
                    ORDER BY lessDate DESC 
                    LIMIT 20;";

                using SQLiteDataReader reader2 = cmd.ExecuteReader();
                if(reader2 != null && reader2.HasRows) {
                    while(reader2.Read()) {
                        recentStudents.Add((string)reader2[0]);
                    }
                }
            }
            catch(Exception ex) {

                MessageBox.Show("ERROR:\n" + ex.ToString());
                Application.Exit();
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void Form3_Load(object sender, EventArgs e) {
            allStudents = new Dictionary<int, string>();
            recentStudents = new List<string>();

            SetUpStorage();

            chbRecent.CheckState = CheckState.Checked;

            //If the selected student's name is not of your most recent,
            //search the entire list of students for it
            if(!MainForm.studentName.Equals("")) {
                if(!lbStudentList.Items.Contains(MainForm.studentName)) {
                    chbRecent.CheckState = CheckState.Unchecked;
                }

                lbStudentList.SelectedItem = MainForm.studentName;
            }
        }

        private void rbRecentStudents_CheckedChanged(object sender, EventArgs e) {
            lbStudentList.Items.Clear();

            if(chbRecent.CheckState == CheckState.Unchecked) {
                lbStudentList.Items.AddRange(allStudents.Values.ToArray());
            }
            else if(chbRecent.CheckState == CheckState.Checked) {
                lbStudentList.Items.AddRange(recentStudents.ToArray());
            }
        }

        private void lbStudentList_SelectedIndexChanged(object sender, EventArgs e) {
            if (!button2.Enabled) {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            
            dgvTopicList.Rows.Clear();
            FindLessonsForStudent();
        }

        private void tbSearch_Click(object sender, EventArgs e) {
            tbSearch.Text = "";
        }

        private void tbSearch_TextChanged(object sender, EventArgs e) {
            //in case name is not selected when search bar is used
            try {
                if(tbSearch.Text.Length > 0) {
                    List<string[]> newResults = new List<string[]>();

                    //Loop through each row in the present results
                    foreach(DataGridViewRow row in dgvTopicList.Rows) {
                        //If the topic matches any part of the search term...
                        //(converted to uppercase)
                        if(row.Cells[1].Value.ToString().ToUpper().Contains(tbSearch.Text.ToUpper())) {
                            //... add the row to the newResults List
                            newResults.Add(new string[] { row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString() });
                        }
                    }
                    dgvTopicList.Rows.Clear();

                    //Add the rows in newResults to the datagridview
                    //Final search results
                    foreach(string[] row in newResults) {
                        dgvTopicList.Rows.Add(row);
                    }
                }
                else if(tbSearch.Text.Length == 0) {
                    dgvTopicList.Rows.Clear();
                    FindLessonsForStudent();
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}