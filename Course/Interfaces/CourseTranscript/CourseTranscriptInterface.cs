using Course.Entities;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Interfaces
{
    public interface CourseTranscriptRepositoryInterface : BaseRepositories.Interfaces.IRepository<Course.Entities.CourseTranscript, CourseTranscriptID> {
        Task<List<CourseTranscript>> FindByStudentID(StudentID student_id);
        Task<List<CourseTranscript>> FindByStudentIDAndCourseID(StudentID student_id, Course.Entities.CourseID course_id);
        new Task<CourseTranscriptID> InsertAsync(CourseTranscript transcript_object);
    }
}
