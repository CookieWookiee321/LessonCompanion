using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace LessonCompanion
{
    public partial class AllStudents : Form
    {
        SQLiteConnection conn = ReportCreator.conn;

        public AllStudents() {
            InitializeComponent();
        }

        private void AllStudents_Load(object sender, EventArgs e) {
            dgvStudents.ColumnCount = 3;
            dgvStudents.Columns[0].Name = "Name";
            dgvStudents.Columns[1].Name = "Country";
            dgvStudents.Columns[2].Name = "First Lesson";

            try {
                conn.Open();

                SQLiteCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT stuName, firstLesson " +
                    "FROM Students " +
                    "ORDER BY stuName";
                SQLiteDataReader reader = command.ExecuteReader();

                string[] row;

                if (reader != null && reader.HasRows) {
                    while (reader.Read()) {
                        DateTime date = (DateTime)reader["firstLesson"];
                        string dateString = date.ToString("yyyy/MM/dd");

                        row = new string[] {
                            $"{(string)reader["stuName"]}",
                            $"{(string)reader["sNationality"]}",
                            $"{dateString}"
                        };
                        dgvStudents.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("ERROR:\n" + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void AllStudents_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                conn.Close();
            }
            catch (Exception) {
                //do nothing
            }
        }
    }
}
