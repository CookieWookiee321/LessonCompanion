using LessonCompanion.Logic;
using LessonCompanion.Logic.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LessonCompanion.Report {
    public class Report {
        private int stuId;
        private int repId;
        private string stuName;
        private DateTime lessDate;
        private string lessTopic;
        private string lessHomework;
        private Dictionary<string, string> newLang;
        private Dictionary<string, string> pron;
        private Dictionary<string, string> corr;
        private Dictionary<string, List<string>> tags;

        //Markers
        public static readonly char[] MarkerCharacters = new char[] { 'q', 'e', 'i', 'p' };
        public static readonly char[] BraceOpen = new char[] { '[', '<', '(', '{' };

        public enum MarkerType {
            QUESTION, INFO, EXAMPLE, PICTURE, BASE
        }

        //CONSTRUCTOR
        public Report(Lesson lesson,
                Dictionary<string, string> newLanguage,
                Dictionary<string, string> pronunciation,
                Dictionary<string, string> corrections) {
            Name = DBConnect.FindStudentName(lesson.StudentID);
            ReportID = DBConnect.FindReportID(Name, lesson.Date);
            Date = lesson.Date;
            Topic = lesson.Topic;
            Homework = lesson.Homework;
            NewLanguage = newLanguage;
            Pronunciation = pronunciation;
            Corrections = corrections;
        }

        public bool Create() {
            try {
                //initial set up
                var saveDest = DBConnect.FindReportSaveLocation(this);
                var writer = new PdfWriter(saveDest);
                var pdf = new PdfDocument(writer);
                pdf.AddNewPage();
                var document = new Document(pdf);

                //creation
                //header
                var cellName = new Cell()
                    .Add(Components.CTitle(Name, iText.Layout.Properties.HorizontalAlignment.LEFT))
                    .SetBorder(Border.NO_BORDER);
                var cellDate = new Cell()
                    .Add(Components.CTitle(Date.ToShortDateString(), iText.Layout.Properties.HorizontalAlignment.RIGHT))
                    .SetBorder(Border.NO_BORDER);
                var tableTitle = new Table(new float[] { 300, 300 })
                    .AddCell(cellName)
                    .AddCell(cellDate);

                Table tableHeader = new Table(new float[] { 100, 500 });
                var cellTopicHeader = new Cell()
                    .Add(Components.CHeader("Topic:"))
                    .SetBorder(Border.NO_BORDER);
                var cellTopic = new Cell()
                    .Add(Components.CHeader(Topic))
                    .SetBorder(Border.NO_BORDER);
                tableHeader
                    .AddCell(cellTopicHeader)
                    .AddCell(cellTopic);
                if(!Homework.Equals("")) {
                    var cellHomeworkHeader = new Cell()
                        .Add(Components.CHeader("Homework:"))
                        .SetBorder(Border.NO_BORDER);
                    var cellHomework = new Cell()
                        .Add(Components.CHeader(Homework))
                        .SetBorder(Border.NO_BORDER);
                    tableHeader
                        .AddCell(cellHomeworkHeader)
                        .AddCell(cellHomework);
                }

                document
                    .Add(tableTitle)
                    .Add(tableHeader);

                document.Add(Components.CSeperatorHorizontal());

                int counter = 0;
                string header = "";

                //notes
                foreach(var category in new Dictionary<string, string>[] {
                    NewLanguage,
                    Pronunciation,
                    Corrections}) 
                {
                    if(category.Count != 0) {
                        switch(counter) {
                            case 0:
                                header = "New Language";
                                break;
                            case 1:
                                header = "Pronunciation";
                                break;
                            case 2:
                                header = "Corrections";
                                break;
                        }

                        document
                            .Add(Components.CHeader2(header))
                            .Add(Components.CTable(category));
                    }
                    counter++;
                }

                document.Close();

                return true;
            }
            catch(Exception e) {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        #region PROPERTIES
        public int StudentID { get => stuId; set => stuId = value; }
        public int ReportID { get => repId; set => repId = value; }
        public string Name { get => stuName; set => stuName = value; }
        public DateTime Date { get => lessDate; set => lessDate = value; }
        public string Topic { get => lessTopic; set => lessTopic = value; }
        public string Homework { get => lessHomework; set => lessHomework = value; }
        public Dictionary<string, string> NewLanguage { get => newLang; set => newLang = value; }
        public Dictionary<string, string> Pronunciation { get => pron; set => pron = value; }
        public Dictionary<string, string> Corrections { get => corr; set => corr = value; }
        #endregion
    }
}