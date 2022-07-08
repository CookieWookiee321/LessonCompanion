using System;

namespace LessonCompanion.Logic.Models {
    public class Lesson {
        public Lesson(int lessonID, int studentID, DateTime date, string topic, string homework) {
            LessonID = lessonID;
            StudentID = studentID;
            Date = date;
            Topic = topic;
            Homework = homework;
        }

        public int LessonID { get; set; }
        public int StudentID { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public string Homework { get; set; }


    }
}
