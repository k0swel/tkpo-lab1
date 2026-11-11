using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Services
{
    public class StudentService
    {
        private Student.Interfaces.StudentRepositoryInterface _repository;
        public StudentService(Student.Interfaces.StudentRepositoryInterface repository) {
            _repository = repository;
        }
        public async Task<Entities.StudentID> CreateStudent(Entities.Student student_object) {
            Entities.StudentID new_studentid = await _repository.InsertAsync(student_object);
            return new_studentid;
        }
        public async Task<Entities.Student> GetStudent(StudentID id) {
            Entities.Student? student_object = await _repository.FindByIDAsync(id);
            if (student_object is null) {
                throw new Entities.StudentDoesNotExistsError($"A student with specified ID({id}) does not exists in the repo");
            }
            return student_object;
        }
    }
}
