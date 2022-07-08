using LessonCompanion.Logic;
using LessonCompanion.Logic.Models;
using LessonCompanion.Report;
using LessonCompanion.Frontend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LessonCompanion {
    //TODO: Question markers aren't working correctly

    public partial class MainForm : Form {
        //FIELDS - STATIC
        public static string studentName = "";
        public static SQLiteConnection conn;

        //FIELDS - INSTANCE
        Report.Report thisReport;

        //INITIALISING
        public MainForm() => InitializeComponent();

        private void Testing() { 
            cbStudentName.Text = "Abdullah (teenager)";
            tTopic.Text = "test";
            dgvNewLanguage.Rows.Add(new object[] { "hiking e:\"I'm going hiking tomorrow.\"" });
            dgvNewLanguage.Rows.Add(new object[] { "join e:\"My friend is joining me for hiking tomorrow.\"" });
            dgvNewLanguage.Rows.Add(new object[] { "anything else?" });
            dgvNewLanguage.Rows.Add(new object[] { "I can't make it e:\"Sorry I can't make it to the party next week. I'm too busy.\"" });
            dgvCorrections.Rows.Add(new object[] { "What do you play this weekend?" });
            dgvCorrections.Rows.Add(new object[] { "My friend and I are you hiking" });
            dgvCorrections.Rows.Add(new object[] { "I'm going a friend of mine for coffee" });
        }

        #region ACTIONS__________________________________________________________________________________________________________________

        private void ResetForm() {
            tTopic.Clear();
            tDate.Clear();
            tHomework.Clear();
            cbStudentName.Text = "";
            dgvNewLanguage.Rows.Clear();
            dgvPronunciation.Rows.Clear();
            dgvCorrections.Rows.Clear();

            splitContainer1.SplitterDistance = splitContainer1.Width / 2; //set splitter
            tDate.Text = DateTime.Today.ToString("yyyy/MM/dd"); //get date
            //TODO: Fix this drop down list
            cbStudentName.Items.AddRange(DBConnect.FindStudentNamesRecent()); //populate drop down list

            

            cbStudentName.Select();
        }
        #endregion

        #region EVENT HANDLERS___________________________________________________________________________________________________________
        //FORM
        private void MainForm_Load(object sender, EventArgs e) {
            try {
                //create new database if one doesn't exist
                if(!File.Exists("classes.sqlite")) {
                    DBConnect.CreateDatabase();
                }

                //set up autocomplete for student names
                AutoCompleteStringCollection autoCompleteDataSource = new AutoCompleteStringCollection();
                autoCompleteDataSource.AddRange(DBConnect.FindStudentNamesActive());
                cbStudentName.AutoCompleteCustomSource = autoCompleteDataSource;

                //Set form to default state
                ResetForm();

                //initialise toolstrip items' event handlers
                lessonListToolStripMenuItem.Click += lessonListToolStripMenuItem_Click;
                studentListToolStripMenuItem.Click += studentListToolStripMenuItem_Click;

                cbStudentName.Select();

                //Testing();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if(tTopic.Text.Length > 0 | tHomework.Text.Length > 0) {
                var response = MessageBox.Show(
                    "There are unsaved changed present on the form.\n" +
                        "Are you sure that you want to exit?",
                    "Exiting",
                    MessageBoxButtons.YesNo);
                
                if(response == DialogResult.No) {
                    e.Cancel = true;
                }
                else {
                    if(conn != null && conn.State == System.Data.ConnectionState.Open) {
                        conn.Close();
                    }
                }
            }
        }

        //BUTTONS---------------------------------------------------------------------------------------------
        private void btnExport_Click(object sender, EventArgs e) {
            //VALIDATION
            string dbDate;
            Dictionary<string, string> mapNewLang = CompanionActions.DgvToDict(dgvNewLanguage);
            Dictionary<string, string> mapPron = CompanionActions.DgvToDict(dgvPronunciation);
            Dictionary<string, string> mapCorr = CompanionActions.DgvToDict(dgvCorrections);

            //Check for dublicate keys (left-hand column)
            foreach(var map in new Dictionary<string, string>[] {
                mapNewLang, mapPron, mapCorr
            }) {
                if(map.Count > 0) {
                    string key = map.ElementAt(0).Key;
                    string value = map.ElementAt(0).Value;

                    if(key.Equals("1011909") && value.Equals("1011909")) {
                        return;
                    }
                }
            }

            try {
                dbDate = CompanionActions.ConvertDateFormat(tDate.Text);
            }
            catch(Exception) {
                MessageBox.Show(
                    "The date is not in the correct format\nIt must conform to YYYY/MM/DD",
                    "Input Error",
                    MessageBoxButtons.OK);
                return;
            }

            if(cbStudentName.Text.Length == 0
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
                Cursor.Current = Cursors.WaitCursor;

                if(alwaysOnTopToolStripMenuItem.CheckState == CheckState.Checked) {
                    alwaysOnTopToolStripMenuItem.CheckState = CheckState.Unchecked;
                }

                Student thisStudent;
                bool passStudent = true;
                bool passLesson = true;
                int stuID = DBConnect.FindStudentID(cbStudentName.Text);

                //CREATE NEW STUDENT IF NEW
                //a stuID of -1 means this student is not in the database
                if(stuID == -1) {
                    //ADD NEW STUDENT
                    thisStudent = new StudentNew(stuID, cbStudentName.Text);

                    var affectedRows = DBConnect.InsertStudent(thisStudent);
                    if(!affectedRows) {
                        passStudent = false;
                    }
                }

                if(passStudent) {
                    //CREATE LESSON
                    Lesson thisLesson = new Lesson(
                        DBConnect.NewLessonID(),
                        DBConnect.FindStudentID(cbStudentName.Text),
                        DateTime.Parse(dbDate),
                        tTopic.Text,
                        tHomework.Text);
                    passLesson = DBConnect.InsertLesson(thisLesson);

                    //CREATE REPORT
                    if(passLesson && CompanionActions.TablesContainData(new DataGridView[] {
                        dgvCorrections, dgvNewLanguage, dgvPronunciation
                    })) {
                        thisReport = new Report.Report(
                            lesson: thisLesson,
                            newLanguage: mapNewLang,
                            pronunciation: mapPron,
                            corrections: mapCorr);

                        if(thisReport.ReportID == -1) {
                            //save the new report
                            thisReport.ReportID = DBConnect.NewReportID();
                            DBConnect.InsertReport(thisReport);
                        }
                        else {
                            //TODO: Update report

                        }

                        if(passLesson & chbReport.Enabled) {
                            //Generate pdf
                            thisReport.Create();
                        }
                    }
                }

                if(passStudent && passLesson) {
                    MessageBox.Show("Lesson Saved to " + DBConnect.FindReportSaveLocation(thisReport));
                }

                if(clearFormOnSubmitToolStripMenuItem.CheckState == CheckState.Checked) {
                    ResetForm();
                }

                Cursor.Current = Cursors.Default;
            }
        }

        //TOOLSTRIP-------------------------------------------------------------------------------------------
        private void studentListToolStripMenuItem_Click(object sender, EventArgs e) {
            new StudentList().Show();
        }

        private void lessonListToolStripMenuItem_Click(object sender, EventArgs e) {
            new LessonList().Show();
        }

        private void alwaysOnTopToolStripMenuItem_CheckStateChanged(object sender, EventArgs e) {
            if(alwaysOnTopToolStripMenuItem.CheckState == CheckState.Checked) {
                this.TopMost = true;
            }
            else {
                this.TopMost = false;
            }
        }

        private void newWindowStripMenuItem_Click(object sender, EventArgs e) {
            new MainForm().Visible = true;
        }

        //DATAGRIDVIEWS---------------------------------------------------------------------------------------
        private void dgvPronunciation_KeyUp(object sender, KeyEventArgs e) {
            ReportActions.InsertBraceEnd((DataGridViewTextBoxEditingControl)sender);
        }

        private void dgvNewLanguage_KeyUp(object sender, KeyEventArgs e) {
            ReportActions.InsertBraceEnd((DataGridViewTextBoxEditingControl)sender);
        }

        private void dgvCorrections_KeyUp(object sender, KeyEventArgs e) {
            ReportActions.InsertBraceEnd((DataGridViewTextBoxEditingControl)sender);
        }

        private void dgvPronunciation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if(e.Control is DataGridViewTextBoxEditingControl tb) {
                tb.KeyUp -= dgvPronunciation_KeyUp;
                tb.KeyUp += dgvPronunciation_KeyUp;
            }
        }

        private void dgvCorrections_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if(e.Control is DataGridViewTextBoxEditingControl tb) {
                tb.KeyUp -= dgvCorrections_KeyUp;
                tb.KeyUp += dgvCorrections_KeyUp;
            }
        }

        private void dgvNewLanguage_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if(e.Control is DataGridViewTextBoxEditingControl tb) {
                tb.KeyUp -= dgvNewLanguage_KeyUp;
                tb.KeyUp += dgvNewLanguage_KeyUp;
            }
        }

        private void dgvPronunciation_Leave(object sender, EventArgs e) {
            var dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        private void dgvNewLanguage_Leave(object sender, EventArgs e) {
            var dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        private void dgvCorrections_Leave(object sender, EventArgs e) {
            var dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        //OTHER-----------------------------------------------------------------------------------------------
        private void chbReportOn_CheckedChanged(object sender, EventArgs e) {
            if(chbReport.CheckState == CheckState.Checked) {
                dgvNewLanguage.Enabled = true;
                dgvPronunciation.Enabled = true;
                dgvCorrections.Enabled = true;
            }
            else {
                dgvNewLanguage.Enabled = false;
                dgvPronunciation.Enabled = false;
                dgvCorrections.Enabled = false;
            }
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

        private void cbStudentName_Leave(object sender, EventArgs e) {
            var text = cbStudentName.Text;

            if(text.Length > 0) {
                //Set first character to upper case automatically
                if(!Char.IsUpper(text[0])) {
                    cbStudentName.Text = Char.ToUpper(text[0]).ToString() + text[1..(text.Length)];
                }
            }
        }

        private void cbStudentName_TextChanged(object sender, EventArgs e) {
            //Store currently selected student's name
            studentName = cbStudentName.Text;
        }
        #endregion
    }
}
