using Course.Entities;
using Course.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        async void _set_a_mark_handler() {
            try
            {
                Student.Entities.StudentID student_id = new Student.Entities.StudentID(Convert.ToUInt64(_get_prompt("Please put a student id")));
                Course.Entities.CourseID course_id = new Course.Entities.CourseID(Convert.ToUInt64(_get_prompt("Please put a course id")));


                GradeTypes _grade = (Course.Entities.GradeTypes)(Convert.ToUInt64(_get_prompt("Please put a mark (from 1 to 5)")));
                Course.Entities.CourseTranscript transcript = new(student_id, course_id, _grade);

                CourseTranscriptService course_transcript_service = new(this._course_transcript_repository, this._student_repository, this._course_repository);
                CourseTranscriptID transcript_id = await course_transcript_service.AppendGrade(student_id, course_id, _grade);
                _print_text($"A transcript is added to the repository. Transcript ID is {transcript_id}");
            }
            catch (Exception err) {
                _print_error(err.Message);
            }
        }
    }
}
