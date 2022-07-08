using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LessonCompanion.Report {
    internal static class ReportActions {
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

            foreach(var entry in dictionary.Keys) {
                sb.Append($"{entry}||{dictionary[entry]}");

                if(counter != max) {
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        public static Dictionary<Report.MarkerType, string> ExtractFromMarker(string input) {
            var RetDictionary = new Dictionary<Report.MarkerType, string>();
            var trueTermList = new List<char>();
            //check if any colons are present

            //q:"Question" Answer. i:"Information" Some more text e:"Example"
            int newStart = 0;
            if(input.Contains(':')) {
                while(input.Contains(':')) {
                    int index = -1;
                    foreach(var c in input) {
                        index++;
                        if(c.Equals(':')) {

                            char preceeding = input[index - 1];
                            char proceeding = input[index + 1];
                            char thisMarker = '?';

                            if(proceeding == '"') {
                                foreach(var marker in new char[] { 'q', 'i', 'e', 'p' }) {
                                    if(marker.Equals(preceeding)) {
                                        thisMarker = marker;
                                        break;
                                    }
                                }

                                //if a valid marker is found...
                                if(!thisMarker.Equals('?')) {
                                    //remove the previously input item from the base List
                                    trueTermList.RemoveAt(trueTermList.Count - 1);

                                    //extract the true term from the input
                                    newStart = input.IndexOf("\"", index + 2);
                                    string markerTerm = input[(index + 2)..newStart];

                                    //get the MarkerType associated with it
                                    //add the true input into the Dict with type as the key
                                    RetDictionary[ConvertCharToMarker(thisMarker)] = markerTerm;

                                    //reset the loop and the index
                                    index = -1;
                                    input = input[(newStart + 1)..(input.Length)];

                                    thisMarker = '?';
                                    break;
                                }
                            }


                        }
                        else {
                            trueTermList.Add(c);
                        }
                    }


                }
                RetDictionary.Add(Report.MarkerType.BASE, new string(trueTermList.ToArray()).Trim());
            }
            else {
                RetDictionary.Add(Report.MarkerType.BASE, input);
            }

            
            return RetDictionary;
        }

        private static Report.MarkerType ConvertCharToMarker(char marker) {
            switch(marker) {
                case 'q':
                    return Report.MarkerType.QUESTION;
                case 'e':
                    return Report.MarkerType.EXAMPLE;
                case 'p':
                    return Report.MarkerType.PICTURE;
                default:
                    return Report.MarkerType.INFO;
            }
        } 

        /// <summary>
        /// TODO: this currently places the end brace, but...
        /// ... the placement is only based on the final character
        /// ... deleting text is troublesome, because it keeps triggering repeated
        /// </summary>
        /// <param name="editingControl"></param>
        public static void InsertBraceEnd(DataGridViewTextBoxEditingControl editingControl) {
            //var length = editingControl.Text.Length;
            //if(length != 0) {
            //    char mostRecentCharacter = editingControl.Text[editingControl.Text.Length - 1];

            //    switch(mostRecentCharacter) {
            //        case '[':
            //            editingControl.Text += "/]";
            //            break;
            //        case '{':
            //            editingControl.Text += "}";
            //            break;
            //        case '(':
            //            editingControl.Text += ")";
            //            break;
            //        case '<':
            //            editingControl.Text += ">";
            //            break;
            //    }
            //}


        }
    }
}
