using Cambly_Reports.Backend;
using System;
using System.Data.SQLite;
using System.IO;

namespace LessonCompanion.Backend
{
    class DBConnect
    {
        public static SQLiteConnection Connection {
            get { return new SQLiteConnection("Data Source=classes.sqlite;Version=3;"); }
        }

        public static void CreateDatabase() {
            using (var conn = Connection) {
                conn.Open();

                string tables = @"
                    CREATE TABLE Preferences (
                        prefName TEXT NOT NULL, 
                        prefValue TEXT NOT NULL);

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
                        ID INT PRIMARY KEY,
                        stuName TEXT NOT NULL,
                        lessDate DATETIME NOT NULL,
                        lessTopic TEXT NOT NULL,
                        lessHomework TEXT,
                        repTopLeft TEXT,
                        repTopRight TEXT,
                        repBottom TEXT);

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
                    INSERT INTO Preferences (prefName, prefValue)
                    VALUES  ('theme', 'Light'),
                            ('isTopLeftAutoCompleteOn', 'true'), 
                            ('isTopRightAutoCompleteOn', 'true'), 
                            ('isBottomAutoCompleteOn', 'false');

                    INSERT INTO PdfHeadings (headPosition, headName)
                    VALUES  (1, 'New Language'),
                            (2, 'Pronunciation'),
                            (3, 'Corrections'),
                            (4, '[check the options menu to set or disable this footer]');";

                string defaultSavePath = Directory.GetCurrentDirectory();
                string saveDir = @$"
                    INSERT INTO Preferences (prefName, prefValue) 
                    VALUES  ('saveDirectory', '{defaultSavePath}');";

                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = tables;
                command.ExecuteNonQuery();

                command.CommandText = populate;
                command.ExecuteNonQuery();

                command.CommandText = saveDir;
                command.ExecuteNonQuery();
            }
        }

        #region FIND
        public static string FindStuName(int id) {
            using (var conn = Connection) {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT stuName 
                    FROM Students 
                    WHERE stuID = @id";
                SQLiteParameter n = new SQLiteParameter("@id", id);
                cmd.Parameters.Add(n);

                SQLiteDataReader dbRead = cmd.ExecuteReader();

                if (dbRead != null && dbRead.HasRows) {
                    while (dbRead.Read()) {
                        return (string)dbRead["stuName"];
                    }
                }

                return null;
            }
        }

        public static int FindStuID(string studentName) {
            using (var conn = Connection) {
                //TRY FIND EXISTING ENTRY
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT stuID 
                    FROM Students 
                    WHERE stuName = @name";
                SQLiteParameter n = new SQLiteParameter("@name", studentName);
                cmd.Parameters.Add(n);

                SQLiteDataReader dbRead = cmd.ExecuteReader();

                if (dbRead != null && dbRead.HasRows) {
                    while (dbRead.Read()) {
                        return (int)dbRead["stuID"];
                    }
                }

                return -1;
            }
        }

        public static int FindReportID(string studentName, DateTime lessDate) {
            using (var conn = Connection) {
                //TRY FIND EXISTING ENTRY
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT ID 
                    FROM Reports 
                    WHERE stuName = @name 
                    AND lessDate = @date";
                SQLiteParameter n = new SQLiteParameter("@name", studentName);
                SQLiteParameter d = new SQLiteParameter("@date", lessDate);
                cmd.Parameters.Add(n);
                cmd.Parameters.Add(d);

                SQLiteDataReader dbRead = cmd.ExecuteReader();

                if (dbRead != null && dbRead.HasRows) {
                    while (dbRead.Read()) {
                        return (int)dbRead["ID"];
                    }
                }

                //IF NO MATCH, GENERATE A NEW ID
                cmd.CommandText = $@"
                    SELECT ID 
                    FROM Reports 
                    ORDER BY ID DESC 
                    LIMIT 1;";

                while (dbRead.Read()) {
                    return (int)dbRead["ID"] + 1;
                }

                return -1;
            }
        }

        public static string FindReportSaveLocation() {
            using (var conn = Connection) {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT * 
                    FROM Preferences 
                    WHERE settingName = 'saveDirectory';";

                var reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    return (string)reader["settingValue"];
                }
            }
            return null;
        }
        #endregion

        #region NEW
        public static int NewStuID() {
            using (var conn = Connection) {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT stuID 
                    FROM Students 
                    ORDER BY stuID DESC 
                    LIMIT 1;";
                SQLiteDataReader dbRead = cmd.ExecuteReader();

                while (dbRead.Read()) {
                    return (int)dbRead["stuID"] + 1;
                }

                return -1;
            }
        }

        public static int NewLessID() {
            using (var conn = Connection) {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT lessID 
                    FROM Lessons 
                    ORDER BY lessID DESC 
                    LIMIT 1;";
                SQLiteDataReader dbRead = cmd.ExecuteReader();

                while (dbRead.Read()) {
                    return (int)dbRead["stuID"] + 1;
                }

                return -1;
            }
        }
        #endregion

        #region INSERT
        public static bool InsertStudent(Student student) {
            using (var conn = Connection) {
                conn.Open();
                string studentInfo = @$"
                    INSERT INTO Students (stuID, stuName, isCurrent) 
                    VALUES (@1, @2, @3)";
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = studentInfo;

                SQLiteParameter[] parameters = new SQLiteParameter[] {
                    new SQLiteParameter("@1", student.Id),
                    new SQLiteParameter("@2", student.Name),
                    new SQLiteParameter("@3", student.Active)
                };
                cmd.Parameters.AddRange(parameters);

                if (cmd.ExecuteNonQuery() < 1) {
                    throw new SQLiteException("Failed to add student to database.");
                }
                return true;
            }
        }

        public static bool InsertReport(Report report) {
            using (var conn = Connection) {
                conn.Open();
                string studentInfo = @$"
                    INSERT INTO Reports (
                        stuID, 
                        stuName, 
                        lessDate, 
                        lessTopic, 
                        lessHomework, 
                        newLanguage, 
                        pronunciation, 
                        corrections) 
                    VALUES (@1, @2, @3, @4, @5, @6, @7, @8)";
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = studentInfo;

                SQLiteParameter[] parameters = new SQLiteParameter[] {
                    new SQLiteParameter("@1", report.ReportID),
                    new SQLiteParameter("@2", report.Name),
                    new SQLiteParameter("@3", report.Date),
                    new SQLiteParameter("@4", report.Topic),
                    new SQLiteParameter("@5", report.Homework),
                    new SQLiteParameter("@6", Report.DictToString(report.NewLanguage)),
                    new SQLiteParameter("@7", Report.DictToString(report.Pronunciation)),
                    new SQLiteParameter("@8", Report.DictToString(report.Corrections))
                };
                cmd.Parameters.AddRange(parameters);

                if (cmd.ExecuteNonQuery() < 1) {
                    return false;
                }

                return true;
            }
        }

        public static bool InsertLesson(Lesson lesson) {
            using (var conn = Connection) {
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
                    new SQLiteParameter("@4", lesson.Date),
                    new SQLiteParameter("@4", lesson.Topic),
                    new SQLiteParameter("@5", lesson.Homework)
                };
                cmd.Parameters.AddRange(parameters);

                if (cmd.ExecuteNonQuery() < 1) {
                    return false;
                }

                return true;
            }
        }
        #endregion
    }
}
