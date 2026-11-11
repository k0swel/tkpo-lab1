using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Group.Interfaces
{
    public interface GroupRepositoryInterface: BaseRepositories.Interfaces.IRepository<Entities.Group, Entities.GroupID>
    {
        /// <summary>
        /// Update a group (group variable must contain ID)
        /// </summary>
        /// <param name="group_object">Group Object.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Entities.Group does not exists, the error occured
        /// </exception>
        public new Task UpdateAsync(Entities.Group group_object);
        /// <summary>
        /// Insert entry to the database.
        /// </summary>
        /// <returns>
        /// GroupId..
        /// </returns>
        /// <param name="group_object">Group object.</param>
        /// /// <param name="group_id">GroupID</param>
        /// <exception cref="NotImplementedException">
        ///  If the object with the specified Entities.GroupID already exists.
        /// </exception>
        public new Task<Entities.GroupID> InsertAsync(Entities.Group group_object);
        /// <summary>
        /// Delete a student with the speicified student_id
        /// </summary>
        /// <param name="id">Student identifier.</param>
        /// <exception cref="ObjectDoesNotExistsError">
        ///  If the object with the Id does not exists in the repository, the error occured
        /// </exception>
        public new Task DeleteAsync(Entities.GroupID Id);
        /// <summary>
        /// Finding course by its ID
        /// </summary>
        /// <param name="Id">Student Identifier.</param>
        /// <returns>Student object or NULL if the student does not exists in the store.</returns>
        public new Task<Entities.Group?> FindByIDAsync(Entities.GroupID Id);
        /// <summary>
        /// Get Number entries in the database
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<UInt64> CountAsync();
        /// <summary>
        /// Finds Out either Student with specified Student.StudentID exists in the repo
        /// </summary>
        /// <returns>Number of entries in the database</returns>
        public new Task<bool> IsExistsAsync(Entities.GroupID Id);
        public Task<List<Entities.Group>> FindAsync(GroupType? type, GroupState? state, string? RegexPattern, bool DecreasingOrder);

    }
}
