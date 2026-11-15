using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _assign_a_teacher_to_course_handler() {
            Teacher.Entities.TeacherID teacher_id = Convert.ToUInt32(_get_prompt("Please put a teacher ID"));
            Course.Entities.CourseID course_id = Convert.ToUInt32(_get_prompt("Please put a course ID"));

            Course.Services.CourseEnrollmentService course_enrollment_service = new(this._course_enrollment_repository, this._student_repository, this._teacher_repository, this._group_repository, this._course_repository);
            try
            {
                await course_enrollment_service.EnrollTeacher(course_id, teacher_id);
                _print_payload_text($"A teacher with ID {teacher_id} was enrolled to course");
            }
            catch (Exception err) {
                _print_error(err.Message);
            }
        }
    }
}
