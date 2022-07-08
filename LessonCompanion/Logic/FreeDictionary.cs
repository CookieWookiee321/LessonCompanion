using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace LessonCompanion.Logic {
    internal class FreeDictionary {
        private string fullJson;
        private string term;
        private List<string> phonetics;
        private Dictionary<string, string[]> meanings;

        public FreeDictionary(string input) {
            input = input.Replace(" ", "%20");
            var url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + input;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            using var streamReader = new StreamReader(response.GetResponseStream());
            fullJson = (string)JObject.Parse(streamReader.ReadToEnd());

            term = input;
            phonetics = GetPhonetics();
            meanings = GetMeanings();
        }

        /// <summary>
        /// Retrieves phonetic data.
        /// </summary>
        /// <returns>Returns a List with mappings to means of pronunciation
        /// of the given term. Each entry can be accessed through int IDs.
        /// IPA notation and a paired URL to a sound file are concanenated
        /// in a string, conjoined with a double pipe marker - "||".</returns>
        private List<string> GetPhonetics() {
            var ret = new List<string>();

            int start = fullJson.IndexOf("\"phonetics\":[{") + 14;
            int end = fullJson.IndexOf("}]");

            string chunk = fullJson.Substring(start, end);
            var options = chunk.Split("},{");

            foreach(var o in options) {
                string text = o.Substring(
                    o.IndexOf("\"text\":\"") + 8,
                    o.IndexOf("\","));
                string audio = o.Substring(
                    o.IndexOf("\"audio\":\"") + 9,
                    o.IndexOf("\","));
                ret.Add($"{text}||{audio}");
            }
            return ret;
        }

        /// <summary>
        /// Retrieves meanings associated with the term
        /// </summary>
        /// <returns>Returns a dictionary which contains all parts of speech 
        /// associated with the word, and all definitions associated with 
        /// the parts of speech.</returns>
        private Dictionary<string, string[]> GetMeanings() {
            var ret = new Dictionary<string, string[]>();

            //split based on total # of parts of speech
            string[] partsOfSpeech = fullJson.Split("{\"partOfSpeech\":\"");
            foreach(var partChunk in partsOfSpeech) {
                //isolate part of speech name
                string partProper = partChunk[..partChunk.IndexOf("\",")];

                //split based on total # of definitions
                List<string> definitionList = new List<string>();
                string[] definitions = partChunk.Split("{\"definition\":\"");
                foreach(var defChunk in definitions) {
                    //isolate definition + add to list
                    string defProper = defChunk[..defChunk.IndexOf("\",")];
                    definitionList.Add(defProper);
                }

                ret.Add(partProper, definitionList.ToArray());
            }
            return ret;
        }

        public string FullDetails { get => fullJson; }
        public string Term { get => term; }
        public List<string> Phonetics { get => phonetics; }
        public Dictionary<string, string[]> Meanings { get => meanings; }
    }
}
