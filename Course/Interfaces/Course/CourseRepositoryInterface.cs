using Course.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Courseid = Course.Entities.CourseID;
using CourseObj = Course.Entities.Course;

namespace Course.Interfaces
{
    public interface ICourseRepository : BaseRepositories.Interfaces.IRepository<CourseObj, Courseid>
    {
        /// <summary>
        /// Update the course (course_object variable must contain ID)
        /// </summary>
        /// <param name="couse_object">Course Object.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Course.CourseID does not exists, the error occured
        /// </exception>
        public new Task UpdateAsync(CourseObj course_object);
        /// <summary>
        /// Delete the course with the speicified course_id
        /// </summary>
        /// <param name="course_id">Course identifier.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the course_id does not exists in the repository, the error occured
        /// </exception>
        public new Task DeleteAsync(Courseid course_id);
        /// <summary>
        /// Finding course by its ID
        /// </summary>
        /// <param name="course_id">Course Identifier.</param>
        /// <returns>Course object or NULL if the course does not exists in the store.</returns>
        public new Task<CourseObj?> FindByIDAsync(Courseid course_id);
        /// <summary>
        /// Insert entry to the database. Course.ID will be ignored in the parametr.
        /// </summary>
        /// <returns>
        /// CourseId of new entity, but with the correct ID.
        /// </returns>
        /// <param name="course_object">Course object. Course.CourseID will be ignored. Unique ID will be generated.</param>
        public new Task<Courseid> InsertAsync(CourseObj course_object);
        /// <summary>
        /// Get Number entries in the database
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<UInt64> CountAsync();
        /// <summary>
        /// Finds Out either Course with specified Course.CourseID exists in the repo
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<bool> IsExistsAsync(Courseid id);
    }
}
