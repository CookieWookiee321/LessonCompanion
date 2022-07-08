using System;
using System.Windows.Forms;

namespace LessonCompanion.Frontend {
    public partial class ChangeLog : Form {
        public ChangeLog() {
            InitializeComponent();
        }

        private void ChangeLog_Load(object sender, EventArgs e) {
            tLog.Text =
@"2022/06/23:
Cursor now changes to hourglass while processing report output.
'Always on Top' setting now operational.

2022/06/22:
Got the student list dialog working.

2022/06/21:
Updated the report generation.
Using a mixture of single-column lines and double-column lines is now possible. (previously a table could only support either single or double-columns)

2022/06/19:
Changed some UI stuff and gave the main window a new font. Need to change the rest if it sticks around.
The Lesson List dialog works now. One of the others doesn't work, and the next one will just straight up crash the program.
On the previous version of this there were much more features which I'd like to add slowly.
Also, I wanna do some customisation stuff and settings.
";
        }
    }
}
