using System;
using System.Collections.Generic;
using System.Text;
using Teacher.Interfaces;
using Teacher.Entities.Errors;

namespace Teacher.Services
{
    public class TeacherService
    {
        private ITeacherRepository _repository;
        public TeacherService(ITeacherRepository repository)
        {
            _repository = repository;
        }
        public async Task<Entities.TeacherID> CreateTeacher(Entities.Teacher teacher) {
            Entities.TeacherID teacher_id = await _repository.InsertAsync(teacher);
            return teacher_id;
        }

        public async Task<Entities.Teacher> GetTeacher(Entities.TeacherID teacher_id) {
            return await _repository.FindByIDAsync(teacher_id) ?? throw new Entities.Errors.TeacherDoesNotExists($"A teacher with the Id == {teacher_id} does not exists");
        }
        public async Task ChangeName(Entities.TeacherID teacher_id, Entities.TeacherName teacher_name) {
            Entities.Teacher teacher_object = await GetTeacher(teacher_id);
            teacher_object.Rename(teacher_name);
            return ;
        }
        public async Task GoOnVacation(Entities.TeacherID teacher_id) {
            Entities.Teacher teacher_object = await GetTeacher(teacher_id);
            teacher_object.GoOnVacation();
            await _repository.UpdateAsync(teacher_object);
            return;
        }
        public async Task LeaveFromVacation(Entities.TeacherID teacher_id)
        {
            Entities.Teacher teacher_object = await GetTeacher(teacher_id);
            teacher_object.LeaveFromVacation();
            await _repository.UpdateAsync(teacher_object);
            return;
        }
        public async Task Fire(Entities.TeacherID teacher_id)
        {
            Entities.Teacher teacher_object = await GetTeacher(teacher_id);
            teacher_object.Fire();
            await _repository.UpdateAsync(teacher_object);
            return;
        }
    }
}
