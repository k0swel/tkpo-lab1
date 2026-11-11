using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Entities
{
    public class EnrollmentDoesNotExistsError(string text) : Exception(text);
    public class EnrollmentAlreadyExists(string text) : Exception(text);
    public class EntityAlreadyHasACourseError(string text) : Exception(text);
}
