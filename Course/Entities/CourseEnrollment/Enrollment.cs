using BaseRepositories;
using Group.Entities;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Teacher.Entities;


namespace Course.Entities
{
    public record Enrollment : BaseRepositories.Interfaces.Entity<EnrollmentID> {
        public EnrollmentID Id { get; set; }
        public StudentID? Student = null;
        public GroupID? Group = null;
        public CourseID Course;
        public TeacherID? Teacher = null;

        public Enrollment(CourseID course, GroupID group_id) {
            this.Id = 0;
            this.Group = group_id;
            this.Course = course;
        }
        public Enrollment(CourseID course, StudentID student_id)
        {
            this.Id = 0;
            this.Student = student_id;
            this.Course = course;
        }
        public Enrollment(CourseID course, TeacherID teacher_id)
        {
            this.Id = 0;
            this.Teacher = teacher_id;
            this.Course = course;
        }


    }

    public record EnrollmentID : BaseRepositories.Entities.ID, BaseRepositories.IDInterface<EnrollmentID> {
        public new object Value => _value;
        public EnrollmentID(ulong Value)
        {
            this._value = Value;
            
        }
        public EnrollmentID()
        {
            this._value = 0;
        }
        public static EnrollmentID operator +(EnrollmentID left, EnrollmentID right)
        {
            return new EnrollmentID(left._value + right._value);
        }
        public static EnrollmentID operator ++(EnrollmentID Value)
        {
            return new EnrollmentID(Value._value + 1);
        }
        public static implicit operator EnrollmentID(ulong value) {
            return new EnrollmentID(value);
        }
        public object GetID() {
            return _value;
        }
        public override string ToString()
        {
            return $"{this._value}";
        }
    }
}

public sealed record CourseID : BaseRepositories.Entities.ID
{
    public new object Value => _value;
    public CourseID(ulong Value)
    {
        this._value = Value;
    }
    public CourseID()
    {
        this._value = 0;
    }
    public object GetID()
    {
        return _value;
    }
    public static CourseID operator +(CourseID left, CourseID right)
    {
        return new CourseID(left._value + right._value);
    }
    public static CourseID operator ++(CourseID Value)
    {
        return new CourseID(Value._value + 1);
    }
    public static implicit operator CourseID(ulong value) {
        return new CourseID(value);
    }
}
