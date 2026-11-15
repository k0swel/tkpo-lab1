using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Entities
{
    public class CourseAlreadyEnabledError : Exception
    {
        public CourseAlreadyEnabledError(string text) : base(text) { }
    }
    public class CourseAlreadyDisabledError : Exception
    {
        public CourseAlreadyDisabledError(string text) : base(text) { }
    };
    public class CourseDoesNotExistsError : Exception
    {
        public CourseDoesNotExistsError(string text) : base(text) { }
    };
    public class CourseIDIncorrectFormat(string text) : Exception(text) { }
    
}
