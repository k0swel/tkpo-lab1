using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Course.Interfaces
{
    public interface ICourseRepository : BaseRepositories.Interfaces.IRepository<Entities.Course, Entities.CourseID>
    {
        /// <summary>
        /// Update the course (course_object variable must contain ID)
        /// </summary>
        /// <param name="couse_object">Course Object.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Course.CourseID does not exists, the error occured
        /// </exception>
        public new Task UpdateAsync(Entities.Course course_object);
        /// <summary>
        /// Delete the course with the speicified course_id
        /// </summary>
        /// <param name="course_id">Course identifier.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the course_id does not exists in the repository, the error occured
        /// </exception>
        public new Task DeleteAsync(Entities.CourseID course_id);
        /// <summary>
        /// Finding course by its ID
        /// </summary>
        /// <param name="course_id">Course Identifier.</param>
        /// <returns>Course object or NULL if the course does not exists in the store.</returns>
        public new Task<Entities.Course?> FindByIDAsync(Entities.CourseID course_id);
        /// <summary>
        /// Insert entry to the database. Course.ID will be ignored in the parametr.
        /// </summary>
        /// <returns>
        /// CourseId of new entity, but with the correct ID.
        /// </returns>
        /// <param name="course_object">Course object. Course.CourseID will be ignored. Unique ID will be generated.</param>
        public new Task<Entities.CourseID> InsertAsync(Entities.Course course_object);
        /// <summary>
        /// Get Number entries in the database
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<UInt64> CountAsync();
        /// <summary>
        /// Finds Out either Course with specified Course.CourseID exists in the repo
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<bool> IsExistsAsync(Entities.CourseID id);
    }
}
