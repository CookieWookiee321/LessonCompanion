using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text;

namespace Cambly_Reports
{
    public partial class childNewStudent : Form
    {
        string connectionString = "server=localhost;user id=root;database=cambly;password=W3dn35d33y5#;persistsecurityinfo=True";
        SQLiteConnection conn = ReportCreator.conn;

        public childNewStudent()
        {
            InitializeComponent();
        }

        private void childNewStudent_Load(object sender, EventArgs e)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            tbTopic.Text = "Intro";
        }

        public int FindStuID(string studentName)
        {
            int returnValue = -1;
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT stuID FROM Student WHERE sName = '{studentName}'";
            SQLiteDataReader dbRead = cmd.ExecuteReader();

            if (dbRead.HasRows)
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

        private int NewStuID()
        {
            string x = 
                $"SELECT stuID " +
                $"FROM student " +
                $"ORDER BY stuID DESC " +
                $"LIMIT 1;";

            SQLiteDataReader r = new SQLiteCommand(x, conn).ExecuteReader();

            int ret = -1;

            while (r.Read())
            {
                ret = (int)r[0] + 1;
            }

            r.Close();
            return ret;
        }

        private void btnCAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = tbxCName.Text;
                int counter = 0;

                while (FindStuID(name) != -1)
                {
                    counter++;
                    name = tbxCName.Text + " " + counter;
                }

                int stuID = NewStuID();

                StringBuilder sb = new StringBuilder();
                sb.Append(tbxCFirstLesson.Text);
                sb = sb.Replace('/', '-');
                sb.Append(" 00:00:00");
                string correctDateTime = sb.ToString();

                SQLiteCommand cmd = conn.CreateCommand();        //add new student to DB
                cmd.CommandText = "INSERT INTO Student(stuID, sName, sNationality, firstLesson)" +
                                    $"VALUES (@id, @name, @country, @first)";
                SQLiteParameter[] param = new SQLiteParameter[4];
                param[0] = new SQLiteParameter("@id", stuID);
                param[1] = new SQLiteParameter("@name", name);
                param[2] = new SQLiteParameter("@country", tbxCCountry.Text);
                param[3] = new SQLiteParameter("@first", correctDateTime);
                cmd.Parameters.AddRange(param);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO Lesson (lDate, lTopic, lStudent)" +
                                    $"VALUES (@date, @topic, @id)";
                param = new SQLiteParameter[3];
                param[0] = new SQLiteParameter("@date", correctDateTime);
                param[1] = new SQLiteParameter("@topic", tbTopic.Text);
                param[2] = new SQLiteParameter("@id", stuID);
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(param);

                cmd.ExecuteNonQuery();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR adding student to database... " + ex);
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbxCFirstLesson.Text = monthCalendar1.SelectionStart.ToString("yyyy/MM/dd");
            monthCalendar1.Hide();
        }

        private void tbxCFirstLesson_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void childNewStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }

        private void tbTopic_Click(object sender, EventArgs e)
        {
            tbTopic.Text = null;
        }

        private void tbTopic_Leave(object sender, EventArgs e)
        {
            if (tbTopic.Text.Length == 0)
            {
                tbTopic.Text = "Intro";
            }
        }
    }
}
