using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;


namespace Cambly_Reports
{
    public partial class LessonList : Form
    {
        SQLiteConnection conn = ReportCreator.conn;

        public Dictionary<int, string> allStudents;
        Dictionary<string, int> allStudentsInverse;
        Dictionary<int, string> recentStudents;
        Dictionary<string, string[][]> AllLessonsAllStudents = new Dictionary<string, string[][]>();

        public LessonList()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            allStudents = new Dictionary<int, string>();
            allStudentsInverse = new Dictionary<string, int>();
            recentStudents = new Dictionary<int, string>();

            DataGridViewSetUp();

            PopulateDictionaries();

            LoadAllLessons();

            chbRecent.CheckState = CheckState.Checked;

            if (ReportCreator.studentName is not null)
            {
                if (!lbStudentList.Items.Contains(lbStudentList.SelectedItem))
                {
                    chbRecent.CheckState = CheckState.Unchecked;
                }

                lbStudentList.SelectedItem = ReportCreator.studentName;
            }
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
            AllResults();
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
                    List<string[]> newResults = new List<string[]>();

                    //Loop through each row in the present results
                    foreach(DataGridViewRow row in dgvTopicList.Rows)
                    {
                        //If the topic matches any part of the search term...
                        //(converted to uppercase)
                        if (row.Cells[1].Value.ToString().ToUpper().Contains(tbSearch.Text.ToUpper()))
                        {
                            //... add the row to the newResults List
                            newResults.Add(new string[] { row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString() });
                        }
                    }
                    dgvTopicList.Rows.Clear();

                    //Add the rows in newResults to the datagridview
                    //Final search results
                    foreach (string[] row in newResults)
                    {
                        dgvTopicList.Rows.Add(row);
                    }
                }
                else if (tbSearch.Text.Length == 0)
                {
                    dgvTopicList.Rows.Clear();
                    AllResults();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReportCreator.studentName = null;
            conn.Close();
        }

        //--------------------------------------------------------------------------------------------------

        private void AllResults()
        {
            string stuName = lbStudentList.SelectedItem.ToString();

            foreach (string[] row in AllLessonsAllStudents[stuName])
            {
                dgvTopicList.Rows.Add(row);
            }

            lblLessonCount.Text = $"Total lesson count: {dgvTopicList.Rows.Count}";
        }
        
        private void PopulateDictionaries()
        {
            try //Populating allStudents ArrayList
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT stuID, sName FROM Student ORDER BY sName";
                conn.Open();

                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        allStudents.Add((int)reader["stuID"], (string)reader["sName"]);
                        allStudentsInverse.Add((string)reader["sName"], (int)reader["stuID"]);
                    }
                    reader.Close();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR populating allStudents Dictionary... " + ex.Message);
            }

            try //Populating recentStudents ArrayList           
            {
                bool isUnique = true;

                SQLiteCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = 
                    $"SELECT DISTINCT lesson.lDate, lesson.lTopic, student.stuID, student.sName " +
                    $"FROM     lesson, student " +
                    $"WHERE  lesson.lStudent = student.stuID " +
                    $"ORDER BY lDate DESC " +
                    $"LIMIT 20;";
                conn.Open();
                SQLiteDataReader reader2 = cmd2.ExecuteReader();

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

                    reader2.Close();
                }
                conn.Close();
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
            dgvTopicList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadAllLessons()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                List<string[]> lessons;
                string queryLessons =
                    $"SELECT lDate, lTopic " +
                    $"FROM lesson JOIN student " +
                        $"ON student.stuID = lesson.lStudent " +
                    $"WHERE sName = @name " +
                    $"ORDER BY lDate DESC;";

                //Loop through every student
                foreach (string student in allStudents.Values)
                {
                    SQLiteCommand command = new SQLiteCommand(queryLessons, conn);
                    command.Parameters.Add(new SQLiteParameter("@name", student));
                    SQLiteDataReader r = command.ExecuteReader();
                    lessons = new List<string[]>();

                    //Add each lesson by the student to the List
                    while (r.Read())
                    {
                        if (IsDateTime(r))
                        {
                            DateTime hi = (DateTime)r["lDate"];
                            string lo = hi.ToString("yyyy/MM/dd");

                            lessons.Add(new string[] { $"{lo}", $"{(string)r["lTopic"]}" });
                        }
                        else
                        {
                            string lo = (string)r["lDate"];

                            lessons.Add(new string[] { $"{lo}", $"{(string)r["lTopic"]}" });
                        }
                    }
                    r.Close();
                    //Add the List to the Dictionary
                    AllLessonsAllStudents.Add(student, lessons.ToArray());
                }

                conn.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show
                    (
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                this.Dispose();
            }
        }

        private bool IsDateTime(SQLiteDataReader reader)
        {
            try
            {
                DateTime date = (DateTime)reader["lDate"];

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}