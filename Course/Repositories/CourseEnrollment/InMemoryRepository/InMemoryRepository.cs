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
        private readonly Dictionary<Course.Entities.CourseID, HashSet<EnrollmentID>> _byCourseID;
        public InMemoryCourseEnrollmentRepository() {
            this._byGroup = new();
            this._byStudent = new();
            this._byTeacher = new();
            this._byCourseID = new();
        }

        public override Task<EnrollmentID> InsertAsync(Enrollment enrollment)
        {
            lock (_mutex)
            {
                if (enrollment.Student is { } sid
                    && _byStudent.TryGetValue(sid, out var sSet)
                    && sSet.Any(id => _database[id].Course == enrollment.Course))
                    throw new EntityAlreadyHasACourseError(
                        $"Student ({sid}) already has course {enrollment.Course}");

                if (enrollment.Teacher is { } tid
                    && _byTeacher.TryGetValue(tid, out var tSet)
                    && tSet.Any(id => _database[id].Course == enrollment.Course))
                    throw new EntityAlreadyHasACourseError(
                        $"Teacher ({tid}) already has course {enrollment.Course}");

                if (enrollment.Group is { } gid
                    && _byGroup.TryGetValue(gid, out var gSet)
                    && gSet.Any(id => _database[id].Course == enrollment.Course))
                    throw new EntityAlreadyHasACourseError(
                        $"Group ({gid}) already has course {enrollment.Course}");

                if (!_byCourseID.ContainsKey(enrollment.Course)) _byCourseID.Add(enrollment.Course, new HashSet<EnrollmentID>());
                // 2. Генерация ID и запись
                var newId = _NextID();
                enrollment.Id = newId;
                _database.Add(newId, enrollment);

                // 3. Индексы — без исключений
                if (enrollment.Student is { } s) _insertStudent(s, newId);
                if (enrollment.Teacher is { } t) _insertTeacher(t, newId);
                if (enrollment.Group is { } g) _insertGroup(g, newId);

                _byCourseID[enrollment.Course].Add(enrollment.Id);

                return Task.FromResult(newId);
            }
        }

        private void _insertStudent(StudentID studentId, EnrollmentID id)
        {
            if (!_byStudent.TryGetValue(studentId, out var set))
            {
                set = new HashSet<EnrollmentID>();
                _byStudent[studentId] = set;
            }
            set.Add(id);
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
                if (!this._byTeacher.ContainsKey(teacher_id)) throw new Teacher.Entities.Errors.TeacherDoesNotExists($"A teacher with ID {teacher_id} does not exists"); ;
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

        public List<Enrollment> FindByCourseID(Course.Entities.CourseID course_id) {
            lock (_mutex) {
                if (!this._byCourseID.ContainsKey(course_id)) throw new Course.Entities.EnrollmentDoesNotExistsError($"An enrollment with course id ${course_id} does not exists");
                List<Enrollment> list = new List<Enrollment>();
                foreach (var id in this._byCourseID[course_id]) {
                    list.Add(this._database[id]);
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
