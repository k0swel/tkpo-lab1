using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _add_a_course_handler() {
            Course.Entities.CourseName course_name = _get_prompt("Please enter a course name");
            Course.Entities.CourseDescription course_description = _get_prompt("Please enter a course description");
            Course.Entities.Course new_course = new Course.Entities.Course(course_name, course_description, Course.Entities.CourseStatus.ENABLED);


            Course.Services.CourseService course_service = new Course.Services.CourseService(this._course_repository);
            Course.Entities.CourseID course_id = await course_service.CreateCourse(new_course);
            _print_payload_text($"A course with id {course_id} was created");
        }
    }
}
