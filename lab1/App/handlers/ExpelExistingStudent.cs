using Student.Entities;
using Student.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _expel_existing_student_handler()
        {
            Student.Entities.StudentID student_id = Convert.ToUInt64(_get_prompt("Please input StudentID"));
            StudentLifeCycle student_lifecycle_service = new(this._student_repository);
            try
            {
                await student_lifecycle_service.Expel(student_id);
                _print_payload_text($"A student with id {student_id} was expelled", true);
                
            }
            catch (StudentDoesNotExistsError err) {
                _print_error(err.Message);
            }
        }
    }
}
