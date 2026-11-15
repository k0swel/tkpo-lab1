using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Entities
{
    public class GradeIsNotCorrect(string text) : Exception(text);
    public class CourseTranscriptDoesNotExists(string text) : Exception(text);
    public class ImpossibleChangeCourseTranscriptStateError(string text) : Exception(text);
    public class CourseTranscriptImpossibleSetGrade(string text) : Exception(text);
}
