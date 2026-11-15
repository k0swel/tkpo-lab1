using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Entities
{
    public class IncorrectStudentState : Exception { 
        public IncorrectStudentState(string text): base(text) { }
    }
    public class StudentDoesNotExistsError : Exception
    {
        public StudentDoesNotExistsError(string text) : base(text) { }
    }
    public class StudentIDIncorrectError(string text) : Exception(text);
}
