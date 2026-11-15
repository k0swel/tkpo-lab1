using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Entities
{
    public class Grade
    {
        public GradeTypes grade { get; private set; }
        public Grade(GradeTypes value)
        {
            this.grade = value;
        }
        public static implicit operator Grade(GradeTypes value) {
            return new Grade(value);
        }
        public void setGrade(GradeTypes value) {
            this.grade = value;
        }
        public override string ToString()
        {
            return $"{grade.ToString()}";
        }
    }

    public enum GradeTypes {
        ABSENSE = 1,
        BAD = 2,
        SATISFACTORILY = 3,
        GOOD = 4,
        EXCELLENT = 5
    }
}
