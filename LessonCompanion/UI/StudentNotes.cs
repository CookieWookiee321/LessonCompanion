using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace LessonCompanion {
    public partial class StudentNotes : Form {
        SQLiteConnection conn = MainForm.conn;

        public StudentNotes() {
            InitializeComponent();
        }

        private void StudentNotes_Load(object sender, EventArgs e) {
            conn.Open();

            //Add names to combobox1
            using(SQLiteCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = "SELECT Student.stuID, Student.stuName, Lesson.lStudent " +
                    "FROM Lesson INNER JOIN Student " +
                        "ON Student.stuID = Lesson.lStudent " +
                    "ORDER BY stuName;";
                SQLiteDataReader read = cmd.ExecuteReader();

                if(read != null && read.HasRows) {
                    while(read.Read()) {
                        if(!comboBox1.Items.Contains((string)read["stuName"])) {
                            comboBox1.Items.Add((string)read["stuName"]);
                        }
                    }
                }
            }

            conn.Close();

            //Select name
            if(comboBox1.Items.Contains(MainForm.studentName)) {
                comboBox1.SelectedItem = MainForm.studentName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            conn.Open();

            string x =
                $"SELECT notes " +
                $"FROM Student " +
                $"WHERE stuName = '{comboBox1.SelectedItem}';";

            using(SQLiteDataReader reader = new SQLiteCommand(x, conn).ExecuteReader()) {
                if(reader != null && reader.HasRows) {
                    while(reader.Read()) {
                        var bomRim = reader["notes"];
                        string value = (bomRim == DBNull.Value) ? string.Empty : bomRim.ToString();

                        if(value.Length == 0) {
                            textBox1.Text = "";
                        }
                        else {
                            textBox1.Text = value;
                        }
                    }
                    reader.Close();
                }
            }

            conn.Close();
        }

        private void bSave_Click(object sender, EventArgs e) {
            conn.Open();

            string x =
                $"UPDATE Student " +
                $"SET notes = ? " +
                $"WHERE stuName = '{comboBox1.SelectedItem}';";

            using(SQLiteCommand command = new SQLiteCommand(x, conn)) {
                command.Parameters.Add(new SQLiteParameter("?", textBox1.Text));
                command.ExecuteNonQuery();
            }

            conn.Close();

            MainForm.studentName = "";

            this.Dispose();
        }
    }
}
