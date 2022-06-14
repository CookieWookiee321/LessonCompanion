namespace Cambly_Reports.Backend
{
    internal class Student
    {
        public int Id { get => Id; set => Id = value; }
        public string Name { get => Name; set => Name = value; }
        public bool Active { get => Active; set => Active = value; }

        public Student(int id, string name, bool active) {
            this.Id = id;
            this.Name = name;
            this.Active = active;
        }
    }

    class StudentNew : Student
    {
        public StudentNew(int id, string name) : base(id, name, true) { }
    }
}
