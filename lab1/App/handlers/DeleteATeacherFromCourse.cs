using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        async void _delete_a_teacher_from_course_handler() {
           
            Teacher.Entities.TeacherID teacher_id = Convert.ToUInt64(_get_prompt("Please put a teacher_id"));
            Course.Entities.CourseID course_id = Convert.ToUInt64(_get_prompt("Please put a course_id"));

            Course.Services.CourseEnrollmentService course_enrollment_service = new(this._course_enrollment_repository, this._student_repository, this._teacher_repository, this._group_repository, this._course_repository);
            try
            {
                await course_enrollment_service.UnenrollTeacher(course_id, teacher_id);
                _print_payload_text($"A teacher with id {teacher_id} was kicked from course with id {course_id}");
            }
            catch (Exception err) {
                _print_error($"{err.Message}");
            }

        }
    }
}
