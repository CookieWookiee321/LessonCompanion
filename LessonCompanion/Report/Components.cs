using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Element;
using System.Collections.Generic;
using static LessonCompanion.Report.Style;

namespace LessonCompanion.Report {
    internal static class Components {

        public static Cell CCell(string input) {
            return CCell(input, 1);
        }

        public static Cell CCell(string input, int colSpan) {
            var thisCell = new Cell(1, colSpan);
            var markers = ReportActions.ExtractFromMarker(input);

            //QUESTION MARKER
            if(markers.ContainsKey(Report.MarkerType.QUESTION)) {
                thisCell.Add(CTextQuestion(markers[Report.MarkerType.QUESTION]));
            }

            //BASE TERM
            if(markers.ContainsKey(Report.MarkerType.BASE)) {
                thisCell.Add(CText(markers[Report.MarkerType.BASE]));
            }

            //INFO MARKER
            if(markers.ContainsKey(Report.MarkerType.INFO)) {
                thisCell.Add(CTextInfo(markers[Report.MarkerType.INFO]));
            }

            //EXAMPLE MARKER
            if(markers.ContainsKey(Report.MarkerType.EXAMPLE)) {
                thisCell.Add(CTextExample(markers[Report.MarkerType.EXAMPLE]));
            }

            return thisCell;
        }

        public static Paragraph CTitle(string text, iText.Layout.Properties.HorizontalAlignment alignment) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            return new Paragraph(text)
                        .SetFontColor(Colours.rgbPurpleDark)
                        .SetFont(font)
                        .SetFontSize(26)
                        .SetHorizontalAlignment(alignment);
        }

        public static Paragraph CHeader(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            return new Paragraph(text)
                        .SetFontColor(Colours.rgbPurpleDark)
                        .SetFont(font)
                        .SetFontSize(20);
        }

        public static Paragraph CHeader2(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            return new Paragraph(text)
                        .SetFontColor(Colours.rgbPurpleLight)
                        .SetFont(font)
                        .SetFontSize(16);
        }

        public static Paragraph CSeperatorHorizontal() {
            var ret = new Paragraph();

            var line = new SolidLine(1f);
            line.SetColor(Colours.rgbPurpleDark);

            var sep = new LineSeparator(line);
            sep.SetStrokeWidth(1f);
            sep.SetMarginTop(6f);
            sep.SetMarginBottom(6f);

            ret.Add(sep);

            return ret;
        }

        public static Table CTable(Dictionary<string, string> input) {
            Table table = new Table(new float[] { 300, 300 });

            foreach(var x in input.Keys) {
                if(input[x].Equals("")) {
                    table.AddCell(CCell(x, 2));
                }
                else {
                    table.AddCell(CCell(x));
                    table.AddCell(CCell(input[x]));
                }
            }

            return table;
        }

        public static Paragraph CText(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            return new Paragraph(text)
                        .SetFont(font)
                        .SetFontSize(13);
        }

        public static Paragraph CTextExample(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            return new Paragraph(text)
                        .SetFontColor(Colours.rgbOrange)
                        .SetFont(font)
                        .SetFontSize(9);
        }

        public static Paragraph CTextInfo(string text) {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            return new Paragraph(text)
                        .SetFontColor(Colours.rgbGreen)
                        .SetFont(font)
                        .SetFontSize(9);
        }

        public static Paragraph CTextQuestion(string text) {
            var fontHelveticaIt = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

            return new Paragraph(text)
                        .SetFontColor(Colours.rgbBlue)
                        .SetFont(fontHelveticaIt)
                        .SetFontSize(9);
        }
    }
}
