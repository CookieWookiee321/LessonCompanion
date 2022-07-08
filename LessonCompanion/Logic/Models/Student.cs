namespace LessonCompanion.Logic.Models {
    public class Student {
        private int id;
        private string name;
        private bool active;

        public Student(int id, string name, bool active) {
            Id = id;
            Name = name;
            Active = active;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool Active { get => active; set => active = value; }
        public int ActiveInt {
            get => Active ? 1 : 0;
        }
    }

    public class StudentNew : Student {
        public StudentNew(int id, string name) : base(id, name, true) { }
    }
}
