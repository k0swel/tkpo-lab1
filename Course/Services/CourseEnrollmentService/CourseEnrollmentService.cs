using Course.Entities;
using Group.Entities;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Teacher.Entities;

using StudentRepositoryInterface = Student.Interfaces.StudentRepositoryInterface;
using ITeacherRepository = Teacher.Interfaces.ITeacherRepository;
using GroupRepositoryInterface = Group.Interfaces.GroupRepositoryInterface;
using Course.Interfaces;



namespace Course.Services
{
    public class CourseEnrollmentService
    {
        private readonly Interfaces.CourseEnrollmentRepository _enrollment_repository;
        private readonly StudentRepositoryInterface _student_repository;
        private readonly ITeacherRepository _teacher_repository;
        private readonly GroupRepositoryInterface _group_repository;
        private readonly ICourseRepository _course_repository;
        public CourseEnrollmentService(Interfaces.CourseEnrollmentRepository enrollment_repository, StudentRepositoryInterface student_repository, ITeacherRepository teacher_repository, GroupRepositoryInterface group_repository, ICourseRepository course_repository) {
            _enrollment_repository = enrollment_repository;
            _student_repository = student_repository;
            _teacher_repository = teacher_repository;
            _group_repository = group_repository;
            _course_repository = course_repository;
        }
        public async Task<Entities.EnrollmentID> EnrollStudent(Entities.CourseID course_id, StudentID student_id) {
            // 1. Убеждаемся, что студент существует
            Student.Entities.Student? student = await this._student_repository.FindByIDAsync(student_id);
            if (student is null) {
                throw new Student.Entities.StudentDoesNotExistsError($"A student with id {student_id} does not exists");
            }
            // 2. Убеждаемся, что курс существует
            bool is_course_exists = await this._course_repository.IsExistsAsync(course_id);
            if (!is_course_exists) {
                throw new CourseDoesNotExistsError($"A course with id {course_id} does not exists");
            }
            // 3. Убеждаемся, что студент учится (не в академе и  не отчислен)
            if (student.Status == StudentStatus.ACTIVE)
            {
                Enrollment enrollment_object = new Enrollment(course_id, student_id);
                return await _enrollment_repository.InsertAsync(enrollment_object);
            }
            throw new Student.Entities.IncorrectStudentState($"Student is expelled. Enroll to course is not possible");
        }
        public async Task UnenrollStudent(Entities.CourseID course_id, StudentID student_id)
        {
            // 1. Убеждаемся, что студент существует
            bool student_is_exists = await this._student_repository.IsExistsAsync(student_id);
            if (!student_is_exists)
            {
                throw new Student.Entities.StudentDoesNotExistsError($"A student with id {student_id} does not exists");
            }
            // 2. Убеждаемся, что курс существует
            bool is_course_exists = await this._course_repository.IsExistsAsync(course_id);
            if (!is_course_exists)
            {
                throw new CourseDoesNotExistsError($"A course with id {course_id} does not exists");
            }
            // 3. Ищем курс у студента и удаляем, если находим
            List<Enrollment> enrollment_objects = _enrollment_repository.FindByStudentID(student_id);
            foreach (var enroll in enrollment_objects) {
                if (enroll.Course == course_id) {
                    var enrollment_id = enroll.Id;
                    await _enrollment_repository.DeleteAsync(enrollment_id);
                }
            }
            throw new Entities.EnrollmentDoesNotExistsError($"An enrollment with student_id {student_id} and course_id {course_id} does not exists");

        }
        public async Task<Entities.EnrollmentID> EnrollTeacher(Entities.CourseID course_id, TeacherID teacher_id)
        {
            // 1. Учитель вообще существует
            Teacher.Entities.Teacher? teacher = await this._teacher_repository.FindByIDAsync(teacher_id);
            if (teacher is null)
            {
                throw new Teacher.Entities.Errors.TeacherDoesNotExists($"A teacher with id {teacher_id} does not exists");
            }
            // 2. Курс сущестует
            bool is_course_exists = await this._course_repository.IsExistsAsync(course_id);
            if (!is_course_exists)
            {
                throw new CourseDoesNotExistsError($"A course with id {course_id} does not exists");
            }
            // 3. Убеждаемся, что учитель не уволен.
            if (teacher.Status != TeacherStatus.FIRED)
            {
                Enrollment enrollment_object = new Enrollment(course_id, teacher_id);
                return await _enrollment_repository.InsertAsync(enrollment_object);
            }
            throw new Teacher.Entities.Errors.TeacherStatusIsNotCorrect($"A teacher with id ({teacher_id}) is fired. Enrolling to course is not possible");
        }
        public async Task UnenrollTeacher(Entities.CourseID course_id, TeacherID teacher_id)
        {
            bool teacher_is_exists = await this._teacher_repository.IsExistsAsync(teacher_id);
            if (!teacher_is_exists)
            {
                throw new Teacher.Entities.Errors.TeacherDoesNotExists($"A teacher with id {_teacher_repository} does not exists");
            }
            bool is_course_exists = await this._course_repository.IsExistsAsync(course_id);
            if (!is_course_exists)
            {
                throw new CourseDoesNotExistsError($"A course with id {course_id} does not exists");
            }
            List<Enrollment> enrollment_objects = _enrollment_repository.FindByTeacherID(teacher_id);
            foreach (var enroll in enrollment_objects)
            {
                if (enroll.Course == course_id)
                {
                    var enrollment_id = enroll.Id;
                    await _enrollment_repository.DeleteAsync(enrollment_id);
                }
            }
            throw new Entities.EnrollmentDoesNotExistsError($"An enrollment with teacher_id {teacher_id} and course_id {course_id} does not exists");

        }
        public async Task<Entities.EnrollmentID> EnrollGroup(Entities.CourseID course_id, GroupID group_id)
        {
            // 1. Убеждаемся, что группа существует
            Group.Entities.Group? group = await this._group_repository.FindByIDAsync(group_id);
            if (group is not null)
            {
                throw new Group.Entities.GroupDoesNotExistsError($"A group with id {group_id} does not exists");
            }
            // 2. Убеждаемся, что курс существует
            bool is_course_exists = await this._course_repository.IsExistsAsync(course_id);
            if (!is_course_exists)
            {
                throw new CourseDoesNotExistsError($"A course with id {course_id} does not exists");
            }
            // 3. Зачисляем группа на курс.
            Enrollment enrollment_object = new Enrollment(course_id, group_id);
            return await _enrollment_repository.InsertAsync(enrollment_object);
        }
        public async Task UnenrollGroup(Entities.CourseID course_id, GroupID group_id)
        {
            bool group_is_exists = await this._group_repository.IsExistsAsync(group_id);
            if (!group_is_exists)
            {
                throw new Group.Entities.GroupDoesNotExistsError($"A group with id {group_id} does not exists");
            }
            bool is_course_exists = await this._course_repository.IsExistsAsync(course_id);
            if (!is_course_exists)
            {
                throw new CourseDoesNotExistsError($"A course with id {course_id} does not exists");
            }
            List<Enrollment> enrollment_objects = _enrollment_repository.FindByGroupID(group_id);
            foreach (var enroll in enrollment_objects)
            {
                if (enroll.Course == course_id)
                {
                    var enrollment_id = enroll.Id;
                    await _enrollment_repository.DeleteAsync(enrollment_id);
                    return;
                }
            }
            throw new Entities.EnrollmentDoesNotExistsError($"An enrollment with group_id {group_id} and course_id {course_id} does not exists");
        }
        public async Task<Entities.Enrollment> GetEnrollment(EnrollmentID enrollment_id) {
            Entities.Enrollment? enrollment_object = await _enrollment_repository.FindByIDAsync(enrollment_id);
            if (enrollment_object != null) {
                return enrollment_object;
            }
            throw new EnrollmentDoesNotExistsError($"Enrollment with id({enrollment_id}) does not exists");
        }
    }
}
