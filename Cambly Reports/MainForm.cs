using System;
using System.Collections;
using System.IO;
using Spire.Doc;
using System.Data;
using Spire.Doc.Documents;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Cambly_Reports
{
    public partial class ReportCreator : Form
    {
        ArrayList studentArrayList = new ArrayList();
        Document document = new Document();
        string folderPath = "D:/Documents/teaching/";

        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = ../../../cambly.accdb; Persist Security Info=False";
        OleDbConnection conn;

        bool onlyRecent = false;

        public ReportCreator()
        {
            InitializeComponent();

            this.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - this.Width,
                Screen.PrimaryScreen.WorkingArea.Height - this.Height
                );
        }

        public string dataFromForm2
        {
            get { return dataFromForm2; }
            set { studentArrayList.Add(value); }
        }

        #region EVENT_HANDLERS
        private void ReportCreator_Load(object sender, EventArgs e) {
            try {
                conn = new OleDbConnection();
                conn.ConnectionString = connectionString;
                conn.Open();

                RefreshComboBox();

                cmbxStudentName.Select();
            }
            catch (Exception ex) {
                MessageBox.Show("ERROR connecting to database... " + ex);
                Application.Exit();
            }
        } //form loaded

        private void ReportCreator_FormClosed(object sender, FormClosedEventArgs e) {
            conn.Close();
        } //form closed

        private void txbxDate_Click(object sender, EventArgs e) {
            calDate.Visible = true;
        } //Date textbox clicked

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conn.Close();
            Application.Exit();
        }

        #region Buttons
        private void btnExport_Click(object sender, EventArgs e) 
        {
            bool hasSaved = false;

            /* If no text is entered for Language or Correction notes...
             *  Display a dialog asking if you still want to add this to a document
             *  Otherwise, don't export it
             */
            if (rtxbxVocab.Enabled == true && rtxbxGrammar.Enabled == true)
            {
                if (rtxbxGrammar.Text.Length == 0 & rtxbxVocab.Text.Length == 0)
                {
                    DialogResult res = MessageBox.Show(
                                        "No information entered for lesson notes. " +
                                        "Are you sure you want to export this to a document?",
                                        "No Lesson Notes",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {
                        ExportToDoc();
                        hasSaved = true;
                    }
                    else
                    {
                        hasSaved = true;
                    }
                }

                if (hasSaved == false)
                {
                    ExportToDoc();
                }
            }

            //Add lesson to DB
            int stuID = FindStuID(cmbxStudentName.Text);

            string lessonInfo = "INSERT INTO Lesson (lDate, lTopic, lStudent)" +
                                            $"VALUES ('{txbxDate.Text}', '{txbxTopic.Text}', {stuID})";

            if (txbxTopic.Text.Length > 0 | txbxDate.Text.Length > 0 | cmbxStudentName.Text.Length > 0)
            {
                if (stuID != -1)
                {
                    try
                    {
                        OleDbCommand cmd = conn.CreateCommand();
                        cmd.CommandText = lessonInfo;
                        cmd.ExecuteNonQuery();

                        DialogResult d = MessageBox.Show("Clear the data fields?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (d == DialogResult.Yes)
                        {
                            txbxTopic.Clear();
                            txbxDate.Clear();
                            cmbxStudentName.SelectedIndex = -1;
                            rtxbxVocab.Clear();
                            rtxbxGrammar.Clear();

                            cmbxStudentName.Select();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR adding the lesson to the database... " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("This learner's name is spelled wrong, or else has not been added to the database yet.");
                }
            }
            else
            {
                MessageBox.Show("Enter values for a name, topic, and date.");
            }
        } //export to Word document
        private void btnAddArray_Click(object sender, EventArgs e)
        {
            
        } //add lesson to database [OLD]
        private void TodaysDate_Click(object sender, EventArgs e)
        {
            txbxDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        } //set today's date in the date textbox
        #endregion

            #region ToolStripMenu

        private void tsmiNewStudent_Click(object sender, EventArgs e)
        {
            childNewStudent form = new childNewStudent();
            form.Show();
        } //add new student
        private void tsmiOnlyRecent_Click(object sender, EventArgs e)
        {
            if (onlyRecent == false)
            {
                onlyRecent = true;
            }

            if (onlyRecent == true)
            {
                onlyRecent = false;
            }

            cmbxStudentName.Items.Clear();
            RefreshComboBox();
        } //#doesn't work# display only recent students 
        private void tsmiChangeSaveLoc_Click(object sender, EventArgs e)
        {
            diaSave.ShowDialog();
        }  //#doesn't work# change save location for document export 
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        } //restart the application
        private void viewStudentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LessonList studentList = new LessonList();
            studentList.Show();
        } //open the Lesson List form

        #endregion

            #region Calendar
        private void calDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (calDate.SelectionStart.Month < 10 & calDate.SelectionStart.Day < 10)
            {
                txbxDate.Text = $"{calDate.SelectionStart.Year}/0{calDate.SelectionStart.Month}/0{calDate.SelectionStart.Day}";
            }
            else if (calDate.SelectionStart.Month < 10 & calDate.SelectionStart.Day > 10)
            {
                txbxDate.Text = $"{calDate.SelectionStart.Year}/0{calDate.SelectionStart.Month}/{calDate.SelectionStart.Day}";
            }
            else if (calDate.SelectionStart.Month > 10 & calDate.SelectionStart.Day < 10)
            {
                txbxDate.Text = $"{calDate.SelectionStart.Year}/{calDate.SelectionStart.Month}/{calDate.SelectionStart.Day}";
            }
            else
            {
                txbxDate.Text = $"{calDate.SelectionStart.Year}/{calDate.SelectionStart.Month}/{calDate.SelectionStart.Day}";
            }

            calDate.Visible = false;
        }
        private void calDate_Leave(object sender, EventArgs e)
        {
            calDate.Visible = false;
        }
        private void calDate_MouseLeave(object sender, EventArgs e)
        {
            calDate.Visible = false;
        }
        private void calDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            //if (calDate.SelectionStart.Month < 10 & calDate.SelectionStart.Day < 10)
            //{
            //    txbxDate.Text = $"{calDate.SelectionStart.Year}-0{calDate.SelectionStart.Month}-0{calDate.SelectionStart.Day}";
            //}
            //else if (calDate.SelectionStart.Month < 10 & calDate.SelectionStart.Day > 10)
            //{
            //    txbxDate.Text = $"{calDate.SelectionStart.Year}-0{calDate.SelectionStart.Month}-{calDate.SelectionStart.Day}";
            //}
            //else if (calDate.SelectionStart.Month > 10 & calDate.SelectionStart.Day < 10)
            //{
            //    txbxDate.Text = $"{calDate.SelectionStart.Year}-{calDate.SelectionStart.Month}-{calDate.SelectionStart.Day}";
            //}
            //else
            //{
            //    txbxDate.Text = $"{calDate.SelectionStart.Year}-{calDate.SelectionStart.Month}-{calDate.SelectionStart.Day}";
            //}

            //calDate.Visible = false;
        }
            #endregion


        #endregion

        #region FUNCTIONS
        public int FindStuID(string studentName)
        {
            int returnValue = -1;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT stuID FROM Student WHERE sName = '{studentName}'";
            OleDbDataReader dbRead = cmd.ExecuteReader();

            if (dbRead != null && dbRead.HasRows)
            {
                while (dbRead.Read())
                {
                    returnValue = (int)dbRead["stuID"];
                }
                return returnValue;
            }
            else 
            {
                return returnValue;
            }
        }

        public void RefreshComboBox()
        {
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT TOP 10 Student.stuID, Student.sName, Lesson.lDate, Lesson.lStudent " +
                    "FROM Lesson INNER JOIN Student " +
                        "ON Student.stuID = Lesson.lStudent " +
                    "ORDER BY lDATE DESC";
            OleDbDataReader read = cmd.ExecuteReader();

            if (read != null && read.HasRows)
            {
                while (read.Read())
                {
                    if (!cmbxStudentName.Items.Contains((string)read["sName"])) 
                    {
                        cmbxStudentName.Items.Add((string)read["sName"]);
                    }
                }
            }
            
        }

        Dictionary<string, string> GetReplaceDictionary()
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("#name#", cmbxStudentName.Text);
            replaceDict.Add("#date#", txbxDate.Text);
            replaceDict.Add("#vocab#", rtxbxVocab.Text.Trim());
            replaceDict.Add("#grammar#", rtxbxGrammar.Text.Trim());
            replaceDict.Add("#topic#", txbxTopic.Text);

            return replaceDict;
        }

        public string ConvertDateFormat(string originalDateFormat)
        {
            string[] splitDate = originalDateFormat.Split('/', 3);

            return String.Concat($"{splitDate[0]}-{splitDate[1]}-{splitDate[2]}");
        }

        public void ExportToDoc()
        {
            //Try... catch block to send notes to document
            try
            {
                document.LoadFromFile("D:/Documents/teaching/-TEMPLATE.docx");

                Dictionary<string, string> dictReplace = GetReplaceDictionary();

                foreach (KeyValuePair<string, string> kvp in dictReplace)
                {
                    document.Replace(kvp.Key, kvp.Value, true, true);
                }

                string fixedDate = ConvertDateFormat(txbxDate.Text);
                document.SaveToFile($"D:/Documents/teaching/{fixedDate} - {cmbxStudentName.Text}.docx", FileFormat.Docx);
                MessageBox.Show("All tasks are finished.", "Doc Processing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                document.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR... {ex}... {ex.Message}");
            }
        }


        #endregion

        private void ReportCreator_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Closing Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bAllStudents_Click(object sender, EventArgs e)
        {
            
        }

        private void studentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AllStudents().Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                rtxbxGrammar.Enabled = true;
                rtxbxVocab.Enabled = true;
            }
            else
            {
                rtxbxGrammar.Enabled = false;
                rtxbxVocab.Enabled = false;
            }
        }
    }
}
