using Spire.Doc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace Cambly_Reports
{
    public partial class ReportCreator : Form
    {
        ArrayList studentArrayList = new ArrayList();
        static List<string> studentsList = new List<string>();
        Document document = new Document();
        string reportOuput;

        string connectionString = "Data Source=cambly.sqlite;Version=3;";
        public static SQLiteConnection conn;

        bool onlyRecent = false;
        public static string studentName = "";

        public ReportCreator()
        {
            InitializeComponent();

            this.Location = new Point
                (
                Screen.PrimaryScreen.WorkingArea.Width - this.Width,
                Screen.PrimaryScreen.WorkingArea.Height - this.Height
                );
        }

        public string dataFromForm2
        {
            get { return dataFromForm2; }
            set { studentArrayList.Add(value); }
        }

        //EVENT HANDLERS---------------------------------------------------------------------------------

        private void ReportCreator_Load(object sender, EventArgs e) 
        {
            if (!File.Exists("cambly.sqlite"))
            {
                CreateDatabase();
            }
            
            try 
            {
                conn = new SQLiteConnection();
                conn.ConnectionString = connectionString;

                RefreshComboBox();

                GetSaveLocation();

                InitializeAutocomplete(conn, cmbxStudentName);

                cmbxStudentName.Select();

                tsmiAlwaysTop.Checked = true;
                this.TopMost = true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("ERROR connecting to database... " + ex);
                Application.Exit();
            }
        } //form loaded

        private void ReportCreator_FormClosed(object sender, FormClosedEventArgs e) 
        {
            conn.Close();
        } //form closed

        private void txbxDate_Click(object sender, EventArgs e) 
        {
            calDate.Visible = true;
        } //Date textbox clicked

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conn.Close();
            Application.Exit();
        }

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

        private void ReportCreator_Enter(object sender, EventArgs e)
        {
            InitializeAutocomplete(conn, cmbxStudentName);
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
            conn.Open();

            int stuID = FindStuID(cmbxStudentName.Text);

            StringBuilder sb = new StringBuilder();
            sb.Append(txbxDate.Text);
            sb = sb.Replace('/', '-');
            sb.Append(" 00:00:00");
            string correctDateTime = sb.ToString();

            string lessonInfo = "INSERT INTO Lesson (lDate, lTopic, lStudent)" +
                                $"VALUES ('{correctDateTime}', '{txbxTopic.Text}', {stuID})";

            if (txbxTopic.Text.Length > 0 | txbxDate.Text.Length > 0 | cmbxStudentName.Text.Length > 0)
            {
                if (stuID != -1)
                {
                    try
                    {
                        SQLiteCommand cmd = conn.CreateCommand();
                        cmd.CommandText = lessonInfo;
                        cmd.ExecuteNonQuery();

                        conn.Close();

                        txbxTopic.Clear();
                        txbxDate.Clear();
                        cmbxStudentName.Text = "";
                        rtxbxVocab.Clear();
                        rtxbxGrammar.Clear();

                        cmbxStudentName.Select();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR adding the lesson to the database... " + ex.Message);
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("This learner's name is spelled wrong, or else has not been added to the database yet.");
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Enter values for a name, topic, and date.");
                conn.Close();
            }
        } //export to Word document + add lesson to database

        private void btnAddArray_Click(object sender, EventArgs e)
        {
            
        } //add lesson to database [OLD]

        private void TodaysDate_Click(object sender, EventArgs e)
        {
            txbxDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        } //set today's date in the date textbox

        private void bAllStudents_Click(object sender, EventArgs e)
        {

        }

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

        private void tsmiSaveLoc_Click(object sender, EventArgs e)
        {
            diaSave.ShowDialog();
        }

        private void cmbxStudentName_Leave(object sender, EventArgs e)
        {
            if (
                cmbxStudentName.Text.Length > 0
                && Char.IsLower(cmbxStudentName.Text[0])
                )
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(cmbxStudentName.Text);
                sb[0] = Char.ToUpper(sb[0]);

                cmbxStudentName.Text = sb.ToString();
            }
        }

        private void studentNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new StudentNotes().Show();
        }

        //METHODS--------------------------------------------------------------------------------------

        private void GetSaveLocation()
        {
            string query = 
                $"SELECT SaveDir, TemplateFilename " +
                $"FROM Saves;";

            conn.Open();

            SQLiteDataReader reader = new SQLiteCommand(query, conn).ExecuteReader();

            while (reader.Read())
            {
                reportOuput = $"{(string)reader["SaveDir"]}{(string)reader["TemplateFilename"]}";
            }
            reader.Close();
            conn.Close();
        }

        public int FindStuID(string studentName)
        {
            int returnValue = -1;
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT stuID FROM Student WHERE sName = '{studentName}'";
            SQLiteDataReader dbRead = cmd.ExecuteReader();

            if (dbRead != null && dbRead.HasRows)
            {
                while (dbRead.Read())
                {
                    returnValue = (int)dbRead["stuID"];
                }
                dbRead.Close();

                return returnValue;
            }
            else 
            {
                dbRead.Close();

                return returnValue;
            }
        }

        public void RefreshComboBox()
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT Student.stuID, Student.sName, Lesson.lDate, Lesson.lStudent " +
                    "FROM Lesson INNER JOIN Student " +
                        "ON Student.stuID = Lesson.lStudent " +
                    "ORDER BY lDATE DESC " +
                    "LIMIT 10;";
            conn.Open();
            SQLiteDataReader read = cmd.ExecuteReader();

            if (read != null && read.HasRows)
            {
                while (read.Read())
                {
                    if (!cmbxStudentName.Items.Contains((string)read["sName"])) 
                    {
                        cmbxStudentName.Items.Add((string)read["sName"]);
                    }
                }

                read.Close();
            }
            conn.Close();
            
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
                document.LoadFromFile(reportOuput);

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

        public static void InitializeAutocomplete(SQLiteConnection connection, ComboBox control)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT stuID, sName FROM Student ORDER BY sName";
            conn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    studentsList.Add((string)reader["sName"]);
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

        private void cmbxStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            studentName = cmbxStudentName.Text;
        }

        private void cmbxStudentName_TextChanged(object sender, EventArgs e)
        {
            studentName = cmbxStudentName.Text;
        }

        private void stcbAlwaysTop_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAlwaysTop_Click(object sender, EventArgs e)
        {
            if (tsmiAlwaysTop.Checked == true)
            {
                tsmiAlwaysTop.Checked = false;
                this.TopMost = false;
            }
            else
            {
                tsmiAlwaysTop.Checked = true;
                this.TopMost = true;
            }
        }

        private void CreateDatabase()
        {
            SQLiteConnection.CreateFile("cambly.sqlite");
            conn = new SQLiteConnection(connectionString);
            conn.Open();

            //Student
            string createTableStudent =
                "CREATE TABLE student (" +
                    "stuID INT NOT NULL PRIMARY KEY, " +
                    "sName VARCHAR(50) NOT NULL, " +
                    "sNationality VARCHAR(100) NOT NULL, " +
                    "firstLesson DATETIME NOT NULL, " +
                    "notes VARCHAR(255)" +
                "); ";
            SQLiteCommand tStudent = new SQLiteCommand(createTableStudent, conn);
            using (tStudent)
            {
                tStudent.ExecuteNonQuery();
            }

            //tLessons
            string createTableLesson =
                "CREATE TABLE lesson (" +
                    "lDate DATETIME NOT NULL, " +
                    "lTopic VARCHAR(255) NOT NULL, " +
                    "lStudent INT NOT NULL, " +
                    "PRIMARY KEY (lDate, lStudent)" +
                "); ";
            using (SQLiteCommand tLesson = new SQLiteCommand(createTableLesson, conn))
            {
                tLesson.ExecuteNonQuery();
            }

            //tSaves
            string createTableSaves = "CREATE TABLE saves (" +
                    "SaveDir VARCHAR(255), " +
                    "TemplateFilename VARCHAR(255)" +
                ");";
           using (SQLiteCommand tSaves = new SQLiteCommand(createTableSaves, conn))
            {
                tSaves.ExecuteNonQuery();
            }

           //insert
            string insert = "INSERT INTO saves (SaveDir, TemplateFilename) " + 
                    "VALUES ('D:/Documents/teaching/', '-TEMPLATE');";
            using (SQLiteCommand tInsert = new SQLiteCommand(insert, conn))
            {
                tInsert.ExecuteNonQuery();
            }

            //Iterate through and execute

            

            conn.Close();
        }

        //FORMATTING

        private void FormatForm(Form form)
        {
            form.BackColor = ColorTranslator.FromHtml("");
        }
    }
}
