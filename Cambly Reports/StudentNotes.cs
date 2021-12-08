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

namespace Cambly_Reports
{
    public partial class StudentNotes : Form
    {
        string notesInitial;

        public StudentNotes()
        {
            InitializeComponent();
        }

        private void StudentNotes_Load(object sender, EventArgs e)
        {
            //Add names to combobox1
            OleDbCommand cmd = ReportCreator.conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT TOP 10 Student.stuID, Student.sName, Lesson.lStudent " +
                    "FROM Lesson INNER JOIN Student " +
                        "ON Student.stuID = Lesson.lStudent " +
                    "ORDER BY sName;";
            OleDbDataReader read = cmd.ExecuteReader();

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

            //Initialize automcomplete
            ReportCreator.InitializeAutocomplete(ReportCreator.conn, comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string x = 
                $"SELECT notes " +
                $"FROM Student " +
                $"WHERE sName = '{comboBox1.SelectedItem}';";

            OleDbDataReader reader = new OleDbCommand(x, ReportCreator.conn).ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text = (string)reader["sName"];
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            string x = 
                $"UPDATE Student " +
                $"SET notes = ? " +
                $"WHERE sName = {comboBox1.SelectedItem};";

            OleDbCommand command = new OleDbCommand(x, ReportCreator.conn);
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("?", textBox1.Text);
            command.ExecuteNonQuery();
        }
    }
}
