using Group.Entities;
using Group.Services;
using Student.Entities;
using Student.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _createNewStudentHandler() {
            try
            {
                Student.Entities.StudentName Name = Student.Entities.StudentName.TryParse(_get_prompt("Please input LastName, FirstName and SurName (optional) using whitespace as separator"));
                StudentService student_service = new(this._student_repository);
                StudentID student_id = await student_service.CreateStudent(new Student.Entities.Student(Name, StudentStatus.ACTIVE));
                _print_payload_text($"A student with id {student_id.ToString()} was created", true);
            }
            catch (Exception err) {
                _print_error($"{err.Message}");
            }
        }
    }
}
