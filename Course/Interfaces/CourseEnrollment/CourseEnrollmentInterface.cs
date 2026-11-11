using Course.Entities;
using Group.Entities;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Teacher.Entities;



namespace Course.Interfaces
{
    public interface CourseEnrollmentRepository : BaseRepositories.Interfaces.IRepository<Entities.Enrollment, Entities.EnrollmentID>
    {
        List<Enrollment> FindByStudentID(StudentID studentId);
        List<Enrollment> FindByTeacherID(TeacherID teacherID);
        List<Enrollment> FindByGroupID(GroupID groupID);
    }
}
