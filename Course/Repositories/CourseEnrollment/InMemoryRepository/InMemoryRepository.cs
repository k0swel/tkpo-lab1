using Course.Entities;
using Group.Entities;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Teacher.Entities;


namespace Course.Repositories
{
    public class InMemoryCourseEnrollmentRepository: BaseRepositories.InMemoryRepository<Enrollment, EnrollmentID>, BaseRepositories.Interfaces.IRepository<Enrollment, EnrollmentID>, Interfaces.CourseEnrollmentRepository
    {
        private readonly Dictionary<GroupID, HashSet<EnrollmentID>> _byGroup;
        private readonly Dictionary<StudentID, HashSet<EnrollmentID>> _byStudent;
        private readonly Dictionary<TeacherID, HashSet<EnrollmentID>> _byTeacher;
        public InMemoryCourseEnrollmentRepository() {
            this._byGroup = new();
            this._byStudent = new();
            this._byTeacher = new();
        }

        public override async Task<EnrollmentID> InsertAsync(Enrollment enrollment_object) {
            // useless
            //if (await IsExistsAsync(enrollment_object.Id)) {
            //    throw new EnrollmentAlreadyExists($"Enrollment with specified ID ({enrollment_object.Id}) already exists");
            //}
            lock (_mutex)
            {
                if (enrollment_object.Student is { } sid && _byStudent.TryGetValue(sid, out var s) && s.Contains(enrollment_object.Id))
                    throw new EntityAlreadyHasACourseError($"Student ({sid}) already has a course {enrollment_object.Course}");
                if (enrollment_object.Teacher is { } tid && _byTeacher.TryGetValue(tid, out var t) && t.Contains(enrollment_object.Id))
                    throw new EntityAlreadyHasACourseError($"Teacher ({tid}) already has a course {enrollment_object.Course}");
                if (enrollment_object.Group is { } gid && _byGroup.TryGetValue(gid, out var g) && g.Contains(enrollment_object.Id))
                    throw new EntityAlreadyHasACourseError($"Group ({gid}) already has a course {enrollment_object.Course}");

                EnrollmentID newId = _NextID();
                enrollment_object.Id = newId;
                _database.Add(newId, enrollment_object);
                if (enrollment_object.Student is not null)
                {
                    _insertStudent(enrollment_object.Student, enrollment_object.Id);
                }
                if (enrollment_object.Teacher is not null)
                {
                    _insertTeacher(enrollment_object.Teacher, enrollment_object.Id);
                }
                if (enrollment_object.Group is not null)
                {
                    _insertGroup(enrollment_object.Group, enrollment_object.Id);
                }
                return newId;
            }
        }
        // Calling this function must be wrapped by mutex
        private void _insertStudent(StudentID student_id, EnrollmentID id) {
                if (!_byStudent.TryGetValue(student_id, out var value))
                {
                    _byStudent[student_id] = new HashSet<EnrollmentID>();
                }

                if (!_byStudent[student_id].Contains(id))
                    value?.Add(id);
                else
                {
                    throw new EntityAlreadyHasACourseError($"Student ({student_id}) already has a course with ID {_database[id].Course}");
                }
        }
        // Calling this function must be wrapped by mutex
        private void _insertTeacher(TeacherID teacher_id, EnrollmentID id)
        {
            if (!_byTeacher.TryGetValue(teacher_id, out var value))
            {
                _byTeacher[teacher_id] = value = new HashSet<EnrollmentID>();
            }
            if (!_byTeacher[teacher_id].Contains(id))
                value?.Add(id);
            else
            {
                throw new EntityAlreadyHasACourseError($"Teacher ({teacher_id}) already has a course with ID{_database[id].Course}");
            }
        }
        // Calling this function must be wrapped by mutex
        private void _insertGroup(GroupID group_id, EnrollmentID id)
        {
            if (!_byGroup.TryGetValue(group_id, out var value))
            {
                _byGroup[group_id] = value = new HashSet<EnrollmentID>();
            }
            if (!_byGroup[group_id].Contains(id))
                value?.Add(id);
            else {
                throw new EntityAlreadyHasACourseError($"Group ({group_id}) already has a course {_database[id].Course}");
            }
        }
        public List<Enrollment> FindByStudentID(StudentID student_id) {
            lock (_mutex)
            {
                if (!this._byStudent.ContainsKey(student_id)) throw new Student.Entities.StudentDoesNotExistsError($"A student with ID {student_id} does not exists");
                List<Enrollment> list = new List<Enrollment>();
                foreach (var id in this._byStudent[student_id].ToHashSet())
                {
                    list.Add(_database[id]);   
                }
                return list;
            }
        }
        public List<Enrollment> FindByTeacherID(TeacherID teacher_id)
        {
            lock (_mutex)
            {
                if (!this._byTeacher.ContainsKey(teacher_id)) throw new Teacher.Entities.Errors.TeacherDoesNotExists($"A student with ID {teacher_id} does not exists"); ;
                List<Enrollment> list = new List<Enrollment>();
                foreach (var id in this._byTeacher[teacher_id].ToHashSet())
                {
                    list.Add(_database[id]);
                }
                _byTeacher[teacher_id].ToList();
                return list;
            }
        }
        public List<Enrollment> FindByGroupID(GroupID group_id)
        {
            lock (_mutex)
            {
                if (!this._byGroup.ContainsKey(group_id)) throw new Group.Entities.GroupDoesNotExistsError($"A group with ID {group_id} does not exists"); ;
                List<Enrollment> list = new List<Enrollment>();
                foreach (var id in this._byGroup[group_id].ToHashSet())
                {
                    list.Add(_database[id]);
                }
                return list;
            }
        }
        public override Task<EnrollmentID> UpdateAsync(Enrollment enrollment_object) {
            throw new NotSupportedException($"A called function {MethodBase.GetCurrentMethod()?.Name} not supported for Enrollment. Use Delete and Create Other Enrollment");
        }
        public async override Task DeleteAsync(EnrollmentID enrollment_id)
        {
            var result = await this.FindByIDAsync(enrollment_id);
            if (result is null)
            {
                throw new Entities.EnrollmentDoesNotExistsError($"An Enrollment with id {enrollment_id} does not exists.");
            }
            // Удаляем Enrollment записи из структур
            if (result.Teacher is not null)
            {
                TeacherID teacher_id = result.Teacher;
                _mutex.Enter();
                _byTeacher[teacher_id].Remove(enrollment_id);
                _mutex.Exit();
            }
            if (result.Student is not null)
            {
                StudentID student_id = result.Student;
                _mutex.Enter();
                _byStudent[student_id].Remove(enrollment_id);
                _mutex.Exit();
            }
            if (result.Group is not null)
            {
                GroupID group_id = result.Group;
                _mutex.Enter();
                _byGroup[group_id].Remove(enrollment_id);
                _mutex.Exit();
            }
            _mutex.Enter();
            _database.Remove(enrollment_id);
            _mutex.Exit();
        }
    }
}
