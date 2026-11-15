using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _delete_a_group_from_course_handler() {
            Group.Entities.GroupID group_id = _get_prompt("Please put a group ID");
            Course.Entities.CourseID course_id = Convert.ToUInt64(_get_prompt("Please put a course ID"));

            Course.Services.CourseEnrollmentService course_enrollment_service = new(this._course_enrollment_repository, this._student_repository, this._teacher_repository, this._group_repository, this._course_repository);
            try
            {
                await course_enrollment_service.UnenrollGroup(course_id, group_id);
                _print_payload_text($"A group with ID {group_id} was deleted from course with ID {course_id}");
            }
            catch (Exception err)
            {
                _print_error(err.Message);
            }
        }
    }
}
