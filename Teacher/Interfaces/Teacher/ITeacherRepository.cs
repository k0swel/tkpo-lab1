using System;
using System.Collections.Generic;
using System.Text;

namespace Teacher.Interfaces
{
    public interface ITeacherRepository: BaseRepositories.Interfaces.IRepository<Entities.Teacher, Entities.TeacherID>
    {
        /// <summary>
        /// Update the Teacher (teacher_object variable must contain ID)
        /// </summary>
        /// <param name="teacher_object">Teacher Object.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Teacher.TeacherID does not exists, the error occured
        /// </exception>
        public new Task UpdateAsync(Entities.Teacher teacher_object);
        /// <summary>
        /// Delete the Teacher with the speicified teacher_id
        /// </summary>
        /// <param name="teacher_id">Teacher identifier.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the teachere_id does not exists in the repository, the error occured
        /// </exception>
        public new Task DeleteAsync(Entities.TeacherID teacher_id);
        /// <summary>
        /// Finding teaher by its ID
        /// </summary>
        /// <param name="teacher_id">Teacher Identifier.</param>
        /// <returns>Course object or NULL if the techer does not exists in the store.</returns>
        public new Task<Entities.Teacher?> FindByIDAsync(Entities.TeacherID teacher_id);
        /// <summary>
        /// Insert entry to the database. Teacher.TeacherID will be ignored in the parametr.
        /// </summary>
        /// <returns>
        /// Copy of teacher_object, but with the correct ID.
        /// </returns>
        /// <param name="course_object">Teacher object. Teacher.TeacherID will be ignored. Unique ID will be generated.</param>
        public new Task<Entities.TeacherID> InsertAsync(Entities.Teacher teacher_object);
        /// <summary>
        /// Check either a teacher with specified TeacherID is exists.
        /// </summary>
        /// <returns>
        /// True if a teacher exists, otherwise False.
        /// </returns>
        /// <param name="teacher_id">TeacherID variables.</param>
        public new Task<bool> IsExistsAsync(Entities.TeacherID teacher_id);
    }
}
