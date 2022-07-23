using LessonCompanion.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace LessonCompanion.Logic {
    public static class DBConnect {
        public static SQLiteConnection Connection {
            get { return new SQLiteConnection("Data Source=classes.sqlite;Version=3;"); }
        }

        public static void CreateDatabase() {
            using SQLiteConnection conn = Connection;
            conn.Open();

            string tables = @"
                    CREATE TABLE Preferences (
                        settingName TEXT NOT NULL, 
                        settingValue TEXT NOT NULL);

                    CREATE TABLE Students (
                        stuID INT PRIMARY KEY NOT NULL,
                        stuName TEXT NOT NULL,
                        isCurrent INT NOT NULL,
                        CONSTRAINT con_Uni UNIQUE (stuName));

                    CREATE TABLE Lessons (
                        lessID INT PRIMARY KEY,
                        stuID INT NOT NULL,
                        lessDate DATETIME NOT NULL,
                        lessTopic TEXT NOT NULL,
                        lessHomework TEXT,
                        lessMemo TEXT,
                        FOREIGN KEY (stuID) REFERENCES Students (stuID));

                    CREATE TABLE Reports (
                        repID INT PRIMARY KEY,                        
                        stuID INT NOT NULL,
                        stuName TEXT NOT NULL,
                        lessDate DATETIME NOT NULL,
                        lessTopic TEXT NOT NULL,
                        lessHomework TEXT,
                        newLanguage TEXT,
                        pronunciation TEXT,
                        corrections TEXT);

                    CREATE TABLE PdfHeadings (  
                        headPosition INT PRIMARY KEY,
                        headName TEXT NOT NULL);

                    CREATE TABLE PdfDictionary (
                        headName TEXT NOT NULL,
                        dictTag TEXT NOT NULL,
                        dictReference TEXT NOT NULL,
                        dictAutoFill TEXT NOT NULL,
                        PRIMARY KEY (headname, dictTag, dictReference));";

            string populate = @"
                    INSERT INTO Preferences (settingName, settingValue)
                    VALUES  ('theme', 'Light');

                    INSERT INTO PdfHeadings (headPosition, headName)
                    VALUES  (1, 'New Language'),
                            (2, 'Pronunciation'),
                            (3, 'Corrections'),
                            (4, '[check the options menu to set or disable this footer]');";

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Lesson Companion\\";
            string saveDir = @$"
                    INSERT INTO Preferences (settingName, settingValue) 
                    VALUES  ('saveDirectory', '{path}');";

            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = tables;
            command.ExecuteNonQuery();

            command.CommandText = populate;
            command.ExecuteNonQuery();

            command.CommandText = saveDir;
            command.ExecuteNonQuery();
            conn.Close();
        }

        #region FIND_________________________________________________________________
        public static string FindStudentName(int id) {
            string ret = "";

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                    SELECT stuName 
                    FROM Students 
                    WHERE stuID = @id";
            SQLiteParameter n = new SQLiteParameter("@id", id);
            cmd.Parameters.Add(n);

            using SQLiteDataReader dbRead = cmd.ExecuteReader();
            if(dbRead != null && dbRead.HasRows) {
                while(dbRead.Read()) {
                    ret = (string)dbRead["stuName"];
                }
            }
            return ret;
        }

        public static int FindStudentID(string studentName) {
            int ret = -1;

            using var conn = Connection;
            //TRY FIND EXISTING ENTRY
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                SELECT stuID 
                FROM Students 
                WHERE stuName = @name";
            SQLiteParameter n = new SQLiteParameter("@name", studentName);
            cmd.Parameters.Add(n);

            using SQLiteDataReader dbRead = cmd.ExecuteReader();

            if(dbRead != null && dbRead.HasRows) {
                while(dbRead.Read()) {
                    ret = (int)dbRead["stuID"];
                }
            }
            conn.Close();

            return ret;
        }

        public static int FindReportID(string studentName, DateTime lessDate) {
            int ret = -1;

            using var conn = Connection;
            //TRY FIND EXISTING ENTRY
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                SELECT repID 
                FROM Reports 
                WHERE stuName = @name 
                AND lessDate = @date";
            SQLiteParameter n = new SQLiteParameter("@name", studentName);
            SQLiteParameter d = new SQLiteParameter("@date", lessDate);
            cmd.Parameters.Add(n);
            cmd.Parameters.Add(d);

            using SQLiteDataReader dbRead = cmd.ExecuteReader();

            if(dbRead != null && dbRead.HasRows) {
                while(dbRead.Read()) {
                    var retFalse = dbRead["repID"];
                    ret = (int)retFalse;
                }
            }
            conn.Close();

            return ret;
        }

        public static string FindReportSaveLocation(Report.Report report) {
            string ret = "";

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT * 
                FROM Preferences 
                WHERE settingName = 'saveDirectory';";

            using var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                ret = (string)reader["settingValue"];
            }

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Lesson Companion\\";
            if(!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            ret += $"{report.Date.ToShortDateString().Replace('/', '-')} - {report.Name}.pdf";

            return ret;
        }

        public static string[][] FindLessonsByStudent(string name) {
            var ret = new List<string[]>();

            using var conn = Connection;
            conn.Open();
            string commandText = @"
                SELECT lessDate, lessTopic, lessHomework 
                FROM Lessons 
                WHERE stuID = (SELECT stuID 
                               FROM Students 
                               WHERE stuName = @name)
                ORDER BY lessDate DESC;";
            var cmd = new SQLiteCommand(commandText, conn);
            cmd.Parameters.Add(new SQLiteParameter("@name", name));

            using var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                DateTime dateTrue;
                string dateRaw;
                long dateLong;
                try {
                    dateTrue = (DateTime)reader["lessDate"];
                }
                catch(Exception) {
                    dateRaw = (string)reader["lessDate"];
                    dateLong = long.Parse(dateRaw);
                    dateTrue = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTrue = dateTrue.AddSeconds(dateLong).ToLocalTime();
                }
                var dateStr = dateTrue.ToShortDateString();

                ret.Add(new string[] {
                    dateStr,
                    (string)reader["lessTopic"],
                    (string)reader["lessHomework"]
                });
            }

            return ret.ToArray();
        }

        public static string[] FindStudentNamesRecent() {
            var retList = new List<string>();

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"
                    SELECT DISTINCT s.stuName
                    FROM Lessons as l 
                    JOIN Students as s
                    ON s.stuID = l.stuID
                    ORDER BY lessDate DESC;";

            using SQLiteDataReader read = cmd.ExecuteReader();
            string name;
            if(read != null && read.HasRows) {
                while(retList.Count <= 10 && read.Read()) {
                    name = read.GetString(0);
                    if(!retList.Contains(name)) {
                        retList.Add(name);
                    }
                }
            }

            return retList.ToArray();
        }

        /// <summary>
        /// </summary>
        /// <param name="isOrdered"></param>
        /// <returns>Returns an array of every student's name in the database. This array may be in alphabetical order, or in their order of creation.</returns>
        public static string[] FindStudentNamesAll(bool isOrdered) {
            var retList = new List<string>();

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();

            if (!isOrdered) {
                cmd.CommandText = @"
                    SELECT s.stuName
                    FROM Students as s;";
            }
            else {
                cmd.CommandText = @"
                    SELECT s.stuName
                    FROM Students as s
                    ORDER BY stuName;";
            }

            using SQLiteDataReader read = cmd.ExecuteReader();
            if(read != null && read.HasRows) {
                while(read.Read()) {
                    retList.Add(read.GetString(0));
                }
            }

            return retList.ToArray();
        }

        public static string[] FindStudentNamesActive() {
            var retList = new List<string>();

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT s.stuName
                FROM Students as s
                WHERE isCurrent = 1;";

            using SQLiteDataReader read = cmd.ExecuteReader();
            if(read != null && read.HasRows) {
                while(read.Read()) {
                    retList.Add(read.GetString(0));
                }
            }

            return retList.ToArray();
        }

        /// <summary>
        /// Method intended to populate the ListBox in the Report History Form
        /// </summary>
        /// <returns>Returns an array containing the date and student name of every report in descending order of creation</returns>
        public static string[] FindReportsBasicInfo() {
            var retList = new List<string>();
            string query = @"
                SELECT lessDate, stuName 
                FROM Reports 
                ORDER BY repID DESC";

            using var conn = Connection;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = query;

            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var dt = (DateTime)reader[0];
                string date = dt.ToShortDateString();

                retList.Add($"{date} : {reader[1]}");
            }

            return retList.ToArray();
        }

        public static string[] FindReportsDetailedInfo(DateTime date, string name) {
            var retArr = new string[7];
            string query = @"
                SELECT *
                FROM Reports 
                WHERE lessDate = @1 AND stuName = @2";

            using var conn = Connection;
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(new SQLiteParameter[] {
                new SQLiteParameter("@1", date),
                new SQLiteParameter("@2", name)
            });

            using var reader = cmd.ExecuteReader();
            reader.Read();
            retArr[0] = name;
            retArr[1] = date.ToShortDateString();
            retArr[2] = (string)reader["lessTopic"];
            retArr[3] = (string)reader["lessHomework"];
            retArr[4] = (string)reader["newLanguage"];
            retArr[5] = (string)reader["pronunciation"];
            retArr[6] = (string)reader["corrections"];

            return retArr;
        }
        #endregion

        #region NEW_________________________________________________________________
        public static int NewStudentID() {
            int ret = -1;

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                    SELECT stuID 
                    FROM Students 
                    ORDER BY stuID DESC 
                    LIMIT 1;";
            using SQLiteDataReader dbRead = cmd.ExecuteReader();
            while(dbRead.Read()) {
                ret = (int)dbRead["stuID"] + 1;
            }
            return ret;
        }

        public static int NewLessonID() {
            int ret = -1;

            using var conn = Connection;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                SELECT lessID 
                FROM Lessons 
                ORDER BY lessID DESC 
                LIMIT 1;";
            using var dbRead = cmd.ExecuteReader();

            while(dbRead.Read()) {
                ret = (int)dbRead["lessID"] + 1;
            }

            return ret;
        }

        public static int NewReportID() {
            int ret = -1;

            using var conn = Connection;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                SELECT repID 
                FROM Reports 
                ORDER BY repID DESC 
                LIMIT 1;";

            using var dbRead = cmd.ExecuteReader();

            while(dbRead.Read()) {
                var l = dbRead["repID"];
                ret = (int)l + 1;
            }

            return ret;
        }
        #endregion

        #region INSERT_________________________________________________________________
        public static bool InsertStudent(Student student) {
            bool ret = false;

            if(student.Id == -1) {
                student.Id = NewStudentID();
            }

            using var conn = Connection;
            conn.Open();
            string studentInfo = @$"
                    INSERT INTO Students (stuID, stuName, isCurrent) 
                    VALUES (@1, @2, @3)";
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = studentInfo;

            SQLiteParameter[] parameters = new SQLiteParameter[] {
                    new SQLiteParameter("@1", student.Id),
                    new SQLiteParameter("@2", student.Name),
                    new SQLiteParameter("@3", student.ActiveInt)
                };
            cmd.Parameters.AddRange(parameters);
            var affectedRows = cmd.ExecuteNonQuery();

            if(affectedRows == 1) {
                ret = true;
            }

            conn.Close();
            return ret;
        }

        public static bool InsertReport(Report.Report report) {
            bool ret = false;
            using var conn = Connection;
            conn.Open();

            string studentInfo = @$"
                INSERT INTO Reports (
                    repID,
                    stuID, 
                    stuName, 
                    lessDate, 
                    lessTopic, 
                    lessHomework, 
                    newLanguage, 
                    pronunciation, 
                    corrections) 
                VALUES (@repId, @1, @2, @3, @4, @5, @6, @7, @8)";
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = studentInfo;

            SQLiteParameter[] parameters = new SQLiteParameter[] {
                new SQLiteParameter("@repId", report.ReportID),
                new SQLiteParameter("@1", report.StudentID),
                new SQLiteParameter("@2", report.Name),
                new SQLiteParameter("@3", report.Date),
                new SQLiteParameter("@4", report.Topic),
                new SQLiteParameter("@5", report.Homework),
                new SQLiteParameter("@6", Report.ReportActions.DictToString(report.NewLanguage)),
                new SQLiteParameter("@7", Report.ReportActions.DictToString(report.Pronunciation)),
                new SQLiteParameter("@8", Report.ReportActions.DictToString(report.Corrections))
            };
            cmd.Parameters.AddRange(parameters);

            if(cmd.ExecuteNonQuery() >= 1) {
                ret = true;
            }
            else {
                ret = true;
            }

            conn.Close();
            return ret;
        }

        public static bool InsertLesson(Lesson lesson) {
            bool ret = false;
            using var conn = Connection;
            conn.Open();

            string studentInfo = @$"
                    INSERT INTO Lessons (
                        lessID, 
                        stuID, 
                        lessDate,
                        lessTopic, 
                        lessHomework) 
                    VALUES (@1, @2, @3, @4, @5)";
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = studentInfo;

            SQLiteParameter[] parameters = new SQLiteParameter[] {
                    new SQLiteParameter("@1", lesson.LessonID),
                    new SQLiteParameter("@2", lesson.StudentID),
                    new SQLiteParameter("@3", lesson.Date),
                    new SQLiteParameter("@4", lesson.Topic),
                    new SQLiteParameter("@5", lesson.Homework)
                };
            cmd.Parameters.AddRange(parameters);

            if(cmd.ExecuteNonQuery() >= 1) {
                ret = true;
            }

            conn.Close();
            return ret;
        }
        #endregion

        #region UPDATE_________________________________________________________________
        public static bool UpdateReport(Report.Report report) {
            bool ret = false;
            using var conn = Connection;
            conn.Open();

            string studentInfo = @$"
                    UPDATE Reports 
                    SET stuID = @1, 
                        stuName = @2, 
                        lessDate = @3, 
                        lessTopic = @4, 
                        lessHomework = @5, 
                        newLanguage = @6, 
                        pronunciation = @7, 
                        corrections = @8
                    WHERE repID = @id;";
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = studentInfo;

            SQLiteParameter[] parameters = new SQLiteParameter[] {
                    new SQLiteParameter("@1", report.ReportID),
                    new SQLiteParameter("@2", report.Name),
                    new SQLiteParameter("@3", report.Date),
                    new SQLiteParameter("@4", report.Topic),
                    new SQLiteParameter("@5", report.Homework),
                    new SQLiteParameter("@6", Report.ReportActions.DictToString(report.NewLanguage)),
                    new SQLiteParameter("@7", Report.ReportActions.DictToString(report.Pronunciation)),
                    new SQLiteParameter("@8", Report.ReportActions.DictToString(report.Corrections))
                };
            cmd.Parameters.AddRange(parameters);

            if(cmd.ExecuteNonQuery() >= 1) {
                ret = true;
            }

            conn.Close();
            return ret;
        }

        public static bool UpdateStudentActive(string studentName, bool newState) {
            int db = newState ? 1 : 0;

            string query = $@"
                UPDATE Students 
                SET isCurrent = @active 
                WHERE stuName = @name";

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddRange(new SQLiteParameter[] {
                new SQLiteParameter("@active", db),
                new SQLiteParameter("@name", studentName)
            });

            if(cmd.ExecuteNonQuery() == 1) {
                return true;
            }
            return false;
        }

        public static bool UpdateStudentName(string oldName, string newName) {
            string query = $@"
                UPDATE Students 
                SET stuName = @new 
                WHERE stuName = @old";

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddRange(new SQLiteParameter[] {
                new SQLiteParameter("@new", newName),
                new SQLiteParameter("@old", oldName)
            });

            if(cmd.ExecuteNonQuery() == 1) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is only able to update topic and homework details currently.
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="date"></param>
        /// <param name="topic"></param>
        /// <param name="homework"></param>
        /// <returns>True if update is successful, or else false if not.</returns>
        public static bool UpdateLessonDetails(string studentName, DateTime date, string topic, string homework) {
            string query = @"
                UPDATE Lessons 
                SET lessTopic = @topic AND lessHomework = @homework 
                WHERE lessDate = @date AND stuID = (SELECT stuID 
                                                    FROM Students 
                                                    WHERE stuName = @name);";

            using var conn = Connection;
            conn.Open();
            using var cmd = new SQLiteCommand(query, conn);
            var param = new SQLiteParameter[] { 
                new SQLiteParameter("@topic", topic),
                new SQLiteParameter("@homework", homework),
                new SQLiteParameter("@date", date),
                new SQLiteParameter("@name", studentName)
            };
            cmd.Parameters.AddRange(param);
            return cmd.ExecuteNonQuery() == 1;
        }
        #endregion

        #region DELETE_________________________________________________________________
        public static bool DeleteStudent(string stuName) {
            string query = $@"
                DELETE FROM Students 
                WHERE stuName = @name";

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddRange(new SQLiteParameter[] {
                new SQLiteParameter("@name", stuName)
            });

            if(cmd.ExecuteNonQuery() == 1) {
                return true;
            }
            return false;
        }

        public static bool DeleteReport(DateTime date, string studentName) {
            string query = $@"
                DELETE FROM Reports 
                WHERE stuName = @name AND lessDate = @date";

            using var conn = Connection;
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddRange(new SQLiteParameter[] {
                new SQLiteParameter("date", date),
                new SQLiteParameter("@name", studentName)
            });

            if (cmd.ExecuteNonQuery() == 1) {
                return true;
            }
            return false;
        }
        #endregion
    }
}
