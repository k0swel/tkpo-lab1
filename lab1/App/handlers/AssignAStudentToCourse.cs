using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        async void _assign_a_student_to_course_handler() {
            Student.Entities.StudentID student_id = Convert.ToUInt64(_get_prompt("Please put a student id"));
            Course.Entities.CourseID course_id = Convert.ToUInt64( _get_prompt("Please put a course id"));

            Course.Services.CourseEnrollmentService course_enrollment_service = new(this._course_enrollment_repository, _student_repository, _teacher_repository, _group_repository, _course_repository);
            try
            {
                await course_enrollment_service.EnrollStudent(course_id, student_id);
                _print_payload_text($"A student with ID({student_id}) is enrolled to course with ID({course_id})");
            }
            catch (Exception err) {
                _print_error($"{err.Message}");
            }
        }
    }
}
