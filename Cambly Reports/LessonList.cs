using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Cambly_Reports
{
    public partial class LessonList : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=../../../cambly.accdb; Persist Security Info=False";
        OleDbConnection conn;

        public Dictionary<int, string> allStudents;
        Dictionary<string, int> allStudentsInverse;
        Dictionary<int, string> recentStudents;

        int thisStudentID;

        public LessonList()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            allStudents = new Dictionary<int, string>();
            allStudentsInverse = new Dictionary<string, int>();
            recentStudents = new Dictionary<int, string>();

            DataGridViewSetUp();

            PopulateDictionaries();

            chbRecent.CheckState = CheckState.Checked;
        }
        private void rbAllStudents_CheckedChanged(object sender, EventArgs e)
        {
            //empty OLD
        }
        private void rbRecentStudents_CheckedChanged(object sender, EventArgs e)
        {
            //only need to use one event handler?
        }
        private void lbStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTopicList.Rows.Clear();

            string[] row;

            thisStudentID = allStudentsInverse[$"{lbStudentList.SelectedItem.ToString()}"]; //gets the stuID associated
                                                                                            //with the selected name
            OleDbCommand cmdFetch = conn.CreateCommand();
            cmdFetch.CommandText = $"SELECT lDate, lTopic FROM Lesson WHERE lStudent = {thisStudentID} ORDER BY lDate Desc";
            OleDbDataReader reader = cmdFetch.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    DateTime hi = (DateTime)reader["lDate"];
                    string lo = hi.ToString("yyyy/MM/dd");

                    row = new string[] { $"{lo}", $"{(string)reader["lTopic"]}" };
                    dgvTopicList.Rows.Add(row);
                }
            }

            lblLessonCount.Text = $"Total lesson count: {dgvTopicList.Rows.Count}";
        }

        //--------------------------------------------------------------------------------------------------
        private void PopulateDictionaries()
        {
            try //Populating allStudents ArrayList
            {
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT stuID, sName FROM Student ORDER BY sName";
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        allStudents.Add((int)reader["stuID"], (string)reader["sName"]);
                        allStudentsInverse.Add((string)reader["sName"], (int)reader["stuID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR populating allStudents Dictionary... " + ex.Message);
            }

            try //Populating recentStudents ArrayList           
            {
                bool isUnique = true;

                OleDbCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "SELECT DISTINCT TOP 10 Student.stuID, Student.sName, Lesson.lDate, Lesson.lStudent " +
                                    "FROM Lesson INNER JOIN Student " +
                                        "ON Student.stuID = Lesson.lStudent " +
                                    "ORDER BY lDATE DESC";
                OleDbDataReader reader2 = cmd2.ExecuteReader();

                if (reader2 != null && reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        for (int i = 0; i < recentStudents.Count; i++)
                        {
                            if ((string)reader2["sName"] == recentStudents.Values.ElementAt(i))
                            {                                       //Compares new student names to
                                isUnique = false;                   //those already in the dictionary  
                                break;                              //if they match, the loop is broken
                            }
                        }

                        if (isUnique == true)
                        {                                                                           //if a match wasn't found, 
                            recentStudents.Add((int)reader2["stuID"], (string)reader2["sName"]);    //the name will be added
                        }                                                                           //to the array

                        isUnique = true;                                    //isUnique is set back to true for the next loop
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR populating recentStudents Dictionary... " + ex.Message);
            }
        }
        private void PopulateStudentLists()
        {
            lbStudentList.Items.Clear();

            if (chbRecent.CheckState == CheckState.Unchecked)
            {
                lbStudentList.Items.AddRange(allStudents.Values.ToArray());
            }
            else if (chbRecent.CheckState == CheckState.Checked)
            {
                lbStudentList.Items.AddRange(recentStudents.Values.ToArray());
            }
        }
        private void DataGridViewSetUp()
        {
            dgvTopicList.ColumnCount = 2;
            dgvTopicList.Columns[0].Name = "Date";
            dgvTopicList.Columns[1].Name = "Topic";
            dgvTopicList.Columns[0].Width = 200;
        }

        private void chbRecent_CheckedChanged(object sender, EventArgs e)
        {
            PopulateStudentLists();
        }

        private void tbSearch_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            //in case name is not selected when search bar is used
            try
            {
                if (tbSearch.Text.Length > 0)
                {
                    dgvTopicList.Rows.Clear();

                    string[] row;

                    thisStudentID = allStudentsInverse[$"{lbStudentList.SelectedItem.ToString()}"]; //gets the stuID associated
                                                                                                    //with the selected name
                    OleDbCommand cmdFetch = conn.CreateCommand();
                    cmdFetch.CommandText = $"SELECT lDate, lTopic FROM Lesson WHERE lStudent = {thisStudentID} AND lTopic like '%{tbSearch.Text}%' ORDER BY lDate Desc";
                    OleDbDataReader reader = cmdFetch.ExecuteReader();

                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime hi = (DateTime)reader["lDate"];
                            string lo = hi.ToString("yyyy/MM/dd");

                            row = new string[] { $"{lo}", $"{(string)reader["lTopic"]}" };
                            dgvTopicList.Rows.Add(row);
                        }
                    }

                    lblLessonCount.Text = $"Total lesson count: {dgvTopicList.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }
    }
}