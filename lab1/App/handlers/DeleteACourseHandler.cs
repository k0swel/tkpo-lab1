using Course.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        async void _delete_a_course_handler() {
            Course.Entities.CourseID course_id = Convert.ToUInt64(_get_prompt("Please enter a course ID"));
            Course.Services.CourseEnrollmentService course_enrollment_service = new(this._course_enrollment_repository, this._student_repository, this._teacher_repository, this._group_repository, this._course_repository);
            Course.Services.CourseService course_service = new Course.Services.CourseService(this._course_repository, course_enrollment_service);
            try
            {
                await course_service.DeleteCourse(course_id);
                _print_payload_text($"A course with id {course_id} is deleted");
            }
            catch (CourseDoesNotExistsError err) {
                _print_error($"{err.Message}");
            }
        }
    }
}
