using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LessonCompanion.Logic {
    public static class CompanionActions {

        public static string ConvertDateFormat(string originalDateFormat) {
            string[] splitDate = originalDateFormat.Split('/', 3);

            return string.Concat($"{splitDate[0]}-{splitDate[1]}-{splitDate[2]}");
        }

        public static Dictionary<string, string> DgvToDict(DataGridView dgv) {
            try {
                var ret = new Dictionary<string, string>();

                if(dgv.Rows.Count != 0) {
                    for(int i = 0 ; i < dgv.RowCount ; i++) {
                        string firstCell = (string)dgv.Rows[i].Cells[0].Value;
                        string secondCell = (string)dgv.Rows[i].Cells[1].Value;

                        if(firstCell != null) {
                            if(secondCell != null) {
                                ret.Add(firstCell, secondCell);
                            }
                            else {
                                ret.Add(firstCell, "");
                            }
                        }
                    }
                }

                return ret;
            }
            catch(ArgumentException ex) {
                MessageBox.Show(
                    "The left-hand column cannot contain any duplicates.",
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                var ret = new Dictionary<string, string>();
                ret.Add("1011909", "1011909");
                return ret;
            }
        }

        public static bool TablesContainData(DataGridView table) {
            return TablesContainData(new DataGridView[] { table });
        }

        public static bool TablesContainData(DataGridView[] tables) {
            //loop through each table
            foreach(var table in tables) {
                //loop through each row
                for(int i = 0 ; i < table.Rows.Count ; i++) {
                    //get the value of the each cell
                    string lhs = (string)table.Rows[i].Cells[0].Value;
                    string rhs = (string)table.Rows[i].Cells[0].Value;

                    //return true if any value is found at any point
                    if(lhs != null || rhs != null) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}