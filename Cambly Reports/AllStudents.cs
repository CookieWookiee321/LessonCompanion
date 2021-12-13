using System;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Cambly_Reports
{
    public partial class AllStudents : Form
    {
        string connectionString = "server=localhost;user id=root;database=cambly;password=W3dn35d33y5#;persistsecurityinfo=True";
        MySqlConnection conn;

        public AllStudents()
        {
            InitializeComponent();
        }

        private void AllStudents_Load(object sender, EventArgs e)
        {
            dgvStudents.ColumnCount = 3;
            dgvStudents.Columns[0].Name = "Name";
            dgvStudents.Columns[1].Name = "Country";
            dgvStudents.Columns[2].Name = "First Lesson";

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();

                MySqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT sName, sCountry, firstLesson " +
                    "FROM Student " +
                    "ORDER BY sName";
                MySqlDataReader reader = command.ExecuteReader();

                string[] row;

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime date = (DateTime)reader["firstLesson"];
                        string dateString = date.ToString("yyyy/MM/dd");

                        row = new string[] {
                            $"{(string)reader["sName"]}",
                            $"{(string)reader["sCountry"]}",
                            $"{dateString}"
                        };
                        dgvStudents.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:\n" + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void AllStudents_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                //do nothing
            }
        }
    }
}
