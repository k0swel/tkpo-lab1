using BaseRepositories.Entities;
using BaseRepositories.Interfaces;
using Course.Entities;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Repositories
{
    public class InMemoryStudentTranscriptRepository: BaseRepositories.InMemoryRepository<Entities.CourseTranscript, Entities.CourseTranscriptID>, Interfaces.CourseTranscriptRepositoryInterface
    {
        private Dictionary<StudentID, HashSet<CourseTranscriptID>> _byStudentId;
        public InMemoryStudentTranscriptRepository() {
            _byStudentId = new Dictionary<StudentID, HashSet<CourseTranscriptID>>();
        }
        public override async Task<CourseTranscriptID> InsertAsync(CourseTranscript transcript_object) {
            lock (_mutex)
            {
                CourseTranscriptID id = _NextID();
                transcript_object.Id = id;
                _database[id] = transcript_object;
                if (!_byStudentId.ContainsKey(transcript_object.student_id)) _byStudentId.Add(transcript_object.student_id, new HashSet<CourseTranscriptID>());
                _byStudentId[transcript_object.student_id].Add(transcript_object.Id);
            }
            return transcript_object.Id;
        }
        public override async Task DeleteAsync(CourseTranscriptID course_transcript_id) {
            CourseTranscript? course_trabscript_object = await FindByIDAsync(course_transcript_id);
            if (course_trabscript_object is not null)
            {
                lock (_mutex) {
                    _database.Remove(course_transcript_id);
                    this._byStudentId[course_trabscript_object.student_id].Remove(course_transcript_id);
                }
                return;
            }
        }
        public async Task<List<CourseTranscript>> FindByStudentID(StudentID student_id) {
            lock (_mutex)
            {
                HashSet<CourseTranscriptID> coursetranscripts_ids;
                try
                {
                    coursetranscripts_ids = _byStudentId[student_id];
                }
                catch (KeyNotFoundException err) { return new List<CourseTranscript>(); }
                List<CourseTranscript> coursetrancript_objects = new();
                foreach (var coursetranscript_id in coursetranscripts_ids)
                {
                    coursetrancript_objects.Add(_database[coursetranscript_id]);
                }
                return coursetrancript_objects;
            }
        }
        public async Task<List<CourseTranscript>> FindByStudentIDAndCourseID(StudentID student_id, Course.Entities.CourseID course_id)
        {
            lock (_mutex) {
                HashSet<CourseTranscriptID> coursetranscripts_ids;
                try
                {
                    coursetranscripts_ids = _byStudentId[student_id];
                }
                catch (KeyNotFoundException err) { return new List<CourseTranscript>(); }
                List<CourseTranscript> coursetrancript_objects = new();
                foreach (var coursetranscript_id in coursetranscripts_ids)
                {
                    if (_database[coursetranscript_id].course_id == course_id)
                        coursetrancript_objects.Add(_database[coursetranscript_id]);
                }
                return coursetrancript_objects;
            }
        }
        

    }
}
