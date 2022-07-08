using LessonCompanion.Logic;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace LessonCompanion {
    public partial class StudentList : Form {
        SQLiteConnection conn = MainForm.conn;

        public StudentList() {
            InitializeComponent();
        }

        private void AllStudents_Load(object sender, EventArgs e) {
            try {
                using var conn = DBConnect.Connection;
                conn.Open();

                //get list of all IDs
                var allIDs = new Dictionary<int, bool>();
                SQLiteCommand commandIDs = conn.CreateCommand();
                commandIDs.CommandText = @"
                    SELECT stuID, isCurrent 
                    FROM Students;";

                using var readerIDs = commandIDs.ExecuteReader();
                if(readerIDs != null && readerIDs.HasRows) {
                    while(readerIDs.Read()) {
                        bool active = false;

                        if((int)readerIDs[1] == 1) {
                            active = true;
                        }

                        allIDs.Add((int)readerIDs[0], active);
                    }
                }
                readerIDs.Close();

                //get all details associated with IDs
                using SQLiteCommand command = conn.CreateCommand();

                List<object[]> rows = new List<object[]>();
                foreach(var id in allIDs.Keys) {
                    command.CommandText = $@"
                        SELECT stuID, MIN(lessDate), COUNT(*)
                        FROM Lessons
                        WHERE stuID = {id};";

                    using SQLiteDataReader reader = command.ExecuteReader();
                    if(reader != null && reader.HasRows) {
                        while(reader.Read()) {
                            if(reader[0] != DBNull.Value) {
                                rows.Add(new object[] {
                                    reader[0], //id int
                                    reader[1], //date string
                                    reader[2] //count int
                                });
                            }
                        }
                    }
                }

                //find the name associated with each id
                //add row to table
                foreach(var row in rows) {
                    int x = (int)row[0];
                    row[0] = DBConnect.FindStudentName(x);

                    string trueDate = row[1].ToString().Split(' ')[0];

                    var newRow = new object[] {
                        row[0], trueDate, row[2], allIDs[x]
                    };
                    dgvStudents.Rows.Add(newRow);
                }
            }
            catch(Exception ex) {
                MessageBox.Show(
                    "ERROR:\n" + ex.Message,
                    ex.ToString(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void bSwitch_Click(object sender, EventArgs e) {
            if(dgvStudents.SelectedCells.Count != 0) {
                bool newState = !(bool)dgvStudents.SelectedCells[3].Value;

                DBConnect.UpdateStudentActive((string)dgvStudents.SelectedCells[0].Value, newState);

                dgvStudents.SelectedCells[3].Value = newState;
            }
        }

        private void bDelete_Click(object sender, EventArgs e) {
            if(dgvStudents.SelectedCells.Count != 0) {
                DBConnect.DeleteStudent((string)dgvStudents.SelectedCells[0].Value);
                dgvStudents.Rows.RemoveAt(dgvStudents.SelectedRows[0].Index);
            }
        }

        private void bEdit_Click(object sender, EventArgs e) {
            if(dgvStudents.SelectedCells.Count != 0) {
                dgvStudents.ReadOnly = false;
                dgvStudents.BeginEdit(true);
            }
        }

        private void dgvStudents_CellEndEdit(object sender, DataGridViewCellEventArgs e) {

        }
    }
}
