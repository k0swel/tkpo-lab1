using BaseRepositories.Interfaces;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Interfaces
{
    public interface StudentRepositoryInterface : BaseRepositories.Interfaces.IRepository<Entities.Student, Entities.StudentID>
    {
        /// <summary>
        /// Update a student (student_object variable must contain ID)
        /// </summary>
        /// <param name="student_object">Student Object.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Student.Student does not exists, the error occured
        /// </exception>
        public new Task UpdateAsync(Entities.Student student_object);
        /// <summary>
        /// Insert entry to the database. Student.StudentID will be ignored in the parametr.
        /// </summary>
        /// <returns>
        /// Copy of student_id, but with the correct ID.
        /// </returns>
        /// <param name="student_object">Student object. Student.StudentID will be ignored. Unique ID will be generated.</param>
        public new Task<StudentID> InsertAsync(Entities.Student student_object);
        /// <summary>
        /// Delete a student with the speicified student_id
        /// </summary>
        /// <param name="id">Student identifier.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Id does not exists in the repository, the error occured
        /// </exception>
        public new Task DeleteAsync(Entities.StudentID Id);
        /// <summary>
        /// Finding course by its ID
        /// </summary>
        /// <param name="Id">Student Identifier.</param>
        /// <returns>Student object or NULL if the student does not exists in the store.</returns>
        public new Task<Entities.Student?> FindByIDAsync(Entities.StudentID Id);
        /// <summary>
        /// Get Number entries in the database
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<UInt64> CountAsync();
        /// <summary>
        /// Finds Out either Student with specified Student.StudentID exists in the repo
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<bool> IsExistsAsync(Entities.StudentID Id);
    }
}
