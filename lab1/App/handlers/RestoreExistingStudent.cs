using Student.Entities;
using Student.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _restore_existing_student_handler() {
            Student.Entities.StudentID student_id = Convert.ToUInt64(_get_prompt("Please input StudentID"));
            StudentService student_service = new(this._student_repository);
            StudentLifeCycle student_lifecycle_service = new(this._student_repository);
            try
            {
                Student.Entities.Student student_object = await student_service.GetStudent(student_id);
                if (student_object.Status != StudentStatus.ACTIVE)
                {
                    await student_lifecycle_service.Restore(student_id);
                    _print_payload_text($"A student with id ({student_id}) is restored");
                }
                else {
                    _print_error($"A student with id ({student_id}) already restored(or active)");
                }
            }
            catch (StudentDoesNotExistsError err)
            {
                _print_error(err.Message);
            }
        }
    }
}
