using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Repositories
{
    public class InMemoryStudentRepository : BaseRepositories.InMemoryRepository<Entities.Student, Entities.StudentID>, Interfaces.StudentRepositoryInterface
    {
    }
}
