using Cambly_Reports.Backend;
using iText.Kernel.Pdf;
using LessonCompanion.Backend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LessonCompanion
{
    public partial class ReportCreator : Form
    {
        ArrayList studentArrayList = new ArrayList();
        static List<string> studentsList = new List<string>();
        Lesson thisLesson;

        public static SQLiteConnection conn;

        bool onlyRecent = false;
        public static string studentName = "";

        public ReportCreator() {
            InitializeComponent();
        }

        public string dataFromForm2 {
            get { return dataFromForm2; }
            set { studentArrayList.Add(value); }
        }

        #region EVENT HANDLERS

        private void cmbxStudentName_SelectedIndexChanged(object sender, EventArgs e) {
            studentName = cbStudentName.Text;
        }

        private void cmbxStudentName_TextChanged(object sender, EventArgs e) {
            studentName = cbStudentName.Text;
        }

        private void stcbAlwaysTop_Click(object sender, EventArgs e) {

        }

        private void tsmiAlwaysTop_Click(object sender, EventArgs e) {

        }

        private void tbNewLang_TextChanged(object sender, EventArgs e) {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {

        }

        private void ReportCreator_Load(object sender, EventArgs e) {
            if (!File.Exists("classes.sqlite")) {
                DBConnect.CreateDatabase();
            }

            try {
                splitContainer1.SplitterDistance = splitContainer1.Width / 2;
                tDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
                RefreshComboBox();
                DBConnect.GetSaveLocation();
                InitializeAutocomplete(cbStudentName);
                InitMenuStrip();

                cbStudentName.Select();
            }
            catch (Exception ex) {
                MessageBox.Show("ERROR connecting to database... " + ex);
                Application.Exit();
            }
        } //form loaded

        private void ReportCreator_FormClosed(object sender, FormClosedEventArgs e) {
            if (conn != null && conn.State == System.Data.ConnectionState.Open) {
                conn.Close();
            }
        } //form closed

        private void txbxDate_Click(object sender, EventArgs e) {

        } //Date textbox clicked

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// If any user input data is detected on the form, 
        /// the user is asked to confirm that they want to exit.
        /// If the user chooses to exit, the application will close.
        /// Before closing, the application checks that there is no 
        /// active SqliteConnection object, and closes it if there is.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportCreator_FormClosing(object sender, FormClosingEventArgs e) {
            if (tTopic.Text.Length > 0
                | tHomework.Text.Length > 0
                | tNewLanguage.Text.Length > 0
                | tPronunciation.Text.Length > 0
                | tbCorrections.Text.Length > 0) {
                var response = MessageBox.Show(
                    "There are unsaved changed present on the form.\n" +
                        "Are you sure that you want to exit?",
                    "Exiting",
                    MessageBoxButtons.YesNo);

                if (response == DialogResult.Yes) {
                    if (conn != null && conn.State == System.Data.ConnectionState.Open) {
                        conn.Close();
                    }

                    Application.Exit();
                }
            }


        }

        private void studentListToolStripMenuItem_Click(object sender, EventArgs e) {
            new AllStudents().Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (chbReport.CheckState == CheckState.Checked) {
                tbCorrections.Enabled = true;
                tNewLanguage.Enabled = true;
                tPronunciation.Enabled = true;
            }
            else {
                tbCorrections.Enabled = false;
                tNewLanguage.Enabled = false;
                tPronunciation.Enabled = false;
            }
        }

        private void ReportCreator_Enter(object sender, EventArgs e) {
            InitializeAutocomplete(cbStudentName);
        }

        #region Buttons

        private void btnExport_Click(object sender, EventArgs e) {
            //VALIDATION
            if (cbStudentName.Text.Length == 0
                | tDate.Text.Length == 0
                | tTopic.Text.Length == 0) {
                MessageBox.Show(
                    "Lesson entries must contain at least a name, topic, and date.",
                    "Input Error",
                    MessageBoxButtons.OK);
            }
            //TODO: regex for this - to check date format
            //else if () { }
            else {
                if (chbReport.Enabled & CountReportChars() > 0) {
                    //MAKE A REPORT
                }

                //SUBMIT THE LESSON
                using (conn) {
                    conn.Open();
                    int stuID = DBConnect.FindStuID(cbStudentName.Text);
                    string dbDate = tDate.Text.Replace('/', '-') + " 00:00:00";

                    //TODO: mixed up the lesson adding and new student adding

                    Student thisStudent = new StudentNew(stuID, cbStudentName.Text);
                    if (DBConnect.InsertStudent(thisStudent)) {
                        //clear the screen
                        tTopic.Clear();
                        tDate.Clear();
                        cbStudentName.Text = "";
                        tNewLanguage.Clear();
                        tbCorrections.Clear();
                        tPronunciation.Clear();

                        cbStudentName.Select();
                    }
                    else {
                        //throw up an error
                    }
                }



                if (tTopic.Text.Length > 0 | tDate.Text.Length > 0 | cbStudentName.Text.Length > 0) {
                    if (stuID != -1) {
                        try {
                            SQLiteCommand cmd = conn.CreateCommand();
                            cmd.CommandText = lessonInfo;
                            cmd.ExecuteNonQuery();

                            conn.Close();

                            tTopic.Clear();
                            tDate.Clear();
                            cbStudentName.Text = "";
                            tNewLanguage.Clear();
                            tbCorrections.Clear();
                            tPronunciation.Clear();

                            cbStudentName.Select();
                        }
                        catch (Exception ex) {
                            MessageBox.Show("ERROR adding the lesson to the database... " + ex.Message);
                            conn.Close();
                        }
                    }
                }
            }
        }

        private void btnAddArray_Click(object sender, EventArgs e) {

        } //add lesson to database [OLD]

        private void TodaysDate_Click(object sender, EventArgs e) {
            tDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        } //set today's date in the date textbox

        private void bAllStudents_Click(object sender, EventArgs e) {

        }

        #endregion

        #region ToolStripMenu

        private void tsmiNewStudent_Click(object sender, EventArgs e) {

        } //add new student

        private void tsmiOnlyRecent_Click(object sender, EventArgs e) {
            if (onlyRecent == false) {
                onlyRecent = true;
            }

            if (onlyRecent == true) {
                onlyRecent = false;
            }

            cbStudentName.Items.Clear();
            RefreshComboBox();
        } //#doesn't work# display only recent students 

        private void tsmiChangeSaveLoc_Click(object sender, EventArgs e) {
            diaSave.ShowDialog();
        }  //#doesn't work# change save location for document export 

        private void restartToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Restart();
        } //restart the application

        private void viewStudentListToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cbStudentName.SelectedItem != null) {
                studentName = cbStudentName.SelectedItem.ToString();
            }

            new LessonList().Show();
        } //open the Lesson List form


        #endregion

        #region Calendar
        private void calDate_DateSelected(object sender, DateRangeEventArgs e) {

        }

        private void calDate_Leave(object sender, EventArgs e) {

        }

        private void calDate_MouseLeave(object sender, EventArgs e) {

        }

        private void calDate_DateChanged(object sender, DateRangeEventArgs e) {
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

        private void tsmiSaveLoc_Click(object sender, EventArgs e) {
            diaSave.ShowDialog();
        }

        private void cmbxStudentName_Leave(object sender, EventArgs e) {
            if (
                cbStudentName.Text.Length > 0
                && Char.IsLower(cbStudentName.Text[0])
                ) {
                StringBuilder sb = new StringBuilder();

                sb.Append(cbStudentName.Text);
                sb[0] = Char.ToUpper(sb[0]);

                cbStudentName.Text = sb.ToString();
            }
        }

        private void studentNotesToolStripMenuItem_Click(object sender, EventArgs e) {
            new StudentNotes().Show();
        }

        #endregion

        #region CONTROLLER

        private int CountReportChars() {
            return tNewLanguage.Text.Length + tPronunciation.Text.Length + tbCorrections.Text.Length;
        }

        public void RefreshComboBox() {
            using (var conn = DBConnect.Connection) {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT DISTINCT 
                        s.stuID, 
                        s.stuName, 
                        l.lessDate, 
                        l.stuID
                    FROM Lessons as l 
                    JOIN Students as s
                    ON s.stuID = l.stuID
                    ORDER BY lessDate DESC;";

                SQLiteDataReader read = cmd.ExecuteReader();

                if (read != null && read.HasRows) {
                    while (cbStudentName.Items.Count <= 10 && read.Read()) {
                        if (!cbStudentName.Items.Contains((string)read["stuName"])) {
                            cbStudentName.Items.Add((string)read["stuName"]);
                        }
                    }
                }
            }
        }

        public string ConvertDateFormat(string originalDateFormat) {
            string[] splitDate = originalDateFormat.Split('/', 3);

            return String.Concat($"{splitDate[0]}-{splitDate[1]}-{splitDate[2]}");
        }

        public void ExportToDoc() {
            string filename = "";

            using (conn) {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT * 
                    FROM Preferences 
                    WHERE prefName = 'saveDirectory';
                    ";
            }

            PdfWriter writer = new PdfWriter(new FileInfo(""));
        }

        private void InitMenuStrip() {
            //CONTROLLER
            lessonListToolStripMenuItem.Click += LessonListToolStripMenuItem_Click;
            studentListToolStripMenuItem.Click += StudentListToolStripMenuItem_Click;
        }

        private void StudentListToolStripMenuItem_Click(object sender, EventArgs e) {
            new AllStudents().Visible = true;
        }

        private void LessonListToolStripMenuItem_Click(object sender, EventArgs e) {
            new LessonList().Visible = true;
        }

        public static void InitializeAutocomplete(ComboBox control) {
            using (var conn = DBConnect.Connection) {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT stuID, stuName FROM Students ORDER BY stuName";
                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader != null && reader.HasRows) {
                    while (reader.Read()) {
                        studentsList.Add((string)reader["stuName"]);
                    }

                    reader.Close();
                }
                conn.Close();
                var source = new AutoCompleteStringCollection();
                source.AddRange(studentsList.ToArray());

                control.AutoCompleteCustomSource = source;

                control.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                control.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void Autosave() {

        }

        /// <summary>
        /// Creates a new Lesson instance to go with this form instance. If a Lesson object
        /// has already been created, this will update the properties of the object with currently
        /// present values on the form.
        /// </summary>
        private void UpdateThisLesson() {
            if (thisLesson == null) {

            }
            else {

            }
        }

        #endregion

        private void lessonListToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
