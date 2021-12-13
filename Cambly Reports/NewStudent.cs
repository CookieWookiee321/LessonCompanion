using System;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Cambly_Reports
{
    public partial class childNewStudent : Form
    {
        string connectionString = "server=localhost;user id=root;database=cambly;password=W3dn35d33y5#;persistsecurityinfo=True";
        MySqlConnection conn;

        public childNewStudent()
        {
            InitializeComponent();
        }

        private void childNewStudent_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection();               //connect to DB on load
            conn.ConnectionString = connectionString;
            conn.Open();

            tbTopic.Text = "Intro";
        }

        public int FindStuID(string studentName)
        {
            int returnValue = -1;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT stuID FROM Student WHERE sName = '{studentName}'";
            MySqlDataReader dbRead = cmd.ExecuteReader();

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

                MySqlCommand cmd = conn.CreateCommand();        //add new student to DB
                cmd.CommandText = "INSERT INTO Student(sName, sCountry, firstLesson)" +
                                    $"VALUES ('{name}', '{tbxCCountry.Text}', '{tbxCFirstLesson.Text}')";
                cmd.ExecuteNonQuery();

                int stuID = FindStuID(name);

                cmd.CommandText = "INSERT INTO Lesson (lDate, lTopic, lStudent)" +
                                            $"VALUES ('{tbxCFirstLesson.Text}', '{tbTopic.Text}', {stuID})";
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
