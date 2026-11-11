using System;
using System.Collections.Generic;
using System.Text;

using Teacher;
namespace Teacher.Entities.Errors
{
    public class TeacherNameIsNotCorrect : Exception
    {
        public TeacherNameIsNotCorrect(string text) : base(text) { }
    }
    public class TeacherStatusIsNotCorrect : Exception
    {
        public TeacherStatusIsNotCorrect(string text) : base(text) { }
    }
    public class TeacherDoesNotExists : Exception {
        public TeacherDoesNotExists(string text) : base(text) { }
    }
}
