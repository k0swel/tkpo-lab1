using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Services
{
    public class StudentLifeCycle
    {
        private Student.Interfaces.StudentRepositoryInterface _repository;
        public StudentLifeCycle(Student.Interfaces.StudentRepositoryInterface repository)
        {
            _repository = repository;
        }

        public async Task Expel(StudentID id)
        {
            Entities.Student? student_object = await _repository.FindByIDAsync(id);
            if (student_object is null)
            {
                throw new Entities.StudentDoesNotExistsError($"A student with specified ID({id}) does not exists in the repo");
            }
            student_object.Expel();
            await _repository.UpdateAsync(student_object);
            return;
        }
        public async Task Restore(StudentID id)
        {
            Entities.Student? student_object = await _repository.FindByIDAsync(id);
            if (student_object is null)
            {
                throw new Entities.StudentDoesNotExistsError($"A student with specified ID({id}) does not exists in the repo");
            }
            student_object.Restore();
            await _repository.UpdateAsync(student_object);
        }
        public async Task GoOnVacation(StudentID id)
        {
            Entities.Student? student_object = await _repository.FindByIDAsync(id);
            if (student_object is null)
            {
                throw new Entities.StudentDoesNotExistsError($"A student with specified ID({id}) does not exists in the repo");
            }
            student_object.GoOnVacation();
            await _repository.UpdateAsync(student_object);
            return;
        }
        public async Task LeaveFromVacation(StudentID id)
        {
            Entities.Student? student_object = await _repository.FindByIDAsync(id);
            if (student_object is null)
            {
                throw new Entities.StudentDoesNotExistsError($"A student with specified ID({id}) does not exists in the repo");
            }
            student_object.LeaveFromVacation();
            await _repository.UpdateAsync(student_object);
            return;
        }
    }
}
