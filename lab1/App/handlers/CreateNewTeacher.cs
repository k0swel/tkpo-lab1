using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _create_a_teacher_handler()
        {
            Teacher.Entities.TeacherName teacher_name = _get_prompt("Please put a teacher name");
            Teacher.Entities.Teacher teacher_object = new(teacher_name, Teacher.Entities.TeacherStatus.WORKING);


            Teacher.Services.TeacherService teacher_service = new(this._teacher_repository);
            
            try
            {
                Teacher.Entities.TeacherID teacher_id = await teacher_service.CreateTeacher(teacher_object);
                _print_payload_text($"A teacher was created . Teacher ID is {teacher_id}");
            }
            catch (GroupAlreadyExistsError err)
            {
                _print_error(err.Message);
            }
        }
    }
}
