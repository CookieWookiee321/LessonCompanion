using LessonCompanion.Backend;
using System;
using System.Collections.Generic;
using System.Text;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

namespace Cambly_Reports.Backend
{
    internal class Report
    {
        public Report(
            Lesson lesson, 
            Dictionary<string, string> newLanguage, 
            Dictionary<string, string> pronunciation, 
            Dictionary<string, string> corrections) 
            {
            this.Name = DBConnect.FindStuName(lesson.StudentID);
            this.ReportID = DBConnect.FindReportID(this.Name, lesson.Date);
            this.Topic = lesson.Topic;
            this.Homework = lesson.Homework;
            this.NewLanguage = newLanguage;
            this.Pronunciation = pronunciation;
            this.Corrections = corrections;
        }

        /// <summary>
        /// Concatenates the Key and Value of each entry, so it may be stored in the database
        /// and retrieved later in a non-final state. These are seperated with a double pipe string 
        /// segment - "||".
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string DictToString(Dictionary<string, string> dictionary) {
            StringBuilder sb = new StringBuilder();
            int counter = 1;
            int max = dictionary.Count;

            foreach (var entry in dictionary.Keys) {
                sb.Append($"{entry}||{dictionary[entry]}");

                if (counter != max) {
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        public bool GeneratePdf() {
            //initial set up
            var saveDest = DBConnect.FindReportSaveLocation();
            var writer = new PdfWriter(saveDest);
            var pdf = new PdfDocument(writer);
            pdf.AddNewPage();
            var document = new Document(pdf);

            //creation
            //header
            document.Add(AddTitle($"{Name}\t{Date}"));
            document.Add(AddTitle($"{Topic}"));
            document.Add(AddTitle($"{Homework}"));

            //notes
            foreach (var category in new Dictionary<string, string>[] {
                NewLanguage, 
                Pronunciation, 
                Corrections}) 
            {
                AddTable(category);
            }

            document.Close();

            return true;
        }

        private Paragraph AddTitle(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLDOBLIQUE);
            return new Paragraph(text).SetFont(font).SetFontSize(26);
        }

        private Paragraph AddHeader(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            return new Paragraph(text).SetFont(font).SetFontSize(20);
        }

        private Paragraph AddText(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            return new Paragraph(text).SetFont(font).SetFontSize(13);
        }

        private Paragraph AddMiniText(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);
            return new Paragraph(text).SetFont(font).SetFontSize(9);
        }

        private Table AddTable(Dictionary<string, string> input) {
            bool twoColumns = true;

            foreach (var x in input.Keys) {
                if (!input[x].Equals("")) {
                    twoColumns = false;
                    break;
                }
            }

            Table table;
            if (twoColumns) {
                table = new Table(new float[] { 300, 300 });

                foreach (var x in input.Keys) {
                    table.AddCell(AddText(x));
                    table.AddCell(AddText(input[x]));
                }
            }
            else {
                table = new Table(new float[] { 600 });

                foreach (var x in input.Keys) {
                    table.AddCell(AddText(x));
                }
            }

            return table;
        }

        private Cell AddCell(string input) {
            var thisCell = new Cell();

            //TODO: check for tag 

            //check for question marker
            if (input.Contains("[q]") && input.Contains("[/q]")) {
                //[q]Who are you?[/q] He's Mike
                int index = input.IndexOf("[/q]");
                string questionX = input.Substring(index, 3).Trim(); //[q]Who are you?
                string question = questionX.Substring(questionX.IndexOf('[') + 1); //[q]Who are you?
                string answer = input.Substring(index + 3, input.Length - 1).Trim(); //He's Mike

            }

            this.AddText(input);
        }

        public int ReportID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public string Homework { get; set; }
        public Dictionary<string, string> NewLanguage { get; set; }
        public Dictionary<string, string> Pronunciation { get; set; }
        public Dictionary<string, string> Corrections { get; set; }
    }
}
