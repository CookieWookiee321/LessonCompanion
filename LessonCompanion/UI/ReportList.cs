using LessonCompanion.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cambly_Reports.UI {
    public partial class ReportList : Form {
        private string[] basicDetails;
        public static Dictionary<string, string> fullDetails = new Dictionary<string, string>() { 
            { "name", "" }, { "date", "" }, { "topic", "" }, { "homework", "" }, 
            { "newLang", "" }, { "pron", "" }, { "corr", "" }
        };

        public ReportList() {
            InitializeComponent();
        }

        //METHODS__________________________________________________________________________________

        private DateTime StringToDateTime(string input) {
            string[] dateSpl = input.Split('/');
            int YY = int.Parse(dateSpl[0]);
            int MM = int.Parse(dateSpl[1]);
            int DD = int.Parse(dateSpl[2]);
            return new DateTime(YY, MM, DD);
        }

        private List<List<string[]>> FormatFeedback(string[] rawData) {
            var retList = new List<List<string[]>>();
            string lhs;
            string rhs;

            for (int i = 4 ; i < 6 ; i++) {
                retList.Add(new List<string[]>());

                //loop through every line of this table
                foreach (var line in rawData[i].Split("\n")) {
                    //split into left and right hands sides
                    rhs = "";

                    if (!line.Contains("||")) {
                        lhs = line;
                    }
                    else {
                        var parts = line.Split("||");

                        lhs = parts[0];
                        if (parts.Length > 1) {
                            rhs = parts[1];
                        }
                    }

                    switch (i) {
                        case 4:
                            retList[0].Add(new string[] { lhs, rhs });
                            break;
                        case 5:
                            retList[1].Add(new string[] { lhs, rhs });
                            break;
                        case 6:
                            retList[2].Add(new string[] { lhs, rhs });
                            break;
                        default:
                            break;
                    }
                }
            }

            return retList;
        }

        //EVENT HANDLERS___________________________________________________________________________

        private void ReportList_Load(object sender, EventArgs e) {
            tableLayoutPanel1.Visible = false;
            
            //get list of every report, with order of creation
            basicDetails = DBConnect.FindReportsBasicInfo();
            //assign to the ListBox
            lbReportList.DataSource = basicDetails;
        }

        //TODO: Make sure that ':' is an illegal char for names
        private void lbReportList_SelectedValueChanged(object sender, EventArgs e) {
            if (!tableLayoutPanel1.Visible) {
                //unhide the table if this is the first item selected
                tableLayoutPanel1.Visible = true;
            }
            if (!bLoad.Enabled) {
                bLoad.Enabled = true;
            }
            if (!bDelete.Enabled) {
                bDelete.Enabled = true;
            }

            //get the name and date strings from the ListBox entry
            var spl = lbReportList.SelectedValue.ToString().Split(":");
            string name = spl[1].Trim();
            string[] dateSpl = spl[0].Split('/');
            DateTime date = StringToDateTime(spl[0]);

            //find the details of the currently selected report
            var details = DBConnect.FindReportsDetailedInfo(date, name);

            //assign to Dictionary
            fullDetails["name"] = name;
            fullDetails["date"] = date.ToShortDateString();
            fullDetails["topic"] = details[2];
            fullDetails["homework"] = details[3];
            fullDetails["newLang"] = details[4];
            fullDetails["pron"] = details[5];
            fullDetails["corr"] = details[6];

            

            //TODO: Make sure double pipe is banned from new lang, pron, and corr
            //method

            //display on the screen
            tTopic.Text = fullDetails["topic"];
            tHomework.Text = fullDetails["homework"];

            //format the New Language, Pronunciation, and Corrections data
            var formattedDetails = FormatFeedback(DBConnect.FindReportsDetailedInfo(date, name));
            for (int i = 0 ; i < 3 ; i++) {



                switch (i) {
                    case 0:

                    default:
                        break;
                }
            }

            //make data accessible to main form

            
        }

        private void bDelete_Click(object sender, EventArgs e) {
            try {
                DBConnect.DeleteReport(StringToDateTime(fullDetails["date"]), fullDetails["name"]);
                lbReportList.Items.RemoveAt(lbReportList.SelectedIndex);
                MessageBox.Show("Report entry deleted");
            }
            catch (Exception) {

                throw;
            }
        }

        private void bLoad_Click(object sender, EventArgs e) {

        }

        private void bClose_Click(object sender, EventArgs e) {

        }
    }
}
