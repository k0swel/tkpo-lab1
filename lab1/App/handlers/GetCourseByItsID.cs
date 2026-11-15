using Course.Entities;
using System;

namespace lab1
{
    internal partial class Application
    {
        private async void _get_a_course_by_id_handler()
        {
           
                var course_service = new Course.Services.CourseService(this._course_repository);
                var course_enrollment = new Course.Services.CourseEnrollmentService(
                    this._course_enrollment_repository,
                    this._student_repository,
                    this._teacher_repository,
                    this._group_repository,
                    this._course_repository);
                Teacher.Services.TeacherService teacher_service = new(this._teacher_repository);
                Student.Services.StudentService student_service = new(this._student_repository);
                Group.Services.GroupService group_service = new(this._group_repository);


                Course.Entities.CourseID course_id = Convert.ToUInt64(_get_prompt("Please put a course id"));
            Course.Entities.Course course_object;
            try {
                course_object = await course_service.GetCourse(course_id);
                _print_payload_text($"{course_object}");
                var enrollments = await course_enrollment.FindByCourseID(course_id);

                foreach (var enrollment in enrollments)
                {
                    if (enrollment.Teacher is { } t)
                    {
                        Teacher.Entities.TeacherID teacher_id = new Teacher.Entities.TeacherID((ulong)t.Value);
                        Teacher.Entities.Teacher teacher_object = await teacher_service.GetTeacher(teacher_id);
                        _print_payload_text($"{teacher_object.ToString()}");
                    }
                    else if (enrollment.Student is { } s)
                    {
                        Student.Entities.StudentID student_id = new Student.Entities.StudentID((ulong)s.Value);
                        Student.Entities.Student student_object = await student_service.GetStudent(student_id);
                        _print_payload_text($"{student_object.ToString()}");
                    }
                    else if (enrollment.Group is { } g)
                    {
                        Group.Entities.GroupID group_id = new Group.Entities.GroupID((string)g.Value);
                        Group.Entities.Group group_object = await group_service.GetGroup(group_id);
                        _print_payload_text($"{group_object.ToString()}");
                    }
                }
            }
            catch (CourseDoesNotExistsError err)
            {
                _print_error($"{err.Message}");
            }
            catch (EnrollmentDoesNotExistsError) {
                _print_error($"Repository does not have any enrollments");
            }
            catch (Exception err)
            {
                _print_error($"Unexpected error: {err.Message}");
            }
        }
    }
}