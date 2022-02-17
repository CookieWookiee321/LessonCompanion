using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SQLite;

namespace Cambly_Reports
{
    public partial class StudentNotes : Form
    {
        SQLiteConnection conn = ReportCreator.conn;

        string notesInitial;

        public StudentNotes()
        {
            InitializeComponent();
        }

        private void StudentNotes_Load(object sender, EventArgs e)
        {
            conn.Open();

            //Add names to combobox1
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Student.stuID, Student.sName, Lesson.lStudent " +
                    "FROM Lesson INNER JOIN Student " +
                        "ON Student.stuID = Lesson.lStudent " +
                    "ORDER BY sName;";
                SQLiteDataReader read = cmd.ExecuteReader();

                if (read != null && read.HasRows)
                {
                    while (read.Read())
                    {
                        if (!comboBox1.Items.Contains((string)read["sName"]))
                        {
                            comboBox1.Items.Add((string)read["sName"]);
                        }
                    }
                }
            }

            conn.Close();

            //Select name
            if (comboBox1.Items.Contains(ReportCreator.studentName))
            {
                comboBox1.SelectedItem = ReportCreator.studentName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();

            string x = 
                $"SELECT notes " +
                $"FROM Student " +
                $"WHERE sName = '{comboBox1.SelectedItem}';";

            using (SQLiteDataReader reader = new SQLiteCommand(x, conn).ExecuteReader())
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var bomRim = reader["notes"];
                        string value = (bomRim == DBNull.Value) ? string.Empty : bomRim.ToString();

                        if (value.Length == 0)
                        {
                            textBox1.Text = "";
                        }
                        else
                        {
                            textBox1.Text = value;
                        }
                    }
                    reader.Close();
                }
            }

            conn.Close();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            conn.Open();

            string x = 
                $"UPDATE Student " +
                $"SET notes = ? " +
                $"WHERE sName = '{comboBox1.SelectedItem}';";

            using (SQLiteCommand command = new SQLiteCommand(x, conn))
            {
                command.Parameters.Add(new SQLiteParameter("?", textBox1.Text));
                command.ExecuteNonQuery();
            }

            conn.Close();

            ReportCreator.studentName = null;

            this.Dispose();
        }
    }
}
